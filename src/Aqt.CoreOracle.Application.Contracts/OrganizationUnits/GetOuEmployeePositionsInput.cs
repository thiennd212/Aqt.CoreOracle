using System;
using Volo.Abp.Application.Dtos;

namespace Aqt.CoreOracle.Application.Contracts.OrganizationUnits
{
    public class GetOuEmployeePositionsInput : PagedAndSortedResultRequestDto
    {
        public Guid OrganizationUnitId { get; set; }
        // Có thể thêm các thuộc tính lọc khác ở đây nếu cần
        // public string Filter { get; set; }
    }
} 