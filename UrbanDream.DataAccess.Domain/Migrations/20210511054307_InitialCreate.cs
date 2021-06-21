using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BazarJok.DataAccess.Domain.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Role = table.Column<byte>(type: "tinyint", nullable: false),
                    ModeratedPinsCount = table.Column<int>(type: "int", nullable: false),
                    AcceptedPinsCount = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brigades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrigadeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrigadeWorkAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrigadeWorkersCount = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    BrigadePinsCount = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brigades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    BrigadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcceptedModeratorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_Brigades_BrigadeId",
                        column: x => x.BrigadeId,
                        principalTable: "Brigades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PinsEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountOfWatchings = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Raiting = table.Column<double>(type: "float", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<byte>(type: "tinyint", nullable: true),
                    ReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AcceptedModeratorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AcceptedModeratorAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModeratedModeratorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModeratedModeratorAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrigadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PinsEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PinsEntity_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportReportImage",
                columns: table => new
                {
                    ImagesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportReportImage", x => new { x.ImagesId, x.ReportsId });
                    table.ForeignKey(
                        name: "FK_ReportReportImage_ReportImages_ImagesId",
                        column: x => x.ImagesId,
                        principalTable: "ReportImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportReportImage_Reports_ReportsId",
                        column: x => x.ReportsId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImagePinsEntity",
                columns: table => new
                {
                    ImagesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PinsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagePinsEntity", x => new { x.ImagesId, x.PinsId });
                    table.ForeignKey(
                        name: "FK_ImagePinsEntity_Images_ImagesId",
                        column: x => x.ImagesId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImagePinsEntity_PinsEntity_PinsId",
                        column: x => x.PinsId,
                        principalTable: "PinsEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PinsEntityTag",
                columns: table => new
                {
                    PinsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PinsEntityTag", x => new { x.PinsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_PinsEntityTag_PinsEntity_PinsId",
                        column: x => x.PinsId,
                        principalTable: "PinsEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PinsEntityTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "AcceptedPinsCount", "CreationDate", "FirstName", "LastName", "Login", "ModeratedPinsCount", "PasswordHash", "Role" },
                values: new object[] { new Guid("61e8a6b6-53c0-4e55-9a55-6357b83f62b6"), 0, new DateTime(2021, 5, 11, 11, 43, 6, 283, DateTimeKind.Local).AddTicks(1383), null, null, "Admin", 0, "$2a$11$u2woPH3HBxZpLSqAEw5WBulzJfZFpW/G2OlKvMxDUXARlJ0y5ALFa", (byte)5 });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_Login",
                table: "Admins",
                column: "Login",
                unique: true,
                filter: "[Login] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ImagePinsEntity_PinsId",
                table: "ImagePinsEntity",
                column: "PinsId");

            migrationBuilder.CreateIndex(
                name: "IX_PinsEntity_ReportId",
                table: "PinsEntity",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_PinsEntityTag_TagsId",
                table: "PinsEntityTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportReportImage_ReportsId",
                table: "ReportReportImage",
                column: "ReportsId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_BrigadeId",
                table: "Reports",
                column: "BrigadeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "ImagePinsEntity");

            migrationBuilder.DropTable(
                name: "PinsEntityTag");

            migrationBuilder.DropTable(
                name: "ReportReportImage");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "PinsEntity");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "ReportImages");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Brigades");
        }
    }
}
