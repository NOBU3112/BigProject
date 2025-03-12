using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BigProject.Migrations
{
    /// <inheritdoc />
    public partial class ver1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlAvatar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventLocation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaSV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlAvatar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_documents_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "emailConfirms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Exprired = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    IsActiveAccount = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emailConfirms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_emailConfirms_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "eventJoins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventJoins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_eventJoins_events_EventId",
                        column: x => x.EventId,
                        principalTable: "events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_eventJoins_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "memberInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    religion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlAvatar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PoliticalTheory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfJoining = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlaceOfJoining = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsOutstandingMember = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_memberInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_memberInfos_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "refreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Exprited = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_refreshTokens_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rewardDisciplines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RewardOrDiscipline = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RejectReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipientId = table.Column<int>(type: "int", nullable: false),
                    ProposerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rewardDisciplines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rewardDisciplines_users_ProposerId",
                        column: x => x.ProposerId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_rewardDisciplines_users_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "requestToBeOutStandingMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberInfoId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RejectReason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requestToBeOutStandingMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_requestToBeOutStandingMembers_memberInfos_MemberInfoId",
                        column: x => x.MemberInfoId,
                        principalTable: "memberInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "approvalHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestToBeOutstandingMemberId = table.Column<int>(type: "int", nullable: true),
                    RewardDisciplineId = table.Column<int>(type: "int", nullable: true),
                    ApprovedById = table.Column<int>(type: "int", nullable: false),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HistoryType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAccept = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_approvalHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_approvalHistories_requestToBeOutStandingMembers_RequestToBeOutstandingMemberId",
                        column: x => x.RequestToBeOutstandingMemberId,
                        principalTable: "requestToBeOutStandingMembers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_approvalHistories_rewardDisciplines_RewardDisciplineId",
                        column: x => x.RewardDisciplineId,
                        principalTable: "rewardDisciplines",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_approvalHistories_users_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Đoàn viên" },
                    { 2, "Bí thư đoàn viên" },
                    { 3, "Liên chi đoàn khoa" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Email", "IsActive", "MaSV", "Password", "RoleId", "Username" },
                values: new object[,]
                {
                    { 1, "admin@gmail.com", true, "1111111111", "$2a$12$umDEKg3yORpv174r7kzKxO7Z.BVbw0HDzb44jCsvgjHGGn5rM6/Ky", 3, "admin" },
                    { 2, "member@gmail.com", true, "1111111112", "$2a$12$umDEKg3yORpv174r7kzKxO7Z.BVbw0HDzb44jCsvgjHGGn5rM6/Ky", 1, "member" },
                    { 3, "secretary@gmail.com", true, "1111111113", "$2a$12$umDEKg3yORpv174r7kzKxO7Z.BVbw0HDzb44jCsvgjHGGn5rM6/Ky", 2, "secretary" }
                });

            migrationBuilder.InsertData(
                table: "emailConfirms",
                columns: new[] { "Id", "Code", "CreateTime", "Exprired", "IsActiveAccount", "IsConfirmed", "UserId" },
                values: new object[,]
                {
                    { 1, "123456", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, 1 },
                    { 2, "123456", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, 2 },
                    { 3, "123456", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, 3 }
                });

            migrationBuilder.InsertData(
                table: "memberInfos",
                columns: new[] { "Id", "Birthdate", "Class", "DateOfJoining", "FullName", "IsOutstandingMember", "Nation", "PhoneNumber", "PlaceOfJoining", "PoliticalTheory", "Status", "UrlAvatar", "UserId", "religion" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "string", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "string", false, "string", "string", "string", "string", 1, "string", 1, "string" },
                    { 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "string", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "string", false, "string", "string", "string", "string", 1, "string", 2, "string" },
                    { 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "string", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "string", false, "string", "string", "string", "string", 1, "string", 3, "string" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_approvalHistories_ApprovedById",
                table: "approvalHistories",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_approvalHistories_RequestToBeOutstandingMemberId",
                table: "approvalHistories",
                column: "RequestToBeOutstandingMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_approvalHistories_RewardDisciplineId",
                table: "approvalHistories",
                column: "RewardDisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_documents_UserId",
                table: "documents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_emailConfirms_UserId",
                table: "emailConfirms",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_eventJoins_EventId",
                table: "eventJoins",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_eventJoins_UserId",
                table: "eventJoins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_memberInfos_UserId",
                table: "memberInfos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_refreshTokens_UserId",
                table: "refreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_requestToBeOutStandingMembers_MemberInfoId",
                table: "requestToBeOutStandingMembers",
                column: "MemberInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_rewardDisciplines_ProposerId",
                table: "rewardDisciplines",
                column: "ProposerId");

            migrationBuilder.CreateIndex(
                name: "IX_rewardDisciplines_RecipientId",
                table: "rewardDisciplines",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_users_RoleId",
                table: "users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "approvalHistories");

            migrationBuilder.DropTable(
                name: "documents");

            migrationBuilder.DropTable(
                name: "emailConfirms");

            migrationBuilder.DropTable(
                name: "eventJoins");

            migrationBuilder.DropTable(
                name: "refreshTokens");

            migrationBuilder.DropTable(
                name: "requestToBeOutStandingMembers");

            migrationBuilder.DropTable(
                name: "rewardDisciplines");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "memberInfos");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
