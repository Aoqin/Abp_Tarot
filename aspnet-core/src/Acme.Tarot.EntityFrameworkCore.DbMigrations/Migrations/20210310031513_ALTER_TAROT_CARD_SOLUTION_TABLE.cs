using Microsoft.EntityFrameworkCore.Migrations;

namespace Acme.Tarot.Migrations
{
    public partial class ALTER_TAROT_CARD_SOLUTION_TABLE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TarotCardIdsToString",
                table: "AppTarotCardSolutions",
                newName: "TarotCardIds");

            migrationBuilder.AddForeignKey(
                name: "FK_AppTarotCardSolutions_AppTarotCardCollections_TarotCardCollectionId",
                table: "AppTarotCardSolutions",
                column: "TarotCardCollectionId",
                principalTable: "AppTarotCardCollections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppTarotCardSolutions_AppTarotCardCollections_TarotCardCollectionId",
                table: "AppTarotCardSolutions");

            migrationBuilder.RenameColumn(
                name: "TarotCardIds",
                table: "AppTarotCardSolutions",
                newName: "TarotCardIdsToString");
        }
    }
}
