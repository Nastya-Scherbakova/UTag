using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UTag.Migrations
{
    public partial class UpdateConnections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagConnections_Products_ConnectToId",
                table: "TagConnections");

            migrationBuilder.DropForeignKey(
                name: "FK_TagConnections_Persons_PersonId",
                table: "TagConnections");

            migrationBuilder.DropIndex(
                name: "IX_TagConnections_ConnectToId",
                table: "TagConnections");

            migrationBuilder.DropIndex(
                name: "IX_TagConnections_PersonId",
                table: "TagConnections");

            migrationBuilder.DropColumn(
                name: "ConnectToId",
                table: "TagConnections");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "TagConnections");

            migrationBuilder.RenameColumn(
                name: "ConnectToType",
                table: "TagConnections",
                newName: "ProductId");

            migrationBuilder.CreateTable(
                name: "PersonTag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonTag_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonTag_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagConnections_ProductId",
                table: "TagConnections",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonTag_PersonId",
                table: "PersonTag",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonTag_TagId",
                table: "PersonTag",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_TagConnections_Products_ProductId",
                table: "TagConnections",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagConnections_Products_ProductId",
                table: "TagConnections");

            migrationBuilder.DropTable(
                name: "PersonTag");

            migrationBuilder.DropIndex(
                name: "IX_TagConnections_ProductId",
                table: "TagConnections");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "TagConnections",
                newName: "ConnectToType");

            migrationBuilder.AddColumn<int>(
                name: "ConnectToId",
                table: "TagConnections",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "TagConnections",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TagConnections_ConnectToId",
                table: "TagConnections",
                column: "ConnectToId");

            migrationBuilder.CreateIndex(
                name: "IX_TagConnections_PersonId",
                table: "TagConnections",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_TagConnections_Products_ConnectToId",
                table: "TagConnections",
                column: "ConnectToId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagConnections_Persons_PersonId",
                table: "TagConnections",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
