using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityFramework.Data.Migrations
{
    public partial class InitialCreateTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Addressess_AddressId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_Addressess_AddressId",
                table: "Clubs");

            migrationBuilder.DropForeignKey(
                name: "FK_Races_Addressess_AddressId",
                table: "Races");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addressess",
                table: "Addressess");

            migrationBuilder.RenameTable(
                name: "Addressess",
                newName: "Addresses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressId",
                table: "AspNetUsers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_Addresses_AddressId",
                table: "Clubs",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Races_Addresses_AddressId",
                table: "Races",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_Addresses_AddressId",
                table: "Clubs");

            migrationBuilder.DropForeignKey(
                name: "FK_Races_Addresses_AddressId",
                table: "Races");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "Addressess");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addressess",
                table: "Addressess",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Addressess_AddressId",
                table: "AspNetUsers",
                column: "AddressId",
                principalTable: "Addressess",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_Addressess_AddressId",
                table: "Clubs",
                column: "AddressId",
                principalTable: "Addressess",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Races_Addressess_AddressId",
                table: "Races",
                column: "AddressId",
                principalTable: "Addressess",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
