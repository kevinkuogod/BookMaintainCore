﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using bookMaintain.Model.Models;

#nullable disable

namespace bookMaintain.Model.Migrations
{
    [DbContext(typeof(GSSWEBContext))]
    partial class GSSWEBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("bookMaintain.Model.Models.Admin", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnName("ID");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime")
                        .HasColumnName("CREATE_DATE");

                    b.Property<string>("CreateUser")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("CREATE_USER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("EMAIL");

                    b.Property<DateTime?>("EmailVerifiedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("EMAIL_VERIFIED_AT");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime")
                        .HasColumnName("MODIFY_DATE");

                    b.Property<string>("ModifyUser")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("MODIFY_USER");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("PASSWORD");

                    b.Property<string>("RememberToken")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("REMEMBER_TOKEN");

                    b.Property<int?>("Role")
                        .HasColumnType("int")
                        .HasColumnName("ROLE");

                    b.Property<string>("UserCname")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("USER_CNAME");

                    b.Property<string>("UserEname")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("USER_ENAME");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Id" }, "ID_UNIP_ADMINS")
                        .IsUnique();

                    b.ToTable("ADMINS", (string)null);
                });

            modelBuilder.Entity("bookMaintain.Model.Models.BookClass", b =>
                {
                    b.Property<string>("BookClassId")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)")
                        .HasColumnName("BOOK_CLASS_ID");

                    b.Property<string>("BookClassName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)")
                        .HasColumnName("BOOK_CLASS_NAME");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime")
                        .HasColumnName("CREATE_DATE");

                    b.Property<string>("CreateUser")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)")
                        .HasColumnName("CREATE_USER");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime")
                        .HasColumnName("MODIFY_DATE");

                    b.Property<string>("ModifyUser")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)")
                        .HasColumnName("MODIFY_USER");

                    b.HasKey("BookClassId");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("BookClassId"), false);

                    b.ToTable("BOOK_CLASS", (string)null);
                });

            modelBuilder.Entity("bookMaintain.Model.Models.BookClass1", b =>
                {
                    b.Property<string>("BookClassId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("BOOK_CLASS_ID");

                    b.Property<string>("BookClassName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("BOOK_CLASS_NAME");

                    b.Property<int?>("BookDataBookId")
                        .HasColumnType("int")
                        .HasColumnName("BookData_BOOK_ID");

                    b.Property<string>("CreateDate")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CREATE_DATE");

                    b.Property<string>("CreateUser")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CREATE_USER");

                    b.Property<string>("ModifyDate")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("MODIFY_DATE");

                    b.Property<string>("ModifyUser")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("MODIFY_USER");

                    b.HasKey("BookClassId")
                        .HasName("PK_dbo.BookClasses");

                    b.HasIndex(new[] { "BookDataBookId" }, "IX_BookData_BOOK_ID");

                    b.ToTable("BookClasses", (string)null);
                });

            modelBuilder.Entity("bookMaintain.Model.Models.BookClassEntity", b =>
                {
                    b.Property<string>("BookClassId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("BOOK_CLASS_ID");

                    b.Property<string>("BookClassName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("BOOK_CLASS_NAME");

                    b.Property<int?>("BookDataEntityBookId")
                        .HasColumnType("int")
                        .HasColumnName("BookDataEntity_BOOK_ID");

                    b.Property<string>("CreateDate")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CREATE_DATE");

                    b.Property<string>("CreateUser")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CREATE_USER");

                    b.Property<string>("ModifyDate")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("MODIFY_DATE");

                    b.Property<string>("ModifyUser")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("MODIFY_USER");

                    b.HasKey("BookClassId")
                        .HasName("PK_dbo.BookClassEntities");

                    b.HasIndex(new[] { "BookDataEntityBookId" }, "IX_BookDataEntity_BOOK_ID");

                    b.ToTable("BookClassEntities");
                });

            modelBuilder.Entity("bookMaintain.Model.Models.BookCode", b =>
                {
                    b.Property<string>("CodeId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("CODE_ID");

                    b.Property<string>("CodeName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("CODE_NAME");

                    b.Property<string>("CodeType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("CODE_TYPE");

                    b.Property<string>("CodeTypeDesc")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("CODE_TYPE_DESC");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime")
                        .HasColumnName("CREATE_DATE");

                    b.Property<string>("CreateUser")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("CREATE_USER");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime")
                        .HasColumnName("MODIFY_DATE");

                    b.Property<string>("ModifyUser")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("MODIFY_USER");

                    b.ToTable("BOOK_CODE", (string)null);
                });

            modelBuilder.Entity("bookMaintain.Model.Models.BookData", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("BOOK_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"), 1L, 1);

                    b.Property<int>("BookAmount")
                        .HasColumnType("int")
                        .HasColumnName("BOOK_AMOUNT");

                    b.Property<string>("BookAuthor")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("BOOK_AUTHOR");

                    b.Property<string>("BookBoughtDate")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("BOOK_BOUGHT_DATE");

                    b.Property<string>("BookClassId")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("BOOK_CLASS_ID");

                    b.Property<string>("BookKeeper")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("BOOK_KEEPER");

                    b.Property<string>("BookName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("BOOK_NAME");

                    b.Property<string>("BookNote")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("BOOK_NOTE");

                    b.Property<string>("BookPublisher")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("BOOK_PUBLISHER");

                    b.Property<string>("BookStatus")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("BOOK_STATUS");

                    b.Property<string>("CreateDate")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CREATE_DATE");

                    b.Property<string>("CreateUser")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CREATE_USER");

                    b.Property<string>("ModifyDate")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("MODIFY_DATE");

                    b.Property<string>("ModifyUser")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("MODIFY_USER");

                    b.HasKey("BookId")
                        .HasName("PK_dbo.BookDatas");

                    b.ToTable("BookDatas");
                });

            modelBuilder.Entity("bookMaintain.Model.Models.BookDataEntity", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("BOOK_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"), 1L, 1);

                    b.Property<int>("BookAmount")
                        .HasColumnType("int")
                        .HasColumnName("BOOK_AMOUNT");

                    b.Property<string>("BookAuthor")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("BOOK_AUTHOR");

                    b.Property<string>("BookBoughtDate")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("BOOK_BOUGHT_DATE");

                    b.Property<string>("BookClassId")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("BOOK_CLASS_ID");

                    b.Property<string>("BookKeeper")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("BOOK_KEEPER");

                    b.Property<string>("BookName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("BOOK_NAME");

                    b.Property<string>("BookNote")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("BOOK_NOTE");

                    b.Property<string>("BookPublisher")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("BOOK_PUBLISHER");

                    b.Property<string>("BookStatus")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("BOOK_STATUS");

                    b.Property<string>("CreateDate")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CREATE_DATE");

                    b.Property<string>("CreateUser")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CREATE_USER");

                    b.Property<string>("ModifyDate")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("MODIFY_DATE");

                    b.Property<string>("ModifyUser")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("MODIFY_USER");

                    b.HasKey("BookId")
                        .HasName("PK_dbo.BookDataEntities");

                    b.ToTable("BookDataEntities");
                });

            modelBuilder.Entity("bookMaintain.Model.Models.BookDatum", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("BOOK_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"), 1L, 1);

                    b.Property<int?>("BookAmount")
                        .HasColumnType("int")
                        .HasColumnName("BOOK_AMOUNT");

                    b.Property<string>("BookAuthor")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("BOOK_AUTHOR");

                    b.Property<DateTime?>("BookBoughtDate")
                        .HasColumnType("datetime")
                        .HasColumnName("BOOK_BOUGHT_DATE");

                    b.Property<string>("BookClassId")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)")
                        .HasColumnName("BOOK_CLASS_ID");

                    b.Property<string>("BookKeeper")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)")
                        .HasColumnName("BOOK_KEEPER");

                    b.Property<string>("BookName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("BOOK_NAME");

                    b.Property<string>("BookNote")
                        .HasMaxLength(1200)
                        .HasColumnType("nvarchar(1200)")
                        .HasColumnName("BOOK_NOTE");

                    b.Property<string>("BookPublisher")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("BOOK_PUBLISHER");

                    b.Property<string>("BookStatus")
                        .IsRequired()
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .HasColumnName("BOOK_STATUS")
                        .IsFixedLength();

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime")
                        .HasColumnName("CREATE_DATE");

                    b.Property<string>("CreateUser")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)")
                        .HasColumnName("CREATE_USER");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime")
                        .HasColumnName("MODIFY_DATE");

                    b.Property<string>("ModifyUser")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)")
                        .HasColumnName("MODIFY_USER");

                    b.HasKey("BookId");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("BookId"), false);

                    b.ToTable("BOOK_DATA", (string)null);
                });

            modelBuilder.Entity("bookMaintain.Model.Models.BookLendRecord", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("BOOK_ID");

                    b.Property<DateTime?>("CreDate")
                        .HasColumnType("datetime")
                        .HasColumnName("CRE_DATE");

                    b.Property<string>("CreUsr")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)")
                        .HasColumnName("CRE_USR");

                    b.Property<int>("IdentityFiled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("IDENTITY_FILED");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdentityFiled"), 1L, 1);

                    b.Property<string>("KeeperId")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)")
                        .HasColumnName("KEEPER_ID");

                    b.Property<DateTime>("LendDate")
                        .HasColumnType("datetime")
                        .HasColumnName("LEND_DATE");

                    b.Property<DateTime?>("ModDate")
                        .HasColumnType("datetime")
                        .HasColumnName("MOD_DATE");

                    b.Property<string>("ModUsr")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)")
                        .HasColumnName("MOD_USR");

                    b.ToTable("BOOK_LEND_RECORD", (string)null);
                });

            modelBuilder.Entity("bookMaintain.Model.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ChineseName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Chinese_Name");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Created_At")
                        .HasDefaultValueSql("('')");

                    b.Property<string>("EnglishName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("English_Name");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_At")
                        .HasDefaultValueSql("('')");

                    b.HasKey("Id");

                    b.ToTable("Country", (string)null);
                });

            modelBuilder.Entity("bookMaintain.Model.Models.Customer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime")
                        .HasColumnName("CREATE_DATE");

                    b.Property<string>("CreateUser")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("CREATE_USER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("EMAIL");

                    b.Property<DateTime?>("EmailVerifiedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("EMAIL_VERIFIED_AT");

                    b.Property<string>("Fax")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("FAX");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("FIRST_NAME");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("LAST_NAME");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime")
                        .HasColumnName("MODIFY_DATE");

                    b.Property<string>("ModifyUser")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("MODIFY_USER");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("PASSWORD");

                    b.Property<string>("RememberToken")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("REMEMBER_TOKEN");

                    b.Property<int?>("Role")
                        .HasColumnType("int")
                        .HasColumnName("ROLE");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Telephone")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("TELEPHONE");

                    b.HasKey("Id");

                    b.ToTable("CUSTOMERS", (string)null);
                });

            modelBuilder.Entity("bookMaintain.Model.Models.MemberM", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)")
                        .HasColumnName("USER_ID");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime")
                        .HasColumnName("CREATE_DATE");

                    b.Property<string>("CreateUser")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)")
                        .HasColumnName("CREATE_USER");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime")
                        .HasColumnName("MODIFY_DATE");

                    b.Property<string>("ModifyUser")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)")
                        .HasColumnName("MODIFY_USER");

                    b.Property<string>("UserCname")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("USER_CNAME");

                    b.Property<string>("UserEname")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("USER_ENAME");

                    b.HasKey("UserId");

                    b.ToTable("MEMBER_M", (string)null);
                });

            modelBuilder.Entity("bookMaintain.Model.Models.MigrationHistory", b =>
                {
                    b.Property<string>("MigrationId")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("ContextKey")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<byte[]>("Model")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ProductVersion")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("MigrationId", "ContextKey")
                        .HasName("PK_dbo.__MigrationHistory");

                    b.ToTable("__MigrationHistory", (string)null);
                });

            modelBuilder.Entity("bookMaintain.Model.Models.SpanTable", b =>
                {
                    b.Property<DateTime?>("CreDate")
                        .HasColumnType("datetime")
                        .HasColumnName("CRE_DATE");

                    b.Property<string>("CreUsr")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)")
                        .HasColumnName("CRE_USR");

                    b.Property<int>("IdentityFiled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("IDENTITY_FILED");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdentityFiled"), 1L, 1);

                    b.Property<DateTime?>("ModDate")
                        .HasColumnType("datetime")
                        .HasColumnName("MOD_DATE");

                    b.Property<string>("ModUsr")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)")
                        .HasColumnName("MOD_USR");

                    b.Property<string>("Note")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)")
                        .HasColumnName("NOTE");

                    b.Property<string>("SpanEnd")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)")
                        .HasColumnName("SPAN_END");

                    b.Property<string>("SpanStart")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)")
                        .HasColumnName("SPAN_START");

                    b.Property<string>("SpanYear")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)")
                        .HasColumnName("SPAN_YEAR");

                    b.ToTable("SPAN_TABLE", (string)null);
                });

            modelBuilder.Entity("bookMaintain.Model.Models.Vocabulary", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Analyze")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)")
                        .HasColumnName("analyze")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("Chances")
                        .HasColumnType("int");

                    b.Property<string>("ChineseName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)")
                        .HasColumnName("Chinese_Name");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Created_At")
                        .HasDefaultValueSql("('')");

                    b.Property<string>("Editor")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasDefaultValueSql("('')");

                    b.Property<string>("EnglishName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("English_Name");

                    b.Property<string>("ExampleSentences")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasColumnName("Example_Sentences")
                        .HasDefaultValueSql("('')");

                    b.Property<string>("ExampleSentencesTranslation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasColumnName("Example_Sentences_Translation")
                        .HasDefaultValueSql("('')");

                    b.Property<string>("ExtraMatters")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Extra_Matters")
                        .HasDefaultValueSql("('')");

                    b.Property<int?>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Image_Id")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("KenyonAndKnott")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Kenyon_And_Knott")
                        .HasDefaultValueSql("('')");

                    b.Property<int?>("Language")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("PartOfSpeech")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)")
                        .HasColumnName("Part_Of_Speech")
                        .HasDefaultValueSql("('')");

                    b.Property<string>("PartOfSpeechDetial")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Part_Of_Speech_Detial")
                        .HasDefaultValueSql("('')");

                    b.Property<string>("PerfectTense")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Perfect_Tense")
                        .HasDefaultValueSql("('')");

                    b.Property<string>("ProfessionalField")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Professional_Field")
                        .HasDefaultValueSql("('')");

                    b.Property<string>("Provenance")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasDefaultValueSql("('')");

                    b.Property<string>("Remark")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasDefaultValueSql("('')");

                    b.Property<string>("Tense")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasDefaultValueSql("('')");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_At")
                        .HasDefaultValueSql("('')");

                    b.HasKey("Id");

                    b.ToTable("Vocabulary", (string)null);
                });

            modelBuilder.Entity("bookMaintain.Model.Models.BookClass1", b =>
                {
                    b.HasOne("bookMaintain.Model.Models.BookData", "BookDataBook")
                        .WithMany("BookClass1s")
                        .HasForeignKey("BookDataBookId")
                        .HasConstraintName("FK_dbo.BookClasses_dbo.BookDatas_BookData_BOOK_ID");

                    b.Navigation("BookDataBook");
                });

            modelBuilder.Entity("bookMaintain.Model.Models.BookClassEntity", b =>
                {
                    b.HasOne("bookMaintain.Model.Models.BookDataEntity", "BookDataEntityBook")
                        .WithMany("BookClassEntities")
                        .HasForeignKey("BookDataEntityBookId")
                        .HasConstraintName("FK_dbo.BookClassEntities_dbo.BookDataEntities_BookDataEntity_BOOK_ID");

                    b.Navigation("BookDataEntityBook");
                });

            modelBuilder.Entity("bookMaintain.Model.Models.BookData", b =>
                {
                    b.Navigation("BookClass1s");
                });

            modelBuilder.Entity("bookMaintain.Model.Models.BookDataEntity", b =>
                {
                    b.Navigation("BookClassEntities");
                });
#pragma warning restore 612, 618
        }
    }
}
