using Aqt.CoreOracle.Application.Contracts.Positions;
using Aqt.CoreOracle.Domain;
using Aqt.CoreOracle.Domain.Positions;
using Aqt.CoreOracle.Permissions;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Aqt.CoreOracle.Application.Positions
{
    public class PositionAppService
        : CrudAppService<
            Position, // The Domain Entity
            PositionDto, // DTO used for reading data
            Guid, // Primary key of the entity
            GetPositionListInput, // Used for filtering and sorting on getting a list
            CreateUpdatePositionDto, // DTO used for creating the entity
            CreateUpdatePositionDto>, // DTO used for updating the entity
          IPositionAppService // The contract interface
    {
        private readonly IPositionRepository _positionRepository;

        public PositionAppService(IPositionRepository repository)
            : base(repository)
        {
            _positionRepository = repository; // Store the specific repository if needed for custom methods

            // Set Policy Names from Permissions
            GetPolicyName = CoreOraclePermissions.OrganizationManagement.Positions.Default;
            GetListPolicyName = CoreOraclePermissions.OrganizationManagement.Positions.Default;
            CreatePolicyName = CoreOraclePermissions.OrganizationManagement.Positions.Create;
            UpdatePolicyName = CoreOraclePermissions.OrganizationManagement.Positions.Update;
            DeletePolicyName = CoreOraclePermissions.OrganizationManagement.Positions.Delete;
        }

        public override async Task<PositionDto> CreateAsync(CreateUpdatePositionDto input)
        {
            await CheckCodeExistsAsync(input.Code);
            return await base.CreateAsync(input);
        }

        public override async Task<PositionDto> UpdateAsync(Guid id, CreateUpdatePositionDto input)
        {
            await CheckCodeExistsAsync(input.Code, id);
            return await base.UpdateAsync(id, input);
        }

        private async Task CheckCodeExistsAsync(string code, Guid? excludeId = null)
        {
            if (await _positionRepository.CodeExistsAsync(code, excludeId))
            {
                throw new BusinessException(CoreOracleDomainErrorCodes.PositionCodeAlreadyExists)
                    .WithData("code", code);
            }
        }
    }
} 