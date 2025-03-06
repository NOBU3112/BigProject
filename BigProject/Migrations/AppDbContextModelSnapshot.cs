﻿// <auto-generated />
using System;
using BigProject.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BigProject.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BigProject.Entities.ApprovalHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ApprovedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("ApprovedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAccept")
                        .HasColumnType("bit");

                    b.Property<string>("RejectReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RequestToBeOutstandingMemberId")
                        .HasColumnType("int");

                    b.Property<int?>("RewardDisciplineId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApprovedById");

                    b.HasIndex("RequestToBeOutstandingMemberId");

                    b.HasIndex("RewardDisciplineId");

                    b.ToTable("approvalHistories");
                });

            modelBuilder.Entity("BigProject.Entities.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DocumentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("FileData")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("documents");
                });

            modelBuilder.Entity("BigProject.Entities.EmailConfirm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Exprired")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActiveAccount")
                        .HasColumnType("bit");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("emailConfirms");
                });

            modelBuilder.Entity("BigProject.Entities.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EventEndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EventLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EventStartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("EventTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventTypeId");

                    b.ToTable("events");
                });

            modelBuilder.Entity("BigProject.Entities.EventJoin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("eventJoins");
                });

            modelBuilder.Entity("BigProject.Entities.EventType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EventTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("eventTypes");
                });

            modelBuilder.Entity("BigProject.Entities.MemberInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Class")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfJoining")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsOutstandingMember")
                        .HasColumnType("bit");

                    b.Property<string>("MemberId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlaceOfJoining")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PoliticalTheory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UrlAvatar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("religion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("memberInfos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Birthdate = new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Class = "string",
                            DateOfJoining = new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FullName = "string",
                            IsOutstandingMember = false,
                            MemberId = "string",
                            Nation = "string",
                            PhoneNumber = "string",
                            PlaceOfJoining = "string",
                            PoliticalTheory = "string",
                            Status = 1,
                            UrlAvatar = "string",
                            UserId = 1,
                            religion = "string"
                        },
                        new
                        {
                            Id = 2,
                            Birthdate = new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Class = "string",
                            DateOfJoining = new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FullName = "string",
                            IsOutstandingMember = false,
                            MemberId = "string",
                            Nation = "string",
                            PhoneNumber = "string",
                            PlaceOfJoining = "string",
                            PoliticalTheory = "string",
                            Status = 1,
                            UrlAvatar = "string",
                            UserId = 2,
                            religion = "string"
                        },
                        new
                        {
                            Id = 3,
                            Birthdate = new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Class = "string",
                            DateOfJoining = new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FullName = "string",
                            IsOutstandingMember = false,
                            MemberId = "string",
                            Nation = "string",
                            PhoneNumber = "string",
                            PlaceOfJoining = "string",
                            PoliticalTheory = "string",
                            Status = 1,
                            UrlAvatar = "string",
                            UserId = 3,
                            religion = "string"
                        });
                });

            modelBuilder.Entity("BigProject.Entities.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Exprited")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("refreshTokens");
                });

            modelBuilder.Entity("BigProject.Entities.RequestToBeOutStandingMember", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MemberInfoId")
                        .HasColumnType("int");

                    b.Property<string>("RejectReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MemberInfoId");

                    b.ToTable("requestToBeOutStandingMembers");
                });

            modelBuilder.Entity("BigProject.Entities.RewardDiscipline", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProposerId")
                        .HasColumnType("int");

                    b.Property<int>("RecipientId")
                        .HasColumnType("int");

                    b.Property<int>("RewardDisciplineTypeId")
                        .HasColumnType("int");

                    b.Property<bool>("RewardOrDiscipline")
                        .HasColumnType("bit");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProposerId");

                    b.HasIndex("RecipientId");

                    b.HasIndex("RewardDisciplineTypeId");

                    b.ToTable("rewardDisciplines");
                });

            modelBuilder.Entity("BigProject.Entities.RewardDisciplineType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("RewardDisciplineTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("RewardOrDiscipline")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("rewardDisciplineTypes");
                });

            modelBuilder.Entity("BigProject.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Đoàn viên"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Bí thư đoàn viên"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Liên chi đoàn khoa"
                        });
                });

            modelBuilder.Entity("BigProject.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("MaTV")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@gmail.com",
                            IsActive = true,
                            MaTV = "1111111111",
                            Password = "$2a$12$umDEKg3yORpv174r7kzKxO7Z.BVbw0HDzb44jCsvgjHGGn5rM6/Ky",
                            RoleId = 3,
                            Username = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Email = "member@gmail.com",
                            IsActive = true,
                            MaTV = "1111111111",
                            Password = "$2a$12$umDEKg3yORpv174r7kzKxO7Z.BVbw0HDzb44jCsvgjHGGn5rM6/Ky",
                            RoleId = 1,
                            Username = "member"
                        },
                        new
                        {
                            Id = 3,
                            Email = "secretary@gmail.com",
                            IsActive = true,
                            MaTV = "1111111111",
                            Password = "$2a$12$umDEKg3yORpv174r7kzKxO7Z.BVbw0HDzb44jCsvgjHGGn5rM6/Ky",
                            RoleId = 2,
                            Username = "secretary"
                        });
                });

            modelBuilder.Entity("BigProject.Entities.ApprovalHistory", b =>
                {
                    b.HasOne("BigProject.Entities.User", "ApprovedBy")
                        .WithMany()
                        .HasForeignKey("ApprovedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BigProject.Entities.RequestToBeOutStandingMember", "RequestToBeOutstandingMember")
                        .WithMany()
                        .HasForeignKey("RequestToBeOutstandingMemberId");

                    b.HasOne("BigProject.Entities.RewardDiscipline", "RewardDiscipline")
                        .WithMany()
                        .HasForeignKey("RewardDisciplineId");

                    b.Navigation("ApprovedBy");

                    b.Navigation("RequestToBeOutstandingMember");

                    b.Navigation("RewardDiscipline");
                });

            modelBuilder.Entity("BigProject.Entities.Document", b =>
                {
                    b.HasOne("BigProject.Entities.User", "User")
                        .WithMany("documents")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BigProject.Entities.EmailConfirm", b =>
                {
                    b.HasOne("BigProject.Entities.User", "user")
                        .WithMany("emailConfirms")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("BigProject.Entities.Event", b =>
                {
                    b.HasOne("BigProject.Entities.EventType", "EventType")
                        .WithMany()
                        .HasForeignKey("EventTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventType");
                });

            modelBuilder.Entity("BigProject.Entities.EventJoin", b =>
                {
                    b.HasOne("BigProject.Entities.Event", "Event")
                        .WithMany("eventJoints")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BigProject.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BigProject.Entities.MemberInfo", b =>
                {
                    b.HasOne("BigProject.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BigProject.Entities.RefreshToken", b =>
                {
                    b.HasOne("BigProject.Entities.User", "User")
                        .WithMany("refreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BigProject.Entities.RequestToBeOutStandingMember", b =>
                {
                    b.HasOne("BigProject.Entities.MemberInfo", "MemberInfo")
                        .WithMany("requestToBeOutStandingMembers")
                        .HasForeignKey("MemberInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MemberInfo");
                });

            modelBuilder.Entity("BigProject.Entities.RewardDiscipline", b =>
                {
                    b.HasOne("BigProject.Entities.User", "Proposer")
                        .WithMany()
                        .HasForeignKey("ProposerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BigProject.Entities.User", "Recipient")
                        .WithMany()
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BigProject.Entities.RewardDisciplineType", "RewardDisciplineType")
                        .WithMany()
                        .HasForeignKey("RewardDisciplineTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Proposer");

                    b.Navigation("Recipient");

                    b.Navigation("RewardDisciplineType");
                });

            modelBuilder.Entity("BigProject.Entities.User", b =>
                {
                    b.HasOne("BigProject.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("BigProject.Entities.Event", b =>
                {
                    b.Navigation("eventJoints");
                });

            modelBuilder.Entity("BigProject.Entities.MemberInfo", b =>
                {
                    b.Navigation("requestToBeOutStandingMembers");
                });

            modelBuilder.Entity("BigProject.Entities.User", b =>
                {
                    b.Navigation("documents");

                    b.Navigation("emailConfirms");

                    b.Navigation("refreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
