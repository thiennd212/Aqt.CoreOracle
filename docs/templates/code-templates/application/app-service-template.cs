using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aqt.CoreOracle.[ModuleName].Dtos;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Aqt.CoreOracle.[ModuleName]
{
    /// <summary>
    /// Application service implementation for managing [EntityName]s
    /// </summary>
    [Authorize([ModuleName]Permissions.[EntityName]s.Default)]
    public class [EntityName]AppService :
        CrudAppService<
            [EntityName],
            [EntityName]ListDto,
            Guid,
            PagedAndSortedResultRequestDto,
            [EntityName]CreateUpdateDto,
            [EntityName]CreateUpdateDto>,
        I[EntityName]AppService
    {
        protected I[EntityName]Repository [EntityName]Repository { get; }

        public [EntityName]AppService(I[EntityName]Repository repository)
            : base(repository)
        {
            [EntityName]Repository = repository;

            GetPolicyName = [ModuleName]Permissions.[EntityName]s.Default;
            GetListPolicyName = [ModuleName]Permissions.[EntityName]s.Default;
            CreatePolicyName = [ModuleName]Permissions.[EntityName]s.Create;
            UpdatePolicyName = [ModuleName]Permissions.[EntityName]s.Edit;
            DeletePolicyName = [ModuleName]Permissions.[EntityName]s.Delete;
        }

        public virtual async Task<[EntityName]ListDto> GetByCodeAsync(string code)
        {
            var entity = await [EntityName]Repository.GetByCodeAsync(code);
            return ObjectMapper.Map<[EntityName], [EntityName]ListDto>(entity);
        }

        public virtual async Task<ListResultDto<[EntityName]LookupDto>> GetLookupAsync(
            string filter = null,
            bool? isActive = null)
        {
            var items = await [EntityName]Repository.GetListAsync(
                filter: filter,
                isActive: isActive,
                maxResultCount: MaxLookupCount);

            return new ListResultDto<[EntityName]LookupDto>(
                ObjectMapper.Map<List<[EntityName]>, List<[EntityName]LookupDto>>(items));
        }

        protected override async Task ValidateCreateAsync([EntityName]CreateUpdateDto input)
        {
            await ValidateCodeUniqueAsync(input.Code);
        }

        protected override async Task ValidateUpdateAsync(Guid id, [EntityName]CreateUpdateDto input)
        {
            await ValidateCodeUniqueAsync(input.Code, id);
        }

        protected virtual async Task ValidateCodeUniqueAsync(string code, Guid? expectedId = null)
        {
            if (await [EntityName]Repository.ExistsByCodeAsync(code, expectedId))
            {
                throw new [EntityName]CodeAlreadyExistsException(code);
            }
        }

        protected override IQueryable<[EntityName]> ApplyDefaultSorting(IQueryable<[EntityName]> query)
        {
            return query.OrderBy(x => x.DisplayOrder);
        }

        private const int MaxLookupCount = 100;
    }
} 