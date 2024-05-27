using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetSystems.Account.Infrastructure.Migrations
{
    public partial class InitalData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    board = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false),
                    sourceid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    reasonid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    roomnumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    possaletype = table.Column<int>(type: "int", nullable: false),
                    possaletypename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nopost = table.Column<bool>(type: "bit", nullable: false),
                    fcardtype = table.Column<int>(type: "int", nullable: true),
                    enddate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    membernumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    checkindate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    regionid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    limit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    climit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    voucherno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    voucherremark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    birthdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "enterprise",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    enterprisename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    currencycode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    defaultlanguage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    translationlanguage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    timezone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    timezoneownerdetailid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customerinvoiceinfolimit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    usesafelistcontrol = table.Column<bool>(type: "bit", nullable: false),
                    customersearchstatus = table.Column<bool>(type: "bit", nullable: false),
                    moneychange = table.Column<bool>(type: "bit", nullable: false),
                    createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_enterprise", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "rolesetting",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    rolecode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    enterprisesid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    installdevice = table.Column<bool>(type: "bit", nullable: false),
                    isenterpriseadmin = table.Column<bool>(type: "bit", nullable: false),
                    dashboardpath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rolesetting", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tempaccount",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    activationcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    iscomplate = table.Column<bool>(type: "bit", nullable: true),
                    createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tempaccount", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    firstlastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    roleid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    enterprisesid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    authorizeenterprise = table.Column<bool>(type: "bit", nullable: false),
                    createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "abilitygroup",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    groupname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    enterprisesid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_abilitygroup", x => x.id);
                    table.ForeignKey(
                        name: "fk_abilitygroup_enterprise_enterprisesid",
                        column: x => x.enterprisesid,
                        principalTable: "enterprise",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "property",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    propertyname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    serveraddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    timezone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    endoftheday = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    calleridrevcenter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    endofthedayisnextday = table.Column<bool>(type: "bit", nullable: false),
                    eftposautoclose = table.Column<bool>(type: "bit", nullable: false),
                    propertypayment = table.Column<bool>(type: "bit", nullable: false),
                    currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    thousandseperator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    symbolseperator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    symbolposition = table.Column<short>(type: "smallint", nullable: false),
                    symbolspacing = table.Column<short>(type: "smallint", nullable: false),
                    currencyname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    subcurrencyname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    translationlang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    defaultlang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    enterprisesid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    timezoneownerid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    collectionratepercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    barcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ordernumber = table.Column<int>(type: "int", nullable: false),
                    documentprefix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    documentseri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    einvdocumentprefix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    einvdocumentseri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    usechheckprintenterpriselevel = table.Column<bool>(type: "bit", nullable: false),
                    useorderprinterenterpriselevel = table.Column<bool>(type: "bit", nullable: false),
                    endofdatetype = table.Column<int>(type: "int", nullable: false),
                    defaultdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    companytitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mersisno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    website = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    taxoffice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    taxnumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    boards = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    roomtypes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    markets = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cityledgerid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    departments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    erpcompanybranchid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    clagencyaccountid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    foextrainvoiceaccountid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    focontrolaccountid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    clextraaccountid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    sinhotelid = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    hotelinformationaddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    hotelinformationappid = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    hotelinformationappkey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    hotelinformationsecretkey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    regionid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_property", x => x.id);
                    table.ForeignKey(
                        name: "fk_property_enterprise_enterprisesid",
                        column: x => x.enterprisesid,
                        principalTable: "enterprise",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reason",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kind = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    enterprisesid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reason", x => x.id);
                    table.ForeignKey(
                        name: "fk_reason_enterprise_enterprisesid",
                        column: x => x.enterprisesid,
                        principalTable: "enterprise",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userauthorization",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    recid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    usersid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    propertyid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    roleid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    enterprisesid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_userauthorization", x => x.id);
                    table.ForeignKey(
                        name: "fk_userauthorization_enterprise_enterprisesid",
                        column: x => x.enterprisesid,
                        principalTable: "enterprise",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rolesettingdetail",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    target = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rolesettingid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rolesettingdetail", x => x.id);
                    table.ForeignKey(
                        name: "fk_rolesettingdetail_rolesetting_rolesettingid",
                        column: x => x.rolesettingid,
                        principalTable: "rolesetting",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reasonproperties",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    reasonid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    propertyid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    enterprisesid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reasonproperties", x => x.id);
                    table.ForeignKey(
                        name: "fk_reasonproperties_reason_reasonid",
                        column: x => x.reasonid,
                        principalTable: "reason",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_abilitygroup_enterprisesid",
                table: "abilitygroup",
                column: "enterprisesid");

            migrationBuilder.CreateIndex(
                name: "ix_property_enterprisesid",
                table: "property",
                column: "enterprisesid");

            migrationBuilder.CreateIndex(
                name: "ix_reason_enterprisesid",
                table: "reason",
                column: "enterprisesid");

            migrationBuilder.CreateIndex(
                name: "ix_reasonproperties_reasonid",
                table: "reasonproperties",
                column: "reasonid");

            migrationBuilder.CreateIndex(
                name: "ix_rolesettingdetail_rolesettingid",
                table: "rolesettingdetail",
                column: "rolesettingid");

            migrationBuilder.CreateIndex(
                name: "ix_userauthorization_enterprisesid",
                table: "userauthorization",
                column: "enterprisesid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "abilitygroup");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "property");

            migrationBuilder.DropTable(
                name: "reasonproperties");

            migrationBuilder.DropTable(
                name: "rolesettingdetail");

            migrationBuilder.DropTable(
                name: "tempaccount");

            migrationBuilder.DropTable(
                name: "userauthorization");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "reason");

            migrationBuilder.DropTable(
                name: "rolesetting");

            migrationBuilder.DropTable(
                name: "enterprise");
        }
    }
}
