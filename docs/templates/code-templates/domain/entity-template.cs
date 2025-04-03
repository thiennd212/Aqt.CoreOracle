using System;
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
        public string Code { get; private set; }

        /// <summary>
        /// Tên
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Thứ tự hiển thị
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Trạng thái hoạt động
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Thuộc tính mở rộng
        /// </summary>
        public string ExtraProperties { get; set; }
        #endregion

        #region Constructors
        protected [EntityName]() { }

        internal [EntityName](
            Guid id,
            string code,
            string name,
            int displayOrder = 0,
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
            Code = Check.NotNullOrWhiteSpace(
                code,
                nameof(code),
                maxLength: [EntityName]Constants.MaxCodeLength
            );
        }

        internal void SetName(string name)
        {
            Name = Check.NotNullOrWhiteSpace(
                name,
                nameof(name),
                maxLength: [EntityName]Constants.MaxNameLength
            );
        }

        internal void SetDisplayOrder(int displayOrder)
        {
            if (displayOrder < 0)
            {
                throw new BusinessException(CoreOracleDomainErrorCodes.InvalidDisplayOrder);
            }

            DisplayOrder = displayOrder;
        }

        internal void SetExtraProperties(string extraProperties)
        {
            if (!string.IsNullOrEmpty(extraProperties) 
                && extraProperties.Length > [EntityName]Constants.MaxExtraPropertiesLength)
            {
                throw new BusinessException(CoreOracleDomainErrorCodes.ExtraPropertiesTooLong);
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