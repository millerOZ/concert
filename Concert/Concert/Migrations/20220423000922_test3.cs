using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Concert.Migrations
{
    public partial class test3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Entrances_EntranceId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_EntranceId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Entrances_Id",
                table: "Entrances");

            migrationBuilder.DropColumn(
                name: "EntranceId",
                table: "Tickets");

            migrationBuilder.CreateTable(
                name: "TicketEntrance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticket = table.Column<int>(type: "int", nullable: false),
                    Entrance = table.Column<int>(type: "int", nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketEntrance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketEntrance_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entrances_Description",
                table: "Entrances",
                column: "Description",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketEntrance_TicketId",
                table: "TicketEntrance",
                column: "TicketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketEntrance");

            migrationBuilder.DropIndex(
                name: "IX_Entrances_Description",
                table: "Entrances");

            migrationBuilder.AddColumn<int>(
                name: "EntranceId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_EntranceId",
                table: "Tickets",
                column: "EntranceId");

            migrationBuilder.CreateIndex(
                name: "IX_Entrances_Id",
                table: "Entrances",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Entrances_EntranceId",
                table: "Tickets",
                column: "EntranceId",
                principalTable: "Entrances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
