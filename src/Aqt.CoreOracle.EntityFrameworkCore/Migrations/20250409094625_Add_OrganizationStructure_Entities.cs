using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aqt.CoreOracle.Migrations
{
    /// <inheritdoc />
    public partial class Add_OrganizationStructure_Entities : Migration
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
                name: "AppPositions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    Code = table.Column<string>(type: "NVARCHAR2(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: true),
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
                    table.PrimaryKey("PK_AppPositions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppEmployeePositions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    UserId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    OrganizationUnitId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    PositionId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
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
                    table.PrimaryKey("PK_AppEmployeePositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppEmployeePositions_AbpOrganizationUnits_OrganizationUnitId",
                        column: x => x.OrganizationUnitId,
                        principalTable: "AbpOrganizationUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppEmployeePositions_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppEmployeePositions_AppPositions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "AppPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppEmployeePositions_OrganizationUnitId",
                table: "AppEmployeePositions",
                column: "OrganizationUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_AppEmployeePositions_PositionId",
                table: "AppEmployeePositions",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_AppEmployeePositions_UserId",
                table: "AppEmployeePositions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppEmployeePositions_UserId_OrganizationUnitId_PositionId",
                table: "AppEmployeePositions",
                columns: new[] { "UserId", "OrganizationUnitId", "PositionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppPositions_Code",
                table: "AppPositions",
                column: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppEmployeePositions");

            migrationBuilder.DropTable(
                name: "AppPositions");

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
