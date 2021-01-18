using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Acme.Tarot.Migrations
{
    public partial class Created_Tarot_Card_Collection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppTarotCardCollections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Descript = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CardFrontImgUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true, defaultValue: "123"),
                    CardBackImgUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true, defaultValue: "123"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTarotCardCollections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardBindCollection",
                columns: table => new
                {
                    TarotCardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TarotCardCollecitonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardBindCollection", x => new { x.TarotCardId, x.TarotCardCollecitonId });
                    table.ForeignKey(
                        name: "FK_CardBindCollection_AppTarotCardCollections_TarotCardCollecitonId",
                        column: x => x.TarotCardCollecitonId,
                        principalTable: "AppTarotCardCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardBindCollection_AppTarotCards_TarotCardId",
                        column: x => x.TarotCardId,
                        principalTable: "AppTarotCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardBindCollection_TarotCardCollecitonId",
                table: "CardBindCollection",
                column: "TarotCardCollecitonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardBindCollection");

            migrationBuilder.DropTable(
                name: "AppTarotCardCollections");
        }
    }
}
