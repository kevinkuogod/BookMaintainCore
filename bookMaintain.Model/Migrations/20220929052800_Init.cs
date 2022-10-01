using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookMaintain.Model.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "__MigrationHistory",
                columns: table => new
                {
                    MigrationId = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ContextKey = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Model = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ProductVersion = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.__MigrationHistory", x => new { x.MigrationId, x.ContextKey });
                });

            /*migrationBuilder.CreateTable(
                name: "ADMINS",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    USER_CNAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    USER_ENAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ROLE = table.Column<int>(type: "int", nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    EMAIL_VERIFIED_AT = table.Column<DateTime>(type: "datetime", nullable: true),
                    PASSWORD = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    REMEMBER_TOKEN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    CREATE_USER = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MODIFY_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    MODIFY_USER = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMINS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BOOK_CLASS",
                columns: table => new
                {
                    BOOK_CLASS_ID = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    BOOK_CLASS_NAME = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    CREATE_USER = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    MODIFY_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    MODIFY_USER = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BOOK_CLASS", x => x.BOOK_CLASS_ID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "BOOK_CODE",
                columns: table => new
                {
                    CODE_TYPE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CODE_ID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CODE_TYPE_DESC = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CODE_NAME = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    CREATE_USER = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    MODIFY_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    MODIFY_USER = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "BOOK_DATA",
                columns: table => new
                {
                    BOOK_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BOOK_NAME = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BOOK_CLASS_ID = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    BOOK_AUTHOR = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    BOOK_BOUGHT_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    BOOK_PUBLISHER = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    BOOK_NOTE = table.Column<string>(type: "nvarchar(1200)", maxLength: 1200, nullable: true),
                    BOOK_STATUS = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    BOOK_KEEPER = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    BOOK_AMOUNT = table.Column<int>(type: "int", nullable: true),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    CREATE_USER = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    MODIFY_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    MODIFY_USER = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BOOK_DATA", x => x.BOOK_ID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "BOOK_LEND_RECORD",
                columns: table => new
                {
                    IDENTITY_FILED = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BOOK_ID = table.Column<int>(type: "int", nullable: false),
                    KEEPER_ID = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    LEND_DATE = table.Column<DateTime>(type: "datetime", nullable: false),
                    CRE_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    CRE_USR = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    MOD_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    MOD_USR = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "BookDataEntities",
                columns: table => new
                {
                    BOOK_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BOOK_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BOOK_CLASS_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BOOK_AUTHOR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BOOK_BOUGHT_DATE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BOOK_PUBLISHER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BOOK_NOTE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BOOK_STATUS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BOOK_KEEPER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BOOK_AMOUNT = table.Column<int>(type: "int", nullable: false),
                    CREATE_DATE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CREATE_USER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MODIFY_DATE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MODIFY_USER = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.BookDataEntities", x => x.BOOK_ID);
                });

            migrationBuilder.CreateTable(
                name: "BookDatas",
                columns: table => new
                {
                    BOOK_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BOOK_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BOOK_CLASS_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BOOK_AUTHOR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BOOK_BOUGHT_DATE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BOOK_PUBLISHER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BOOK_NOTE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BOOK_STATUS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BOOK_KEEPER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BOOK_AMOUNT = table.Column<int>(type: "int", nullable: false),
                    CREATE_DATE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CREATE_USER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MODIFY_DATE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MODIFY_USER = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.BookDatas", x => x.BOOK_ID);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    English_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Chinese_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "('')"),
                    Updated_At = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CUSTOMERS",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIRST_NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LAST_NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ROLE = table.Column<int>(type: "int", nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    EMAIL_VERIFIED_AT = table.Column<DateTime>(type: "datetime", nullable: true),
                    TELEPHONE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FAX = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PASSWORD = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    REMEMBER_TOKEN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    CREATE_USER = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MODIFY_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    MODIFY_USER = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUSTOMERS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MEMBER_M",
                columns: table => new
                {
                    USER_ID = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    USER_CNAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    USER_ENAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    CREATE_USER = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    MODIFY_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    MODIFY_USER = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MEMBER_M", x => x.USER_ID);
                });

            migrationBuilder.CreateTable(
                name: "SPAN_TABLE",
                columns: table => new
                {
                    IDENTITY_FILED = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SPAN_YEAR = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    SPAN_START = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    SPAN_END = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    NOTE = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    CRE_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    CRE_USR = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    MOD_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    MOD_USR = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Vocabulary",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    English_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Chinese_Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Language = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    Image_Id = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    Part_Of_Speech = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, defaultValueSql: "('')"),
                    Part_Of_Speech_Detial = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValueSql: "('')"),
                    Example_Sentences = table.Column<string>(type: "text", nullable: true, defaultValueSql: "('')"),
                    Example_Sentences_Translation = table.Column<string>(type: "text", nullable: true, defaultValueSql: "('')"),
                    Chances = table.Column<int>(type: "int", nullable: false),
                    Provenance = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "('')"),
                    Editor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "('')"),
                    Kenyon_And_Knott = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "('')"),
                    Professional_Field = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValueSql: "('')"),
                    Extra_Matters = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "('')"),
                    Tense = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "('')"),
                    Remark = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "('')"),
                    Perfect_Tense = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "('')"),
                    analyze = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true, defaultValueSql: "((0))"),
                    Created_At = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "('')"),
                    Updated_At = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vocabulary", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BookClassEntities",
                columns: table => new
                {
                    BOOK_CLASS_ID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    BOOK_CLASS_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CREATE_DATE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CREATE_USER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MODIFY_DATE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MODIFY_USER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookDataEntity_BOOK_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.BookClassEntities", x => x.BOOK_CLASS_ID);
                    table.ForeignKey(
                        name: "FK_dbo.BookClassEntities_dbo.BookDataEntities_BookDataEntity_BOOK_ID",
                        column: x => x.BookDataEntity_BOOK_ID,
                        principalTable: "BookDataEntities",
                        principalColumn: "BOOK_ID");
                });

            migrationBuilder.CreateTable(
                name: "BookClasses",
                columns: table => new
                {
                    BOOK_CLASS_ID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    BOOK_CLASS_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CREATE_DATE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CREATE_USER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MODIFY_DATE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MODIFY_USER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookData_BOOK_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.BookClasses", x => x.BOOK_CLASS_ID);
                    table.ForeignKey(
                        name: "FK_dbo.BookClasses_dbo.BookDatas_BookData_BOOK_ID",
                        column: x => x.BookData_BOOK_ID,
                        principalTable: "BookDatas",
                        principalColumn: "BOOK_ID");
                });

            migrationBuilder.CreateIndex(
                name: "ID_UNIP_ADMINS",
                table: "ADMINS",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookDataEntity_BOOK_ID",
                table: "BookClassEntities",
                column: "BookDataEntity_BOOK_ID");

            migrationBuilder.CreateIndex(
                name: "IX_BookData_BOOK_ID",
                table: "BookClasses",
                column: "BookData_BOOK_ID");*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropTable(
                name: "__MigrationHistory");

            migrationBuilder.DropTable(
                name: "ADMINS");

            migrationBuilder.DropTable(
                name: "BOOK_CLASS");

            migrationBuilder.DropTable(
                name: "BOOK_CODE");

            migrationBuilder.DropTable(
                name: "BOOK_DATA");

            migrationBuilder.DropTable(
                name: "BOOK_LEND_RECORD");

            migrationBuilder.DropTable(
                name: "BookClassEntities");

            migrationBuilder.DropTable(
                name: "BookClasses");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "CUSTOMERS");

            migrationBuilder.DropTable(
                name: "MEMBER_M");

            migrationBuilder.DropTable(
                name: "SPAN_TABLE");

            migrationBuilder.DropTable(
                name: "Vocabulary");

            migrationBuilder.DropTable(
                name: "BookDataEntities");

            migrationBuilder.DropTable(
                name: "BookDatas");*/
        }
    }
}
