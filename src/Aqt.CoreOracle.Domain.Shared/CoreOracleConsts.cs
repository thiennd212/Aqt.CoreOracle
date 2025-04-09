using System;
using System.Collections.Generic;
using System.Text;

namespace Aqt.CoreOracle
{
    public static class CoreOracleConsts
    {
        public const string DbTablePrefix = "App";

        public const string DbSchema = null;

        // Độ dài tối đa cho các trường - Đảm bảo là const int
        public const int MaxOrganizationUnitDisplayNameLength = 128;
        public const int MaxPositionNameLength = 128;
        public const int MaxPositionCodeLength = 32;
        public const int MaxDescriptionLength = 256; // Đảm bảo tồn tại và là const
        public const int MaxOrganizationUnitAddressLength = 512;
        public const int MaxOrganizationUnitSyncCodeLength = 64;

        // Tên các Extra Properties cho OrganizationUnit
        public const string OrganizationUnitAddress = "Address";
        public const string OrganizationUnitSyncCode = "SyncCode";
        public const string OrganizationUnitType = "OrganizationUnitType";

        // Cache keys
        public const string OrganizationUnitTreeCacheKey = "OrganizationUnitTree";
    }
} 