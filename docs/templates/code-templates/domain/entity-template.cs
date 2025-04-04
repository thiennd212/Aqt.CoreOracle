using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp;

namespace Aqt.CoreOracle.[ModuleName]
{
    /// <summary>
    /// [EntityDescription]
    /// </summary>
    public class [EntityName] : FullAuditedAggregateRoot<Guid>
    {
        #region Properties
        /// <summary>
        /// Mã định danh
        /// </summary>
        [Required]
        [StringLength([EntityName]Constants.MaxCodeLength)]
        public virtual string Code { get; protected set; }

        /// <summary>
        /// Tên
        /// </summary>
        [Required]
        [StringLength([EntityName]Constants.MaxNameLength)]
        public virtual string Name { get; protected set; }

        /// <summary>
        /// Thứ tự hiển thị
        /// </summary>
        public virtual int? DisplayOrder { get; set; }

        /// <summary>
        /// Trạng thái hoạt động
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// Thuộc tính mở rộng
        /// </summary>
        [StringLength([EntityName]Constants.MaxExtraPropertiesLength)]
        public virtual string ExtraProperties { get; set; }
        #endregion

        #region Constructors
        protected [EntityName]() { }

        internal [EntityName](
            Guid id,
            string code,
            string name,
            int? displayOrder = null,
            bool isActive = true
        ) : base(id)
        {
            SetCode(code);
            SetName(name);
            DisplayOrder = displayOrder;
            IsActive = isActive;
        }
        #endregion

        #region Methods
        internal void SetCode(string code)
        {
            if (code?.Length > [EntityName]Constants.MaxCodeLength)
            {
                throw new BusinessException(CoreOracleDomainErrorCodes.CodeTooLong)
                    .WithData("MaxLength", [EntityName]Constants.MaxCodeLength);
            }

            Code = code;
        }

        internal void SetName(string name)
        {
            if (name?.Length > [EntityName]Constants.MaxNameLength)
            {
                throw new BusinessException(CoreOracleDomainErrorCodes.NameTooLong)
                    .WithData("MaxLength", [EntityName]Constants.MaxNameLength);
            }

            Name = name;
        }

        internal void SetDisplayOrder(int? displayOrder)
        {
            if (displayOrder.HasValue && displayOrder.Value < 0)
            {
                throw new BusinessException(CoreOracleDomainErrorCodes.InvalidDisplayOrder);
            }

            DisplayOrder = displayOrder;
        }

        internal void SetExtraProperties(string extraProperties)
        {
            if (extraProperties?.Length > [EntityName]Constants.MaxExtraPropertiesLength)
            {
                throw new BusinessException(CoreOracleDomainErrorCodes.ExtraPropertiesTooLong)
                    .WithData("MaxLength", [EntityName]Constants.MaxExtraPropertiesLength);
            }

            ExtraProperties = extraProperties;
        }
        #endregion
    }
}

// Constants file
namespace Aqt.CoreOracle.[ModuleName]
{
    public static class [EntityName]Constants
    {
        public const int MaxCodeLength = 50;
        public const int MaxNameLength = 100;
        public const int MaxExtraPropertiesLength = 2000;
    }
} 