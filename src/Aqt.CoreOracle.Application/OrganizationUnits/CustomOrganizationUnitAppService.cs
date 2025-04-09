using Aqt.CoreOracle.Application.Contracts.OrganizationUnits;
using Aqt.CoreOracle.Permissions;
using Aqt.CoreOracle.Domain;
using Aqt.CoreOracle.Domain.OrganizationStructure;
using Aqt.CoreOracle.Domain.Positions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;
using System.Linq.Dynamic.Core;
using Volo.Abp.Domain.Entities;
using Aqt.CoreOracle.OrganizationUnits;
using Microsoft.Extensions.Logging;

namespace Aqt.CoreOracle.Application.OrganizationUnits
{
    [Authorize] // Require authorization for all methods by default
    public class CustomOrganizationUnitAppService : ApplicationService, ICustomOrganizationUnitAppService
    {
        private readonly IOrganizationUnitRepository _organizationUnitRepository;
        private readonly OrganizationUnitManager _organizationUnitManager;
        private readonly IIdentityUserRepository _userRepository; // To get user names
        private readonly IPositionRepository _positionRepository; // To get position names
        private readonly IEmployeePositionRepository _employeePositionRepository;
        private readonly IDistributedCache<OrganizationUnitTreeNodeDto[]> _treeCache; // Cache for OU tree

        public CustomOrganizationUnitAppService(
            IOrganizationUnitRepository organizationUnitRepository,
            OrganizationUnitManager organizationUnitManager,
            IIdentityUserRepository userRepository,
            IPositionRepository positionRepository,
            IEmployeePositionRepository employeePositionRepository,
            IDistributedCache<OrganizationUnitTreeNodeDto[]> treeCache)
        {
            _organizationUnitRepository = organizationUnitRepository;
            _organizationUnitManager = organizationUnitManager;
            _userRepository = userRepository;
            _positionRepository = positionRepository;
            _employeePositionRepository = employeePositionRepository;
            _treeCache = treeCache;
        }

        [Authorize(CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Create)]
        public virtual async Task<CustomOrganizationUnitDto> CreateAsync(CreateOrganizationUnitInput input)
        {
            // Instantiate the OrganizationUnit entity
            var organizationUnit = new OrganizationUnit(
                GuidGenerator.Create(), // Generate a new Guid for the Id
                input.DisplayName,
                input.ParentId,
                CurrentTenant.Id // TenantId is usually handled implicitly or set here if needed by constructor/logic
            );

            // Set extra properties on the retrieved entity using the ExtraProperties dictionary
            organizationUnit.ExtraProperties[CoreOracleConsts.OrganizationUnitAddress] = input.Address;
            organizationUnit.ExtraProperties[CoreOracleConsts.OrganizationUnitSyncCode] = input.SyncCode;
            organizationUnit.ExtraProperties[CoreOracleConsts.OrganizationUnitType] = input.OrganizationUnitType;

            // Let the manager handle code generation and validation
            await _organizationUnitManager.CreateAsync(organizationUnit);

            // Clear cache if needed
            await _treeCache.RemoveAsync(CoreOracleConsts.OrganizationUnitTreeCacheKey);

            // Map to DTO and return
            return ObjectMapper.Map<OrganizationUnit, CustomOrganizationUnitDto>(organizationUnit);
        }

        [Authorize(CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Update)]
        public virtual async Task<CustomOrganizationUnitDto> UpdateAsync(Guid id, UpdateOrganizationUnitInput input)
        {
            var organizationUnit = await _organizationUnitRepository.GetAsync(id);

            organizationUnit.DisplayName = input.DisplayName;

            // Update ExtraProperties manually
            organizationUnit.ExtraProperties[CoreOracleConsts.OrganizationUnitAddress] = input.Address;
            organizationUnit.ExtraProperties[CoreOracleConsts.OrganizationUnitSyncCode] = input.SyncCode;
            organizationUnit.ExtraProperties[CoreOracleConsts.OrganizationUnitType] = input.OrganizationUnitType;

            // The OrganizationUnitManager.UpdateAsync mainly publishes events and does minimal updates.
            // The changes to DisplayName and ExtraProperties are tracked by the UOW.
            await _organizationUnitManager.UpdateAsync(organizationUnit);

            // No need to call _organizationUnitRepository.UpdateAsync explicitly
            // as the Unit of Work will save the changes.

            return ObjectMapper.Map<OrganizationUnit, CustomOrganizationUnitDto>(organizationUnit);
        }

        [Authorize(CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            // The OrganizationUnitManager handles checks (e.g., for children) and deletion logic.
            await _organizationUnitManager.DeleteAsync(id);

            // Clear the tree cache after deletion
            await _treeCache.RemoveAsync(CoreOracleConsts.OrganizationUnitTreeCacheKey);
        }

        [Authorize(CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Default)]
        public virtual async Task<CustomOrganizationUnitDto> GetAsync(Guid id)
        {
            var organizationUnit = await _organizationUnitRepository.GetAsync(id);
            return ObjectMapper.Map<OrganizationUnit, CustomOrganizationUnitDto>(organizationUnit);
        }

        [Authorize(CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Default)]
        public virtual async Task<PagedResultDto<CustomOrganizationUnitDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            // Use GetListAsync directly since GetQueryableAsync is not available
            var count = await _organizationUnitRepository.GetCountAsync();

            var organizationUnits = await _organizationUnitRepository.GetListAsync(
                sorting: input.Sorting ?? nameof(OrganizationUnit.DisplayName),
                maxResultCount: input.MaxResultCount,
                skipCount: input.SkipCount
            );

            // Apply filter if input is GetOrganizationUnitInput
            if (input is GetOrganizationUnitInput filterInput && !filterInput.Filter.IsNullOrWhiteSpace())
            {
                // Apply filter on the client side
                organizationUnits = organizationUnits
                    .Where(ou =>
                        ou.DisplayName.Contains(filterInput.Filter) ||
                        (ou.Code != null && ou.Code.StartsWith(filterInput.Filter))
                    ).ToList();

                // Adjust count for filtered results
                count = organizationUnits.Count;
            }

            // Map to DTO
            var organizationUnitDtos = ObjectMapper.Map<List<OrganizationUnit>, List<CustomOrganizationUnitDto>>(organizationUnits);

            return new PagedResultDto<CustomOrganizationUnitDto>(
                count,
                organizationUnitDtos
            );
        }

        [Authorize(CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Default)]
        public virtual async Task<ListResultDto<OrganizationUnitTreeNodeDto>> GetOrganizationTreeAsync()
        {
            OrganizationUnitTreeNodeDto[] cachedTree = null;

            // Try to get from cache first (ignore exceptions)
            try
            {
                cachedTree = await _treeCache.GetAsync(CoreOracleConsts.OrganizationUnitTreeCacheKey);
            }
            catch
            {
                // Ignore cache errors
            }

            if (cachedTree != null)
            {
                return new ListResultDto<OrganizationUnitTreeNodeDto>(cachedTree);
            }

            // Get all organization units
            var organizationUnits = await _organizationUnitRepository.GetListAsync();

            // Map all organizations to tree nodes
            var nodes = ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitTreeNodeDto>>(organizationUnits);

            // Build the tree structure
            var tree = BuildOrganizationUnitTree(nodes);

            // Cache the tree
            try
            {
                await _treeCache.SetAsync(CoreOracleConsts.OrganizationUnitTreeCacheKey, tree.ToArray(),
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) // Cache for 30 minutes
                    });
            }
            catch
            {
                // Ignore cache errors
            }

            return new ListResultDto<OrganizationUnitTreeNodeDto>(tree);
        }

        // Helper method to build the organization unit tree
        private List<OrganizationUnitTreeNodeDto> BuildOrganizationUnitTree(List<OrganizationUnitTreeNodeDto> nodes)
        {
            var rootNodes = nodes.Where(n => n.ParentId == null).ToList();

            // Create dictionary with non-null keys only
            var childNodesWithParent = nodes.Where(n => n.ParentId != null)
                                           .GroupBy(n => n.ParentId.Value)  // Use .Value for non-null values
                                           .ToDictionary(g => g.Key, g => g.ToList());

            // Recursive function to build the tree
            void BuildTree(OrganizationUnitTreeNodeDto node)
            {
                if (childNodesWithParent.TryGetValue(node.Id, out var children))
                {
                    node.Children = children.OrderBy(c => c.DisplayName).ToList();

                    foreach (var child in node.Children)
                    {
                        BuildTree(child);
                    }
                }
                else
                {
                    node.Children = new List<OrganizationUnitTreeNodeDto>();
                }
            }

            // Build the tree for each root node
            foreach (var rootNode in rootNodes)
            {
                BuildTree(rootNode);
            }

            return rootNodes.OrderBy(n => n.DisplayName).ToList();
        }

        [Authorize(CoreOraclePermissions.OrganizationManagement.AssignPositions)]
        public virtual async Task AssignPositionToUserAsync(AssignEmployeePositionDto input)
        {
            // Ensure the organization unit exists
            await _organizationUnitRepository.GetAsync(input.OrganizationUnitId);

            // Ensure the user exists
            await _userRepository.GetAsync(input.UserId);

            // Ensure the position exists
            await _positionRepository.GetAsync(input.PositionId);

            // Check if the employee position already exists
            var existingPositions = await _employeePositionRepository.GetListAsync();
            var existingPosition = existingPositions.FirstOrDefault(
                ep => ep.UserId == input.UserId &&
                      ep.OrganizationUnitId == input.OrganizationUnitId &&
                      ep.PositionId == input.PositionId
            );

            if (existingPosition != null)
            {
                throw new UserFriendlyException(L["EmployeePositionAlreadyExists"]);
            }

            // Create new employee position
            var employeePosition = ObjectMapper.Map<AssignEmployeePositionDto, EmployeePosition>(input);

            // Note: If custom properties like StartDate and EndDate need to be set,
            // ensure that EmployeePosition is derived from ExtensibleObject
            // and CoreOracleConsts has the appropriate constants defined.
            // Alternatively, define these as direct properties in the EmployeePosition class.

            // Save to repository
            try
            {
                await _employeePositionRepository.InsertAsync(employeePosition);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error while assigning position to user: {UserId}, {PositionId}", input.UserId, input.PositionId);
                throw new UserFriendlyException(L["ErrorAssigningPositionToUser"]);
            }
        }

        [Authorize(CoreOraclePermissions.OrganizationManagement.AssignPositions)]
        public virtual async Task RemovePositionFromUserAsync(Guid employeePositionId)
        {
            // Get the employee position
            var employeePosition = await _employeePositionRepository.GetAsync(employeePositionId);

            // Delete from repository
            await _employeePositionRepository.DeleteAsync(employeePosition);
        }

        [Authorize(CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Default)]
        public virtual async Task<PagedResultDto<EmployeePositionDto>> GetEmployeePositionsAsync(
            GetOuEmployeePositionsInput input)
        {
            // Get all employee positions
            var allEmployeePositions = await _employeePositionRepository.GetQueryableAsync();

            // Filter by organization unit ID
            var filteredPositions = allEmployeePositions.Where(ep => ep.OrganizationUnitId == input.OrganizationUnitId).ToList();

            // Get total count
            var count = filteredPositions.Count;

            // Apply sorting if provided
            if (!string.IsNullOrEmpty(input.Sorting))
            {
                try
                {
                    // Sử dụng System.Linq.Dynamic.Core để sắp xếp động
                    // Cần đảm bảo UserName và PositionName có trong DTO hoặc được join vào trước khi sắp xếp
                    // Ví dụ này giả sử sắp xếp dựa trên các thuộc tính trực tiếp của EmployeePosition
                    // Nếu muốn sắp xếp theo UserName/PositionName, cần join hoặc xử lý khác
                    filteredPositions = filteredPositions.AsQueryable().OrderBy(input.Sorting).ToList();
                }
                catch (Exception ex)
                {
                    Logger.LogWarning(ex, "Could not apply sorting: {Sorting} for GetEmployeePositionsAsync. Falling back to default sort.", input.Sorting);
                    // Fallback sort or ignore if dynamic sort fails
                    filteredPositions = filteredPositions.OrderBy(ep => ep.CreationTime).ToList(); // Ví dụ fallback
                }
            }
            else
            {
                // Default sorting if none provided
                filteredPositions = filteredPositions.OrderBy(ep => ep.CreationTime).ToList();
            }

            // Apply paging
            filteredPositions = filteredPositions
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToList();

            // Create DTOs with extended information
            var employeePositionDtos = new List<EmployeePositionDto>();

            // Tối ưu hóa: Lấy danh sách UserId và PositionId cần thiết
            var userIds = filteredPositions.Select(ep => ep.UserId).Distinct().ToList();
            var positionIds = filteredPositions.Select(ep => ep.PositionId).Distinct().ToList();

            // Lấy thông tin User và Position một lần thay vì trong vòng lặp
            // Lấy tất cả Users và Positions liên quan (có thể cần tối ưu hơn nếu lượng dữ liệu lớn)
            // Hoặc lọc hiệu quả hơn nếu repository cho phép GetListAsync(Expression<Func<T, bool>> predicate)
            var allUsers = await _userRepository.GetListByIdsAsync(userIds); // Lấy tất cả user (cân nhắc hiệu năng)
            var allPositions = await _positionRepository.GetQueryableAsync(); // Lấy tất cả position (cân nhắc hiệu năng)

            // Tạo dictionary từ danh sách đã lọc theo Id cần thiết
            var users = allUsers.ToDictionary(u => u.Id);
            var positions = allPositions.Where(p => positionIds.Contains(p.Id)).ToDictionary(p => p.Id);

            foreach (var empPosition in filteredPositions)
            {
                var dto = ObjectMapper.Map<EmployeePosition, EmployeePositionDto>(empPosition);

                // Lấy user name từ dictionary (an toàn hơn)
                if (users.TryGetValue(empPosition.UserId, out var user))
                {
                    dto.UserName = user.UserName;
                }

                // Lấy position name từ dictionary (an toàn hơn)
                if (positions.TryGetValue(empPosition.PositionId, out var positionEntity))
                {
                    dto.PositionName = positionEntity.Name;
                }

                employeePositionDtos.Add(dto);
            }

            return new PagedResultDto<EmployeePositionDto>(count, employeePositionDtos);
        }

        [Authorize(CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Move)]
        public virtual async Task<CustomOrganizationUnitDto> MoveAsync(Guid id, Guid? parentId)
        {
            // Get the organization unit to move
            var organizationUnit = await _organizationUnitRepository.GetAsync(id);

            // Use the OrganizationUnitManager to move the unit
            // This will handle code generation, validation, and updating children codes if necessary
            await _organizationUnitManager.MoveAsync(id, parentId);

            // Get the updated organization unit after moving
            var updatedOrganizationUnit = await _organizationUnitRepository.GetAsync(id);

            // Clear the tree cache after moving
            await _treeCache.RemoveAsync(CoreOracleConsts.OrganizationUnitTreeCacheKey);

            // Map to DTO and return
            return ObjectMapper.Map<OrganizationUnit, CustomOrganizationUnitDto>(updatedOrganizationUnit);
        }

        [Authorize(CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Default)]
        public virtual async Task<ListResultDto<CustomOrganizationUnitDto>> GetListAllAsync()
        {
            // Get all organization units
            var organizationUnits = await _organizationUnitRepository.GetListAsync();

            // Map to DTOs
            var organizationUnitDtos = ObjectMapper.Map<List<OrganizationUnit>, List<CustomOrganizationUnitDto>>(organizationUnits);

            return new ListResultDto<CustomOrganizationUnitDto>(organizationUnitDtos);
        }

        [Authorize(CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Default)]
        public virtual async Task<ListResultDto<CustomOrganizationUnitDto>> GetChildrenAsync(Guid parentId)
        {
            // Get organization unit children based on parentId
            var children = await _organizationUnitRepository.GetChildrenAsync(parentId, includeDetails: true);

            // Order by display name for consistent results
            children = children.OrderBy(ou => ou.DisplayName).ToList();

            // Map to DTOs
            var childrenDtos = ObjectMapper.Map<List<OrganizationUnit>, List<CustomOrganizationUnitDto>>(children);

            return new ListResultDto<CustomOrganizationUnitDto>(childrenDtos);
        }

        [Authorize(CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Default)]
        public virtual async Task<ListResultDto<CustomOrganizationUnitDto>> GetRootListAsync()
        {
            // Get all organization units
            var allOus = await _organizationUnitRepository.GetListAsync();

            // Filter root organization units (those with no parent)
            var rootOus = allOus.Where(ou => ou.ParentId == null).ToList();

            // Order by display name
            rootOus = rootOus.OrderBy(ou => ou.DisplayName).ToList();

            // Map to DTOs
            var rootDtos = ObjectMapper.Map<List<OrganizationUnit>, List<CustomOrganizationUnitDto>>(rootOus);

            return new ListResultDto<CustomOrganizationUnitDto>(rootDtos);
        }

        [Authorize(CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Default)]
        public virtual async Task<CustomOrganizationUnitDto> GetRootAsync()
        {
            // Get all organization units
            var allOus = await _organizationUnitRepository.GetListAsync();

            // Get the first root organization unit (assuming there's at least one)
            var rootOu = allOus.FirstOrDefault(ou => ou.ParentId == null);

            if (rootOu == null)
            {
                throw new UserFriendlyException(L["NoRootOrganizationUnitFound"]);
            }

            // Map to DTO
            return ObjectMapper.Map<OrganizationUnit, CustomOrganizationUnitDto>(rootOu);
        }

        [Authorize(CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Default)]
        public virtual async Task<ListResultDto<CustomOrganizationUnitDto>> GetRootNodesAsync()
        {
            // Get all organization units
            var allOus = await _organizationUnitRepository.GetListAsync();

            // Filter root organization units (those with no parent)
            var rootOus = allOus.Where(ou => ou.ParentId == null).ToList();

            // Order by display name
            rootOus = rootOus.OrderBy(ou => ou.DisplayName).ToList();

            // Map to DTOs
            var rootDtos = ObjectMapper.Map<List<OrganizationUnit>, List<CustomOrganizationUnitDto>>(rootOus);

            return new ListResultDto<CustomOrganizationUnitDto>(rootDtos);
        }

        [Authorize(CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Default)]
        public virtual async Task<CustomOrganizationUnitDto> GetLastChildOrNullAsync(Guid? parentId)
        {
            // Get all children of the parent organization unit
            var query = await _organizationUnitRepository.GetListAsync();
            var children = query.Where(ou => ou.ParentId == parentId).ToList();

            if (children == null || !children.Any())
            {
                return null;
            }

            // Order by Code descending to get the last child
            var lastChild = children.OrderByDescending(ou => ou.Code).FirstOrDefault();

            if (lastChild == null)
            {
                return null;
            }

            // Map to DTO
            return ObjectMapper.Map<OrganizationUnit, CustomOrganizationUnitDto>(lastChild);
        }

        [Authorize(CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Default)]
        public virtual async Task<CustomOrganizationUnitDto> GetOuByCodeAsync(string code)
        {
            // Get all organization units
            var allOus = await _organizationUnitRepository.GetListAsync();

            // Get organization unit by code
            var organizationUnit = allOus.FirstOrDefault(ou => ou.Code == code);

            if (organizationUnit == null)
            {
                throw new EntityNotFoundException(typeof(OrganizationUnit), $"code:{code}");
            }

            // Map to DTO
            return ObjectMapper.Map<OrganizationUnit, CustomOrganizationUnitDto>(organizationUnit);
        }

        [Authorize(CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Default)]
        public virtual async Task<string> GetNextChildCodeAsync(Guid? parentId)
        {
            // Get the next child code using OrganizationUnitManager
            return await _organizationUnitManager.GetNextChildCodeAsync(parentId);
        }

        [Authorize(CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Default)]
        public virtual async Task<string> GetCodeAsync(Guid id)
        {
            // Get organization unit
            var organizationUnit = await _organizationUnitRepository.GetAsync(id);

            // Return its code
            return organizationUnit.Code;
        }

        [Authorize(CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Default)]
        public virtual async Task<ListResultDto<CustomOrganizationUnitDto>> GetRootsAsync()
        {
            // This is similar to GetRootListAsync, just to maintain API compatibility
            return await GetRootListAsync();
        }

        // Add other methods from ICustomOrganizationUnitAppService if any...
    }
}