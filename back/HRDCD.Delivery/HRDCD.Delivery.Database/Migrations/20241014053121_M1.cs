using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HRDCD.Delivery.Database.Migrations
{
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
                    order_name = table.Column<string>(type: "text", nullable: true),
                    order_desc = table.Column<string>(type: "text", nullable: true),
                    ins_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    upd_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    del_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_del = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "delivery",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    delivery_status = table.Column<int>(type: "integer", nullable: false),
                    order_entity_id = table.Column<long>(type: "bigint", nullable: false),
                    ins_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    upd_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    del_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_del = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_delivery", x => x.id);
                    table.ForeignKey(
                        name: "FK_delivery_order_order_entity_id",
                        column: x => x.order_entity_id,
                        principalTable: "order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_delivery_order_entity_id",
                table: "delivery",
                column: "order_entity_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_order_order_num",
                table: "order",
                column: "order_num",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "delivery");

            migrationBuilder.DropTable(
                name: "order");
        }
    }
}
