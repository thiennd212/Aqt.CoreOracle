using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aqt.CoreOracle.Migrations
{
    /// <inheritdoc />
    public partial class Add_Category_Table_And_Set_Default_AllowNull_String : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AbpUsers",
                type: "NVARCHAR2(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AbpTenants",
                type: "NVARCHAR2(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AbpSecurityLogs",
                type: "NVARCHAR2(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AbpRoles",
                type: "NVARCHAR2(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AbpOrganizationUnits",
                type: "NVARCHAR2(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AbpClaimTypes",
                type: "NVARCHAR2(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(40)",
                oldMaxLength: 40);

            migrationBuilder.CreateTable(
                name: "AppCategoryTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Code = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    AllowMultipleSelect = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    IsSystem = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    ExtraProperties = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "NVARCHAR2(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "RAW(16)", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "RAW(16)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "BOOLEAN", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "RAW(16)", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCategoryTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppCategoryItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    CategoryTypeId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Code = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR2(2000)", maxLength: 2000, nullable: true),
                    DisplayOrder = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    ParentId = table.Column<Guid>(type: "RAW(16)", nullable: true),
                    IsActive = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    Value = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: true),
                    Icon = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    ExtraProperties = table.Column<string>(type: "NVARCHAR2(2000)", maxLength: 2000, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "RAW(16)", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "RAW(16)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "BOOLEAN", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "RAW(16)", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCategoryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCategoryItems_AppCategoryItems_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AppCategoryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppCategoryItems_AppCategoryTypes_CategoryTypeId",
                        column: x => x.CategoryTypeId,
                        principalTable: "AppCategoryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppCategoryItems_CategoryTypeId_Code",
                table: "AppCategoryItems",
                columns: new[] { "CategoryTypeId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppCategoryItems_ParentId",
                table: "AppCategoryItems",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCategoryTypes_Code",
                table: "AppCategoryTypes",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppCategoryItems");

            migrationBuilder.DropTable(
                name: "AppCategoryTypes");

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AbpUsers",
                type: "NVARCHAR2(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(40)",
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AbpTenants",
                type: "NVARCHAR2(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(40)",
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AbpSecurityLogs",
                type: "NVARCHAR2(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(40)",
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AbpRoles",
                type: "NVARCHAR2(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(40)",
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AbpOrganizationUnits",
                type: "NVARCHAR2(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(40)",
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AbpClaimTypes",
                type: "NVARCHAR2(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(40)",
                oldMaxLength: 40,
                oldNullable: true);
        }
    }
}
