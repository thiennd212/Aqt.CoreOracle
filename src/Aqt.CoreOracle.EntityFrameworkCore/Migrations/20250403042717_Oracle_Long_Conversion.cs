using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aqt.CoreOracle.Migrations
{
    /// <inheritdoc />
    public partial class Oracle_Long_Conversion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Payload",
                table: "OpenIddictTokens",
                type: "long",
                maxLength: 2147483647,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Exceptions",
                table: "AbpAuditLogs",
                type: "long",
                maxLength: 2147483647,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Parameters",
                table: "AbpAuditLogActions",
                type: "long",
                maxLength: 2147483647,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)",
                oldMaxLength: 2000,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Payload",
                table: "OpenIddictTokens",
                type: "NVARCHAR2(2000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "long",
                oldMaxLength: 2147483647,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Exceptions",
                table: "AbpAuditLogs",
                type: "NVARCHAR2(2000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "long",
                oldMaxLength: 2147483647,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Parameters",
                table: "AbpAuditLogActions",
                type: "NVARCHAR2(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "long",
                oldMaxLength: 2147483647,
                oldNullable: true);
        }
    }
}
