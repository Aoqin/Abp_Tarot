using Microsoft.EntityFrameworkCore.Migrations;

namespace Acme.Tarot.Migrations
{
    public partial class EXPEND_TAROT_CARD_COLLECTION_TABLE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DivinationLimit",
                table: "AppTarotCardCollections",
                type: "int",
                nullable: false,
                defaultValue: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DivinationLimit",
                table: "AppTarotCardCollections");
        }
    }
}
