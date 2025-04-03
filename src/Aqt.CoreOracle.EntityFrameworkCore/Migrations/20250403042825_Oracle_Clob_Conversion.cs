using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aqt.CoreOracle.Migrations
{
    /// <inheritdoc />
    public partial class Oracle_Clob_Conversion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Payload",
                table: "OpenIddictTokens",
                type: "clob",
                maxLength: 4000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "long",
                oldMaxLength: 2147483647,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Exceptions",
                table: "AbpAuditLogs",
                type: "clob",
                maxLength: 4000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "long",
                oldMaxLength: 2147483647,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Parameters",
                table: "AbpAuditLogActions",
                type: "clob",
                maxLength: 4000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "long",
                oldMaxLength: 2147483647,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Payload",
                table: "OpenIddictTokens",
                type: "long",
                maxLength: 2147483647,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "clob",
                oldMaxLength: 4000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Exceptions",
                table: "AbpAuditLogs",
                type: "long",
                maxLength: 2147483647,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "clob",
                oldMaxLength: 4000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Parameters",
                table: "AbpAuditLogActions",
                type: "long",
                maxLength: 2147483647,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "clob",
                oldMaxLength: 4000,
                oldNullable: true);
        }
    }
}
