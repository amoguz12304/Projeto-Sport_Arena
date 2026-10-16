using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiSportArena.Migrations
{
    /// <inheritdoc />
    public partial class MudaDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarrinhoItem_Carrinho_CarrinhoId",
                table: "CarrinhoItem");

            migrationBuilder.AddForeignKey(
                name: "FK_CarrinhoItem_Carrinho_CarrinhoId",
                table: "CarrinhoItem",
                column: "CarrinhoId",
                principalTable: "Carrinho",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarrinhoItem_Carrinho_CarrinhoId",
                table: "CarrinhoItem");

            migrationBuilder.AddForeignKey(
                name: "FK_CarrinhoItem_Carrinho_CarrinhoId",
                table: "CarrinhoItem",
                column: "CarrinhoId",
                principalTable: "Carrinho",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
