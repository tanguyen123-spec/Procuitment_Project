using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Produ_project.Migrations
{
    public partial class AddTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoriesID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    NameCategories = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__EFF907B040C2A572", x => x.CategoriesID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    NameUser = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Role_ = table.Column<bool>(type: "bit", nullable: true),
                    Password_ = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "MainProduct",
                columns: table => new
                {
                    MainProductID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    NameMP = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CategoriesID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainProduct", x => x.MainProductID);
                    table.ForeignKey(
                        name: "FK__MainProdu__Categ__398D8EEE",
                        column: x => x.CategoriesID,
                        principalTable: "Categories",
                        principalColumn: "CategoriesID");
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaAccount = table.Column<string>(type: "varchar(30)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JwtID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    IssuedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_MaAccount",
                        column: x => x.MaAccount,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArtWork",
                columns: table => new
                {
                    AWID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    NameAW = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    MainProductID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ImgagesUrl = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ArtWork__07CE6A4C6351BBC9", x => x.AWID);
                    table.ForeignKey(
                        name: "FK__ArtWork__MainPro__3C69FB99",
                        column: x => x.MainProductID,
                        principalTable: "MainProduct",
                        principalColumn: "MainProductID");
                });

            migrationBuilder.CreateTable(
                name: "SupplierInFo",
                columns: table => new
                {
                    SlID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    SupplierName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CategoriesID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Address_ = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    EstablishedYear = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Numberofworkers = table.Column<int>(type: "int", nullable: true),
                    MainProductID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    MOQ = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Certificate_ = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Customized = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    SampleProcess = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Leadtime = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ExportUS = table.Column<bool>(type: "bit", nullable: true),
                    Websitelink = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    Phone = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    ContactPerson = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    UserID = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    ReviewQA = table.Column<bool>(type: "bit", nullable: true),
                    DateQA = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__SupplierI__Categ__4316F928",
                        column: x => x.CategoriesID,
                        principalTable: "Categories",
                        principalColumn: "CategoriesID");
                    table.ForeignKey(
                        name: "FK__SupplierI__MainP__440B1D61",
                        column: x => x.MainProductID,
                        principalTable: "MainProduct",
                        principalColumn: "MainProductID");
                    table.ForeignKey(
                        name: "FK__SupplierI__UserI__44FF419A",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Quality",
                columns: table => new
                {
                    AWID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    PCScustomer = table.Column<int>(type: "int", nullable: true),
                    color = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    size = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Quality__07CE6A4CD218F140", x => x.AWID);
                    table.ForeignKey(
                        name: "FK__Quality__AWID__3F466844",
                        column: x => x.AWID,
                        principalTable: "ArtWork",
                        principalColumn: "AWID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtWork_MainProductID",
                table: "ArtWork",
                column: "MainProductID");

            migrationBuilder.CreateIndex(
                name: "IX_MainProduct_CategoriesID",
                table: "MainProduct",
                column: "CategoriesID");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_MaAccount",
                table: "RefreshTokens",
                column: "MaAccount");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierInFo_CategoriesID",
                table: "SupplierInFo",
                column: "CategoriesID");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierInFo_MainProductID",
                table: "SupplierInFo",
                column: "MainProductID");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierInFo_UserID",
                table: "SupplierInFo",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quality");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "SupplierInFo");

            migrationBuilder.DropTable(
                name: "ArtWork");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "MainProduct");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
