namespace HRDCD.Order.Database.Migrations;

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

/// <inheritdoc />
public partial class M1 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "order",
            columns: table => new
            {
                id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                order_num = table.Column<string>(type: "text", nullable: false),
                order_name = table.Column<string>(type: "text", nullable: false),
                order_desc = table.Column<string>(type: "text", nullable: false),
                ins_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                upd_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                del_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                is_del = table.Column<bool>(type: "boolean", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_order", x => x.id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "order");
    }
}