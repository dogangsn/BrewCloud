﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VetSystems.Account.Infrastructure.Persistence;

#nullable disable

namespace VetSystems.Account.Infrastructure.Migrations
{
    [DbContext(typeof(VetSystemsDbContext))]
    partial class VetSystemsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("VetSystems.Account.Domain.Entities.Abilitygroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("createdate");

                    b.Property<Guid>("EnterprisesId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("enterprisesid");

                    b.Property<string>("Groupname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("groupname");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("updatedate");

                    b.HasKey("Id")
                        .HasName("pk_abilitygroup");

                    b.HasIndex("EnterprisesId")
                        .HasDatabaseName("ix_abilitygroup_enterprisesid");

                    b.ToTable("abilitygroup", (string)null);
                });

            modelBuilder.Entity("VetSystems.Account.Domain.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("birthdate");

                    b.Property<string>("Board")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("board");

                    b.Property<decimal>("CLimit")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("climit");

                    b.Property<DateTime?>("CheckInDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("checkindate");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("createdate");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("email");

                    b.Property<string>("EndDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("enddate");

                    b.Property<int?>("FCardType")
                        .HasColumnType("int")
                        .HasColumnName("fcardtype");

                    b.Property<int>("Gender")
                        .HasColumnType("int")
                        .HasColumnName("gender");

                    b.Property<decimal>("Limit")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("limit");

                    b.Property<string>("MemberNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("membernumber");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<bool>("NoPost")
                        .HasColumnType("bit")
                        .HasColumnName("nopost");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("phone");

                    b.Property<int>("PosSaleType")
                        .HasColumnType("int")
                        .HasColumnName("possaletype");

                    b.Property<string>("PosSaleTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("possaletypename");

                    b.Property<Guid?>("ReasonId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("reasonid");

                    b.Property<Guid?>("RegionId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("regionid");

                    b.Property<string>("RoomNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("roomnumber");

                    b.Property<string>("SourceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("sourceid");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("updatedate");

                    b.Property<string>("VoucherNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("voucherno");

                    b.Property<string>("VoucherRemark")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("voucherremark");

                    b.Property<string>("code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("code");

                    b.HasKey("Id")
                        .HasName("pk_customer");

                    b.ToTable("customer", (string)null);
                });

            modelBuilder.Entity("VetSystems.Account.Domain.Entities.Enterprise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("createdate");

                    b.Property<string>("Currencycode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("currencycode");

                    b.Property<decimal?>("CustomerInvoiceInfoLimit")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("customerinvoiceinfolimit");

                    b.Property<bool>("CustomerSearchStatus")
                        .HasColumnType("bit")
                        .HasColumnName("customersearchstatus");

                    b.Property<string>("Defaultlanguage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("defaultlanguage");

                    b.Property<string>("Enterprisename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("enterprisename");

                    b.Property<bool>("MoneyChange")
                        .HasColumnType("bit")
                        .HasColumnName("moneychange");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("phone");

                    b.Property<string>("Timezone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("timezone");

                    b.Property<Guid>("TimezoneownerdetailId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("timezoneownerdetailid");

                    b.Property<string>("Translationlanguage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("translationlanguage");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("updatedate");

                    b.Property<bool>("UseSafeListControl")
                        .HasColumnType("bit")
                        .HasColumnName("usesafelistcontrol");

                    b.HasKey("Id")
                        .HasName("pk_enterprise");

                    b.ToTable("enterprise", (string)null);
                });

            modelBuilder.Entity("VetSystems.Account.Domain.Entities.Property", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("address");

                    b.Property<string>("Barcode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("barcode");

                    b.Property<string>("Boards")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("boards");

                    b.Property<string>("Calleridrevcenter")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("calleridrevcenter");

                    b.Property<Guid?>("CityLedgerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("cityledgerid");

                    b.Property<Guid?>("ClAgencyAccountId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("clagencyaccountid");

                    b.Property<Guid?>("ClExtraAccountId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("clextraaccountid");

                    b.Property<decimal>("CollectionRatePercentage")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("collectionratepercentage");

                    b.Property<string>("CompanyTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("companytitle");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("createdate");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("currency");

                    b.Property<string>("Currencyname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("currencyname");

                    b.Property<DateTime?>("DefaultDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("defaultdate");

                    b.Property<string>("Defaultlang")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("defaultlang");

                    b.Property<string>("Departments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("departments");

                    b.Property<string>("DocumentPrefix")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("documentprefix");

                    b.Property<string>("DocumentSeri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("documentseri");

                    b.Property<string>("EInvDocumentPrefix")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("einvdocumentprefix");

                    b.Property<string>("EInvDocumentSeri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("einvdocumentseri");

                    b.Property<bool>("Eftposautoclose")
                        .HasColumnType("bit")
                        .HasColumnName("eftposautoclose");

                    b.Property<int>("EndOfDateType")
                        .HasColumnType("int")
                        .HasColumnName("endofdatetype");

                    b.Property<string>("Endoftheday")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("endoftheday");

                    b.Property<bool>("Endofthedayisnextday")
                        .HasColumnType("bit")
                        .HasColumnName("endofthedayisnextday");

                    b.Property<Guid>("EnterprisesId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("enterprisesid");

                    b.Property<Guid?>("ErpCompanyBranchId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("erpcompanybranchid");

                    b.Property<Guid?>("FoControlAccountId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("focontrolaccountid");

                    b.Property<Guid?>("FoExtraInvoiceAccountId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("foextrainvoiceaccountid");

                    b.Property<string>("HotelInformationAddress")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("hotelinformationaddress");

                    b.Property<string>("HotelInformationAppId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("hotelinformationappid");

                    b.Property<string>("HotelInformationAppKey")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("hotelinformationappkey");

                    b.Property<string>("HotelInformationSecretKey")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("hotelinformationsecretkey");

                    b.Property<string>("Markets")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("markets");

                    b.Property<string>("MersisNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("mersisno");

                    b.Property<int>("OrderNumber")
                        .HasColumnType("int")
                        .HasColumnName("ordernumber");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("phone");

                    b.Property<string>("Propertyname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("propertyname");

                    b.Property<bool>("Propertypayment")
                        .HasColumnType("bit")
                        .HasColumnName("propertypayment");

                    b.Property<Guid?>("Regionid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("regionid");

                    b.Property<string>("RoomTypes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("roomtypes");

                    b.Property<string>("SINHotelId")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasColumnName("sinhotelid");

                    b.Property<string>("Serveraddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("serveraddress");

                    b.Property<string>("Subcurrencyname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("subcurrencyname");

                    b.Property<short>("Symbolposition")
                        .HasColumnType("smallint")
                        .HasColumnName("symbolposition");

                    b.Property<string>("Symbolseperator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("symbolseperator");

                    b.Property<short>("Symbolspacing")
                        .HasColumnType("smallint")
                        .HasColumnName("symbolspacing");

                    b.Property<string>("TaxNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("taxnumber");

                    b.Property<string>("TaxOffice")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("taxoffice");

                    b.Property<string>("Thousandseperator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("thousandseperator");

                    b.Property<string>("Timezone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("timezone");

                    b.Property<Guid>("TimezoneownerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("timezoneownerid");

                    b.Property<string>("Translationlang")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("translationlang");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("updatedate");

                    b.Property<bool>("UseChheckPrintEnterpriseLevel")
                        .HasColumnType("bit")
                        .HasColumnName("usechheckprintenterpriselevel");

                    b.Property<bool>("UseOrderPrinterEnterpriseLevel")
                        .HasColumnType("bit")
                        .HasColumnName("useorderprinterenterpriselevel");

                    b.Property<string>("WebSite")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("website");

                    b.HasKey("Id")
                        .HasName("pk_property");

                    b.HasIndex("EnterprisesId")
                        .HasDatabaseName("ix_property_enterprisesid");

                    b.ToTable("property", (string)null);
                });

            modelBuilder.Entity("VetSystems.Account.Domain.Entities.Reason", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("createdate");

                    b.Property<Guid>("EnterprisesId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("enterprisesid");

                    b.Property<int>("Kind")
                        .HasColumnType("int")
                        .HasColumnName("kind");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<int>("Type")
                        .HasColumnType("int")
                        .HasColumnName("type");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("updatedate");

                    b.HasKey("Id")
                        .HasName("pk_reason");

                    b.HasIndex("EnterprisesId")
                        .HasDatabaseName("ix_reason_enterprisesid");

                    b.ToTable("reason", (string)null);
                });

            modelBuilder.Entity("VetSystems.Account.Domain.Entities.ReasonProperties", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("createdate");

                    b.Property<Guid>("EnterprisesId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("enterprisesid");

                    b.Property<Guid>("PropertyId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("propertyid");

                    b.Property<Guid>("ReasonId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("reasonid");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("updatedate");

                    b.HasKey("Id")
                        .HasName("pk_reasonproperties");

                    b.HasIndex("ReasonId")
                        .HasDatabaseName("ix_reasonproperties_reasonid");

                    b.ToTable("reasonproperties", (string)null);
                });

            modelBuilder.Entity("VetSystems.Account.Domain.Entities.Rolesetting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("createdate");

                    b.Property<string>("DashboardPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("dashboardpath");

                    b.Property<Guid>("EnterprisesId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("enterprisesid");

                    b.Property<bool>("Installdevice")
                        .HasColumnType("bit")
                        .HasColumnName("installdevice");

                    b.Property<bool>("IsEnterpriseAdmin")
                        .HasColumnType("bit")
                        .HasColumnName("isenterpriseadmin");

                    b.Property<string>("Rolecode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("rolecode");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("updatedate");

                    b.HasKey("Id")
                        .HasName("pk_rolesetting");

                    b.ToTable("rolesetting", (string)null);
                });

            modelBuilder.Entity("VetSystems.Account.Domain.Entities.RoleSettingDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("action");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("createdate");

                    b.Property<Guid>("RoleSettingId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("rolesettingid");

                    b.Property<string>("Target")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("target");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("updatedate");

                    b.HasKey("Id")
                        .HasName("pk_rolesettingdetail");

                    b.HasIndex("RoleSettingId")
                        .HasDatabaseName("ix_rolesettingdetail_rolesettingid");

                    b.ToTable("rolesettingdetail", (string)null);
                });

            modelBuilder.Entity("VetSystems.Account.Domain.Entities.TempAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("ActivationCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("activationcode");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("company");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("createdate");

                    b.Property<bool?>("IsComplate")
                        .HasColumnType("bit")
                        .HasColumnName("iscomplate");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("phone");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("updatedate");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_tempaccount");

                    b.ToTable("tempaccount", (string)null);
                });

            modelBuilder.Entity("VetSystems.Account.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<bool>("Authorizeenterprise")
                        .HasColumnType("bit")
                        .HasColumnName("authorizeenterprise");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("createdate");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("email");

                    b.Property<Guid>("EnterprisesId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("enterprisesid");

                    b.Property<string>("FirstLastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("firstlastname");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("roleid");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("updatedate");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("VetSystems.Account.Domain.Entities.Userauthorization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("createdate");

                    b.Property<Guid>("EnterprisesId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("enterprisesid");

                    b.Property<Guid>("PropertyId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("propertyid");

                    b.Property<Guid>("Recid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("recid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("roleid");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("updatedate");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("usersid");

                    b.HasKey("Id")
                        .HasName("pk_userauthorization");

                    b.HasIndex("EnterprisesId")
                        .HasDatabaseName("ix_userauthorization_enterprisesid");

                    b.ToTable("userauthorization", (string)null);
                });

            modelBuilder.Entity("VetSystems.Account.Domain.Entities.Abilitygroup", b =>
                {
                    b.HasOne("VetSystems.Account.Domain.Entities.Enterprise", "Enterprises")
                        .WithMany("Abilitygroups")
                        .HasForeignKey("EnterprisesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_abilitygroup_enterprise_enterprisesid");

                    b.Navigation("Enterprises");
                });

            modelBuilder.Entity("VetSystems.Account.Domain.Entities.Property", b =>
                {
                    b.HasOne("VetSystems.Account.Domain.Entities.Enterprise", "Enterprises")
                        .WithMany("Properties")
                        .HasForeignKey("EnterprisesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_property_enterprise_enterprisesid");

                    b.Navigation("Enterprises");
                });

            modelBuilder.Entity("VetSystems.Account.Domain.Entities.Reason", b =>
                {
                    b.HasOne("VetSystems.Account.Domain.Entities.Enterprise", "Enterprises")
                        .WithMany("Reasons")
                        .HasForeignKey("EnterprisesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_reason_enterprise_enterprisesid");

                    b.Navigation("Enterprises");
                });

            modelBuilder.Entity("VetSystems.Account.Domain.Entities.ReasonProperties", b =>
                {
                    b.HasOne("VetSystems.Account.Domain.Entities.Reason", null)
                        .WithMany("ReasonProperties")
                        .HasForeignKey("ReasonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_reasonproperties_reason_reasonid");
                });

            modelBuilder.Entity("VetSystems.Account.Domain.Entities.RoleSettingDetail", b =>
                {
                    b.HasOne("VetSystems.Account.Domain.Entities.Rolesetting", "Rolesetting")
                        .WithMany("Rolesettingdetails")
                        .HasForeignKey("RoleSettingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_rolesettingdetail_rolesetting_rolesettingid");

                    b.Navigation("Rolesetting");
                });

            modelBuilder.Entity("VetSystems.Account.Domain.Entities.Userauthorization", b =>
                {
                    b.HasOne("VetSystems.Account.Domain.Entities.Enterprise", "Enterprises")
                        .WithMany("Userauthorizations")
                        .HasForeignKey("EnterprisesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_userauthorization_enterprise_enterprisesid");

                    b.Navigation("Enterprises");
                });

            modelBuilder.Entity("VetSystems.Account.Domain.Entities.Enterprise", b =>
                {
                    b.Navigation("Abilitygroups");

                    b.Navigation("Properties");

                    b.Navigation("Reasons");

                    b.Navigation("Userauthorizations");
                });

            modelBuilder.Entity("VetSystems.Account.Domain.Entities.Reason", b =>
                {
                    b.Navigation("ReasonProperties");
                });

            modelBuilder.Entity("VetSystems.Account.Domain.Entities.Rolesetting", b =>
                {
                    b.Navigation("Rolesettingdetails");
                });
#pragma warning restore 612, 618
        }
    }
}
