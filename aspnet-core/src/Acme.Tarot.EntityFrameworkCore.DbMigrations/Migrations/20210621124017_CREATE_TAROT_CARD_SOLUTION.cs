using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Acme.Tarot.Migrations
{
    public partial class CREATE_TAROT_CARD_SOLUTION : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppTarotCardSolutions_AppTarotCardCollections_TarotCardCollectionId",
                table: "AppTarotCardSolutions");

            migrationBuilder.AddColumn<Guid>(
                name: "TarotCardSolutionTarotCardCollectionId",
                table: "AppTarotCards",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TarotCardSolutionTarotCardIds",
                table: "AppTarotCards",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppTarotCards_TarotCardSolutionTarotCardCollectionId_TarotCardSolutionTarotCardIds",
                table: "AppTarotCards",
                columns: new[] { "TarotCardSolutionTarotCardCollectionId", "TarotCardSolutionTarotCardIds" });

            migrationBuilder.AddForeignKey(
                name: "FK_AppTarotCards_AppTarotCardSolutions_TarotCardSolutionTarotCardCollectionId_TarotCardSolutionTarotCardIds",
                table: "AppTarotCards",
                columns: new[] { "TarotCardSolutionTarotCardCollectionId", "TarotCardSolutionTarotCardIds" },
                principalTable: "AppTarotCardSolutions",
                principalColumns: new[] { "TarotCardCollectionId", "TarotCardIds" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppTarotCards_AppTarotCardSolutions_TarotCardSolutionTarotCardCollectionId_TarotCardSolutionTarotCardIds",
                table: "AppTarotCards");

            migrationBuilder.DropIndex(
                name: "IX_AppTarotCards_TarotCardSolutionTarotCardCollectionId_TarotCardSolutionTarotCardIds",
                table: "AppTarotCards");

            migrationBuilder.DropColumn(
                name: "TarotCardSolutionTarotCardCollectionId",
                table: "AppTarotCards");

            migrationBuilder.DropColumn(
                name: "TarotCardSolutionTarotCardIds",
                table: "AppTarotCards");

            migrationBuilder.AddForeignKey(
                name: "FK_AppTarotCardSolutions_AppTarotCardCollections_TarotCardCollectionId",
                table: "AppTarotCardSolutions",
                column: "TarotCardCollectionId",
                principalTable: "AppTarotCardCollections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
