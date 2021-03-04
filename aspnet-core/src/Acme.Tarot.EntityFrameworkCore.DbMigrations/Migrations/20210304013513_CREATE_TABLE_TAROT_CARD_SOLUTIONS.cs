using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Acme.Tarot.Migrations
{
    public partial class CREATE_TABLE_TAROT_CARD_SOLUTIONS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppTarotCardSolutions",
                columns: table => new
                {
                    TarotCardCollectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TarotCardIdsToString = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Hexagram = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    HexagramExplain = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTarotCardSolutions", x => new { x.TarotCardCollectionId, x.TarotCardIdsToString });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppTarotCardSolutions");
        }
    }
}
