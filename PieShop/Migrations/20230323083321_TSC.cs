﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PieShop.Migrations
{
    /// <inheritdoc />
    public partial class TSC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShoppingCardId",
                table: "ShoppingCartItems",
                newName: "ShoppingCartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShoppingCartId",
                table: "ShoppingCartItems",
                newName: "ShoppingCardId");
        }
    }
}
