using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelReservationsManager.Migrations
{
    public partial class HotelDb1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isAdult = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "rooms",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    capacity = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    priceAdult = table.Column<double>(type: "float", nullable: false),
                    priceChild = table.Column<double>(type: "float", nullable: false),
                    number = table.Column<int>(type: "int", nullable: false),
                    isFree = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rooms", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    secondName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    thirdName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EGN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isAdmin = table.Column<bool>(type: "bit", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dismisalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    recruitmentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "reservations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    roomid = table.Column<int>(type: "int", nullable: true),
                    userid = table.Column<int>(type: "int", nullable: true),
                    dateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    breakfast = table.Column<bool>(type: "bit", nullable: false),
                    allInclusive = table.Column<bool>(type: "bit", nullable: false),
                    cost = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservations", x => x.id);
                    table.ForeignKey(
                        name: "FK_reservations_rooms_roomid",
                        column: x => x.roomid,
                        principalTable: "rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_reservations_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "linkers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    clientid = table.Column<int>(type: "int", nullable: true),
                    reservationid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_linkers", x => x.id);
                    table.ForeignKey(
                        name: "FK_linkers_clients_clientid",
                        column: x => x.clientid,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_linkers_reservations_reservationid",
                        column: x => x.reservationid,
                        principalTable: "reservations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_linkers_clientid",
                table: "linkers",
                column: "clientid");

            migrationBuilder.CreateIndex(
                name: "IX_linkers_reservationid",
                table: "linkers",
                column: "reservationid");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_roomid",
                table: "reservations",
                column: "roomid");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_userid",
                table: "reservations",
                column: "userid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "linkers");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "reservations");

            migrationBuilder.DropTable(
                name: "rooms");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
