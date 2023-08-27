using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetSystems.Vet.Infrastructure.Migrations
{
    public partial class updatedata_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "vetadress",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    district = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    county = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    longadress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleted = table.Column<bool>(type: "bit", nullable: false),
                    deletedusers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    updateusers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createusers = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vetadress", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vetanimalbreedsdef",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    animaltype = table.Column<int>(type: "int", nullable: false),
                    breedname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("VetAnimalBreedsDef_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "vetanimalcolorsdef",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("VetAnimalColorsDef_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "vetanimalstype",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("VetAnimalsType_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "vetcasingdefinition",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    casename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    kasa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    durumu = table.Column<bool>(type: "bit", nullable: true),
                    createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleted = table.Column<bool>(type: "bit", nullable: false),
                    deletedusers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    updateusers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createusers = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("VetCasingDefinition_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vetcustomergroupdef",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleted = table.Column<bool>(type: "bit", nullable: false),
                    deletedusers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    updateusers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createusers = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("VetCustomerGroupDef_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vetproductcategories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    categorycode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleted = table.Column<bool>(type: "bit", nullable: false),
                    deletedusers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    updateusers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createusers = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("VetProductCategories_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vetproducts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    unitid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    categoryid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    producttypeid = table.Column<int>(type: "int", nullable: true),
                    supplierid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    productbarcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ratio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    buyingprice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    sellingprice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    criticalamount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: true),
                    sellingincludekdv = table.Column<bool>(type: "bit", nullable: true),
                    buyingincludekdv = table.Column<bool>(type: "bit", nullable: true),
                    fixprice = table.Column<bool>(type: "bit", nullable: true),
                    isexpirationdate = table.Column<bool>(type: "bit", nullable: true),
                    createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleted = table.Column<bool>(type: "bit", nullable: false),
                    deletedusers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    updateusers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createusers = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Vetproducts_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vetstores",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    depotcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    depotname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleted = table.Column<bool>(type: "bit", nullable: false),
                    deletedusers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    updateusers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createusers = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("VetStores_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vetsuppliers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    suppliername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleted = table.Column<bool>(type: "bit", nullable: false),
                    deletedusers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    updateusers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createusers = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("VetSuppliers_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vetunits",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    unitcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    unitname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleted = table.Column<bool>(type: "bit", nullable: false),
                    deletedusers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    updateusers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createusers = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("VetUnits_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vetcustomers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phonenumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phonenumber2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    taxoffice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vkntcno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customergroup = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    discountrate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    isemail = table.Column<bool>(type: "bit", nullable: true),
                    isphone = table.Column<bool>(type: "bit", nullable: true),
                    adressid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleted = table.Column<bool>(type: "bit", nullable: false),
                    deletedusers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    updateusers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createusers = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("VetCustomers_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_vetcustomers_vetadress_adressid",
                        column: x => x.adressid,
                        principalTable: "vetadress",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vetpatients",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customerid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    chipnumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sex = table.Column<int>(type: "int", nullable: false),
                    animaltype = table.Column<int>(type: "int", nullable: true),
                    animalbreed = table.Column<int>(type: "int", nullable: true),
                    animalcolor = table.Column<int>(type: "int", nullable: false),
                    reportnumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    specialnote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sterilization = table.Column<bool>(type: "bit", nullable: false),
                    images = table.Column<byte>(type: "tinyint", nullable: true),
                    active = table.Column<bool>(type: "bit", nullable: true),
                    customersid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleted = table.Column<bool>(type: "bit", nullable: false),
                    deletedusers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    updateusers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createusers = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("VetPatients_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_vetpatients_vetcustomers_customersid",
                        column: x => x.customersid,
                        principalTable: "vetcustomers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_vetcustomers_adressid",
                table: "vetcustomers",
                column: "adressid");

            migrationBuilder.CreateIndex(
                name: "ix_vetpatients_customersid",
                table: "vetpatients",
                column: "customersid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vetanimalbreedsdef");

            migrationBuilder.DropTable(
                name: "vetanimalcolorsdef");

            migrationBuilder.DropTable(
                name: "vetanimalstype");

            migrationBuilder.DropTable(
                name: "vetcasingdefinition");

            migrationBuilder.DropTable(
                name: "vetcustomergroupdef");

            migrationBuilder.DropTable(
                name: "vetpatients");

            migrationBuilder.DropTable(
                name: "vetproductcategories");

            migrationBuilder.DropTable(
                name: "vetproducts");

            migrationBuilder.DropTable(
                name: "vetstores");

            migrationBuilder.DropTable(
                name: "vetsuppliers");

            migrationBuilder.DropTable(
                name: "vetunits");

            migrationBuilder.DropTable(
                name: "vetcustomers");

            migrationBuilder.DropTable(
                name: "vetadress");
        }
    }
}
