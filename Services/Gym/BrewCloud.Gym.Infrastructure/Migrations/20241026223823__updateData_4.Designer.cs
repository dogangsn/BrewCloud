﻿// <auto-generated />
using System;
using BrewCloud.Gym.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BrewCloud.Gym.Infrastructure.Migrations
{
    [DbContext(typeof(GymDbContext))]
    [Migration("20241026223823__updateData_4")]
    partial class _updateData_4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BrewCloud.Gym.Domain.Entities.GymLogs", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("createdate");

                    b.Property<string>("CreateUsers")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("createusers");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2")
                        .HasColumnName("date");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit")
                        .HasColumnName("deleted");

                    b.Property<DateTime>("DeletedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("deleteddate");

                    b.Property<string>("DeletedUsers")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("deletedusers");

                    b.Property<string>("FieldName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("fieldname");

                    b.Property<string>("MasterId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("masterid");

                    b.Property<string>("NewValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("newvalue");

                    b.Property<string>("OldValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("oldvalue");

                    b.Property<int>("RecId")
                        .HasColumnType("int")
                        .HasColumnName("recid");

                    b.Property<string>("TableName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("tablename");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("tenantid");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("updatedate");

                    b.Property<string>("UpdateUsers")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("updateusers");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("userid");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("GymLogs_pkey");

                    b.ToTable("gymlogs", (string)null);
                });

            modelBuilder.Entity("BrewCloud.Gym.Domain.Entities.GymMember", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("address");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("birthdate");

                    b.Property<byte?>("BloopType")
                        .HasColumnType("tinyint")
                        .HasColumnName("blooptype");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("branchid");

                    b.Property<string>("CardNumber")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("cardnumber");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("createdate");

                    b.Property<string>("CreateUsers")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("createusers");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit")
                        .HasColumnName("deleted");

                    b.Property<DateTime>("DeletedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("deleteddate");

                    b.Property<string>("DeletedUsers")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("deletedusers");

                    b.Property<string>("District")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("district");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("email");

                    b.Property<string>("EmergencyPerson")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("emergencyperson");

                    b.Property<string>("EmergencyPersonPhone")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("emergencypersonphone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("firstname");

                    b.Property<byte?>("Gender")
                        .HasColumnType("tinyint")
                        .HasColumnName("gender");

                    b.Property<string>("IdentityNumber")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("identitynumber");

                    b.Property<bool?>("IsMaried")
                        .HasColumnType("bit")
                        .HasColumnName("ismaried");

                    b.Property<bool?>("IsMember")
                        .HasColumnType("bit")
                        .HasColumnName("ismember");

                    b.Property<string>("Job")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("job");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("lastname");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("note");

                    b.Property<Guid?>("PersonnelTrainerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("personneltrainerid");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("phone");

                    b.Property<int>("RecId")
                        .HasColumnType("int")
                        .HasColumnName("recid");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("updatedate");

                    b.Property<string>("UpdateUsers")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("updateusers");

                    b.HasKey("Id")
                        .HasName("GymMember_pkey");

                    b.ToTable("gymmember", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
