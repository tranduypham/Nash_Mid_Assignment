using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mid_Assignment_Project.Migrations
{
    public partial class create_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookBorrowingRequests",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<byte>(type: "tinyint", nullable: false),
                    created_at = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValue: new DateTime(2022, 3, 18, 9, 6, 30, 522, DateTimeKind.Local).AddTicks(1699)),
                    AuthorizeBy = table.Column<int>(type: "int", nullable: false),
                    authprized_at = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookBorrowingRequests", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, defaultValue: "no name"),
                    created_at = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValue: new DateTime(2022, 3, 18, 9, 6, 30, 521, DateTimeKind.Local).AddTicks(2992)),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    isAdmin = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    created_at = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValue: new DateTime(2022, 3, 18, 9, 6, 30, 521, DateTimeKind.Local).AddTicks(82))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "no name"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValue: new DateTime(2022, 3, 18, 9, 6, 30, 521, DateTimeKind.Local).AddTicks(5407)),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.id);
                    table.ForeignKey(
                        name: "FK_Books_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    payload = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 3, 18, 9, 6, 30, 523, DateTimeKind.Local).AddTicks(973))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.id);
                    table.ForeignKey(
                        name: "FK_Tokens_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookBorrowingRequestDetails",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookBorrowingRequestId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, defaultValue: "no-name")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookBorrowingRequestDetails", x => x.id);
                    table.ForeignKey(
                        name: "FK_BookBorrowingRequestDetails_BookBorrowingRequests_BookBorrowingRequestId",
                        column: x => x.BookBorrowingRequestId,
                        principalTable: "BookBorrowingRequests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookBorrowingRequestDetails_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BookBorrowingRequests",
                columns: new[] { "id", "AuthorizeBy", "authprized_at", "created_at", "State", "UserId", "Username" },
                values: new object[,]
                {
                    { 1, 0, null, new DateTime(2022, 3, 18, 9, 6, 30, 523, DateTimeKind.Local).AddTicks(6080), (byte)0, 2, "user1" },
                    { 2, 0, null, new DateTime(2022, 3, 18, 9, 6, 30, 523, DateTimeKind.Local).AddTicks(6082), (byte)0, 3, "user2" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "id", "name", "created_at", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Science", new DateTime(2022, 3, 18, 9, 6, 30, 523, DateTimeKind.Local).AddTicks(6024), null },
                    { 2, "History", new DateTime(2022, 3, 18, 9, 6, 30, 523, DateTimeKind.Local).AddTicks(6030), null },
                    { 3, "Literature", new DateTime(2022, 3, 18, 9, 6, 30, 523, DateTimeKind.Local).AddTicks(6031), null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "created_at", "isAdmin", "password", "username" },
                values: new object[] { 1, new DateTime(2022, 3, 18, 9, 6, 30, 523, DateTimeKind.Local).AddTicks(5818), true, "123456789", "admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "created_at", "password", "username" },
                values: new object[,]
                {
                    { 2, new DateTime(2022, 3, 18, 9, 6, 30, 523, DateTimeKind.Local).AddTicks(5826), "123456789", "user1" },
                    { 3, new DateTime(2022, 3, 18, 9, 6, 30, 523, DateTimeKind.Local).AddTicks(5828), "123456789", "user2" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "id", "name", "CategoryId", "created_at", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Campell Biology", 1, new DateTime(2022, 3, 18, 9, 6, 30, 523, DateTimeKind.Local).AddTicks(6050), null },
                    { 2, "Time and space", 1, new DateTime(2022, 3, 18, 9, 6, 30, 523, DateTimeKind.Local).AddTicks(6055), null },
                    { 3, "WW1", 2, new DateTime(2022, 3, 18, 9, 6, 30, 523, DateTimeKind.Local).AddTicks(6057), null },
                    { 4, "Vietnam and the communitism", 2, new DateTime(2022, 3, 18, 9, 6, 30, 523, DateTimeKind.Local).AddTicks(6057), null },
                    { 5, "The phantom of the opera", 3, new DateTime(2022, 3, 18, 9, 6, 30, 523, DateTimeKind.Local).AddTicks(6058), null },
                    { 6, "Kieu", 3, new DateTime(2022, 3, 18, 9, 6, 30, 523, DateTimeKind.Local).AddTicks(6059), null }
                });

            migrationBuilder.InsertData(
                table: "BookBorrowingRequestDetails",
                columns: new[] { "id", "BookBorrowingRequestId", "BookId", "name" },
                values: new object[,]
                {
                    { 1, 1, 1, "Campell Biology Edit" },
                    { 2, 1, 2, "Time and space" },
                    { 3, 2, 2, "Time and space" },
                    { 4, 2, 3, "WW1" },
                    { 5, 2, 4, "Vietnam and the communitism" },
                    { 6, 2, 5, "The phantom of the opera" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookBorrowingRequestDetails_BookBorrowingRequestId",
                table: "BookBorrowingRequestDetails",
                column: "BookBorrowingRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_BookBorrowingRequestDetails_BookId",
                table: "BookBorrowingRequestDetails",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_user_id",
                table: "Tokens",
                column: "user_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookBorrowingRequestDetails");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "BookBorrowingRequests");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
