using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Aqt.CoreOracle.[ModuleName].Dtos
{
    /// <summary>
    /// DTO for [EntityName] list items
    /// </summary>
    public class [EntityName]ListDto : EntityDto<Guid>
    {
        /// <summary>
        /// Unique code of the [EntityName]
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Display name of the [EntityName]
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Display order for UI rendering
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Whether the [EntityName] is active
        /// </summary>
        public bool IsActive { get; set; }
    }

    /// <summary>
    /// DTO for creating/updating [EntityName]
    /// </summary>
    public class [EntityName]CreateUpdateDto
    {
        /// <summary>
        /// Unique code of the [EntityName]
        /// </summary>
        [Required]
        [StringLength([EntityName].MaxCodeLength)]
        public string Code { get; set; }

        /// <summary>
        /// Display name of the [EntityName]
        /// </summary>
        [Required]
        [StringLength([EntityName].MaxNameLength)]
        public string Name { get; set; }

        /// <summary>
        /// Display order for UI rendering
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Whether the [EntityName] is active
        /// </summary>
        public bool IsActive { get; set; }
    }

    /// <summary>
    /// DTO for [EntityName] lookup items
    /// </summary>
    public class [EntityName]LookupDto : EntityDto<Guid>
    {
        /// <summary>
        /// Display name of the [EntityName]
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Unique code of the [EntityName]
        /// </summary>
        public string Code { get; set; }
    }
} 