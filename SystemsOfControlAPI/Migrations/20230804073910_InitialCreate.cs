using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemsOfControlAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cabinets",
                columns: table => new
                {
                    Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cabinets__78A1A19CFB1A0891", x => x.Number);
                });

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__District__78A1A19C3DDF1D75", x => x.Number);
                });

            migrationBuilder.CreateTable(
                name: "Specialization",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Speciali__737584F7D7AD2E06", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Adress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: true),
                    Sex = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    District = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Patients__3214EC27A9D32C4C", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Patients__Distri__49C3F6B7",
                        column: x => x.District,
                        principalTable: "District",
                        principalColumn: "Number");
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Cabinet = table.Column<int>(type: "int", nullable: true),
                    Specialization = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    District = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Doctors__3214EC273D471EE0", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Doctors__Cabinet__4CA06362",
                        column: x => x.Cabinet,
                        principalTable: "Cabinets",
                        principalColumn: "Number");
                    table.ForeignKey(
                        name: "FK__Doctors__Distric__4E88ABD4",
                        column: x => x.District,
                        principalTable: "District",
                        principalColumn: "Number");
                    table.ForeignKey(
                        name: "FK__Doctors__Special__4D94879B",
                        column: x => x.Specialization,
                        principalTable: "Specialization",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_Cabinet",
                table: "Doctors",
                column: "Cabinet");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_District",
                table: "Doctors",
                column: "District");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_Specialization",
                table: "Doctors",
                column: "Specialization");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_District",
                table: "Patients",
                column: "District");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Cabinets");

            migrationBuilder.DropTable(
                name: "Specialization");

            migrationBuilder.DropTable(
                name: "District");
        }
    }
}
