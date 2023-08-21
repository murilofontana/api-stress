using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class NullableField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<string>>(
                name: "Stack",
                table: "Pessoa",
                type: "text[]",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(List<string>),
                oldType: "text[]",
                oldMaxLength: 32);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<string>>(
                name: "Stack",
                table: "Pessoa",
                type: "text[]",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(List<string>),
                oldType: "text[]",
                oldMaxLength: 32,
                oldNullable: true);
        }
    }
}
