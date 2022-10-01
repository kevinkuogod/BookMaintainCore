using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace bookMaintain.Model.Models
{
    public partial class GSSWEBContext : DbContext
    {
        public GSSWEBContext()
        {
        }

        public GSSWEBContext(DbContextOptions<GSSWEBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<BookClass> BookClasses { get; set; } = null!;
        public virtual DbSet<BookClass1> BookClasses1 { get; set; } = null!;
        public virtual DbSet<BookClassEntity> BookClassEntities { get; set; } = null!;
        public virtual DbSet<BookCode> BookCodes { get; set; } = null!;
        public virtual DbSet<BookData> BookDatas { get; set; } = null!;
        public virtual DbSet<BookDataEntity> BookDataEntities { get; set; } = null!;
        public virtual DbSet<BookDatum> BookData { get; set; } = null!;
        public virtual DbSet<BookLendRecord> BookLendRecords { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<MemberM> MemberMs { get; set; } = null!;
        public virtual DbSet<MigrationHistory> MigrationHistories { get; set; } = null!;
        public virtual DbSet<SpanTable> SpanTables { get; set; } = null!;
        public virtual DbSet<Vocabulary> Vocabularies { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=GSSWEB;Integrated Security=False;User ID=kevin;Password=hurty456;Connect Timeout=15;TrustServerCertificate=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("ADMINS");

                entity.HasIndex(e => e.Id, "ID_UNIP_ADMINS")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .HasColumnName("CREATE_USER");

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.EmailVerifiedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("EMAIL_VERIFIED_AT");

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("datetime")
                    .HasColumnName("MODIFY_DATE");

                entity.Property(e => e.ModifyUser)
                    .HasMaxLength(50)
                    .HasColumnName("MODIFY_USER");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.RememberToken)
                    .HasMaxLength(100)
                    .HasColumnName("REMEMBER_TOKEN");

                entity.Property(e => e.Role).HasColumnName("ROLE");

                entity.Property(e => e.UserCname)
                    .HasMaxLength(50)
                    .HasColumnName("USER_CNAME");

                entity.Property(e => e.UserEname)
                    .HasMaxLength(50)
                    .HasColumnName("USER_ENAME");
            });

            modelBuilder.Entity<BookClass>(entity =>
            {
                entity.HasKey(e => e.BookClassId)
                    .IsClustered(false);

                entity.ToTable("BOOK_CLASS");

                entity.Property(e => e.BookClassId)
                    .HasMaxLength(4)
                    .HasColumnName("BOOK_CLASS_ID");

                entity.Property(e => e.BookClassName)
                    .HasMaxLength(60)
                    .HasColumnName("BOOK_CLASS_NAME");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(12)
                    .HasColumnName("CREATE_USER");

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("datetime")
                    .HasColumnName("MODIFY_DATE");

                entity.Property(e => e.ModifyUser)
                    .HasMaxLength(12)
                    .HasColumnName("MODIFY_USER");
            });

            modelBuilder.Entity<BookClass1>(entity =>
            {
                entity.HasKey(e => e.BookClassId)
                    .HasName("PK_dbo.BookClasses");

                entity.ToTable("BookClasses");

                entity.HasIndex(e => e.BookDataBookId, "IX_BookData_BOOK_ID");

                entity.Property(e => e.BookClassId)
                    .HasMaxLength(128)
                    .HasColumnName("BOOK_CLASS_ID");

                entity.Property(e => e.BookClassName).HasColumnName("BOOK_CLASS_NAME");

                entity.Property(e => e.BookDataBookId).HasColumnName("BookData_BOOK_ID");

                entity.Property(e => e.CreateDate).HasColumnName("CREATE_DATE");

                entity.Property(e => e.CreateUser).HasColumnName("CREATE_USER");

                entity.Property(e => e.ModifyDate).HasColumnName("MODIFY_DATE");

                entity.Property(e => e.ModifyUser).HasColumnName("MODIFY_USER");

                entity.HasOne(d => d.BookDataBook)
                    .WithMany(p => p.BookClass1s)
                    .HasForeignKey(d => d.BookDataBookId)
                    .HasConstraintName("FK_dbo.BookClasses_dbo.BookDatas_BookData_BOOK_ID");
            });

            modelBuilder.Entity<BookClassEntity>(entity =>
            {
                entity.HasKey(e => e.BookClassId)
                    .HasName("PK_dbo.BookClassEntities");

                entity.HasIndex(e => e.BookDataEntityBookId, "IX_BookDataEntity_BOOK_ID");

                entity.Property(e => e.BookClassId)
                    .HasMaxLength(128)
                    .HasColumnName("BOOK_CLASS_ID");

                entity.Property(e => e.BookClassName).HasColumnName("BOOK_CLASS_NAME");

                entity.Property(e => e.BookDataEntityBookId).HasColumnName("BookDataEntity_BOOK_ID");

                entity.Property(e => e.CreateDate).HasColumnName("CREATE_DATE");

                entity.Property(e => e.CreateUser).HasColumnName("CREATE_USER");

                entity.Property(e => e.ModifyDate).HasColumnName("MODIFY_DATE");

                entity.Property(e => e.ModifyUser).HasColumnName("MODIFY_USER");

                entity.HasOne(d => d.BookDataEntityBook)
                    .WithMany(p => p.BookClassEntities)
                    .HasForeignKey(d => d.BookDataEntityBookId)
                    .HasConstraintName("FK_dbo.BookClassEntities_dbo.BookDataEntities_BookDataEntity_BOOK_ID");
            });

            modelBuilder.Entity<BookCode>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("BOOK_CODE");

                entity.Property(e => e.CodeId)
                    .HasMaxLength(100)
                    .HasColumnName("CODE_ID");

                entity.Property(e => e.CodeName)
                    .HasMaxLength(200)
                    .HasColumnName("CODE_NAME");

                entity.Property(e => e.CodeType)
                    .HasMaxLength(50)
                    .HasColumnName("CODE_TYPE");

                entity.Property(e => e.CodeTypeDesc)
                    .HasMaxLength(200)
                    .HasColumnName("CODE_TYPE_DESC");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(10)
                    .HasColumnName("CREATE_USER");

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("datetime")
                    .HasColumnName("MODIFY_DATE");

                entity.Property(e => e.ModifyUser)
                    .HasMaxLength(10)
                    .HasColumnName("MODIFY_USER");
            });

            modelBuilder.Entity<BookData>(entity =>
            {
                entity.HasKey(e => e.BookId)
                    .HasName("PK_dbo.BookDatas");

                entity.Property(e => e.BookId).HasColumnName("BOOK_ID");

                entity.Property(e => e.BookAmount).HasColumnName("BOOK_AMOUNT");

                entity.Property(e => e.BookAuthor).HasColumnName("BOOK_AUTHOR");

                entity.Property(e => e.BookBoughtDate).HasColumnName("BOOK_BOUGHT_DATE");

                entity.Property(e => e.BookClassId).HasColumnName("BOOK_CLASS_ID");

                entity.Property(e => e.BookKeeper).HasColumnName("BOOK_KEEPER");

                entity.Property(e => e.BookName).HasColumnName("BOOK_NAME");

                entity.Property(e => e.BookNote).HasColumnName("BOOK_NOTE");

                entity.Property(e => e.BookPublisher).HasColumnName("BOOK_PUBLISHER");

                entity.Property(e => e.BookStatus).HasColumnName("BOOK_STATUS");

                entity.Property(e => e.CreateDate).HasColumnName("CREATE_DATE");

                entity.Property(e => e.CreateUser).HasColumnName("CREATE_USER");

                entity.Property(e => e.ModifyDate).HasColumnName("MODIFY_DATE");

                entity.Property(e => e.ModifyUser).HasColumnName("MODIFY_USER");
            });

            modelBuilder.Entity<BookDataEntity>(entity =>
            {
                entity.HasKey(e => e.BookId)
                    .HasName("PK_dbo.BookDataEntities");

                entity.Property(e => e.BookId).HasColumnName("BOOK_ID");

                entity.Property(e => e.BookAmount).HasColumnName("BOOK_AMOUNT");

                entity.Property(e => e.BookAuthor).HasColumnName("BOOK_AUTHOR");

                entity.Property(e => e.BookBoughtDate).HasColumnName("BOOK_BOUGHT_DATE");

                entity.Property(e => e.BookClassId).HasColumnName("BOOK_CLASS_ID");

                entity.Property(e => e.BookKeeper).HasColumnName("BOOK_KEEPER");

                entity.Property(e => e.BookName).HasColumnName("BOOK_NAME");

                entity.Property(e => e.BookNote).HasColumnName("BOOK_NOTE");

                entity.Property(e => e.BookPublisher).HasColumnName("BOOK_PUBLISHER");

                entity.Property(e => e.BookStatus).HasColumnName("BOOK_STATUS");

                entity.Property(e => e.CreateDate).HasColumnName("CREATE_DATE");

                entity.Property(e => e.CreateUser).HasColumnName("CREATE_USER");

                entity.Property(e => e.ModifyDate).HasColumnName("MODIFY_DATE");

                entity.Property(e => e.ModifyUser).HasColumnName("MODIFY_USER");
            });

            modelBuilder.Entity<BookDatum>(entity =>
            {
                entity.HasKey(e => e.BookId)
                    .IsClustered(false);

                entity.ToTable("BOOK_DATA");

                entity.Property(e => e.BookId).HasColumnName("BOOK_ID");

                entity.Property(e => e.BookAmount).HasColumnName("BOOK_AMOUNT");

                entity.Property(e => e.BookAuthor)
                    .HasMaxLength(30)
                    .HasColumnName("BOOK_AUTHOR");

                entity.Property(e => e.BookBoughtDate)
                    .HasColumnType("datetime")
                    .HasColumnName("BOOK_BOUGHT_DATE");

                entity.Property(e => e.BookClassId)
                    .HasMaxLength(4)
                    .HasColumnName("BOOK_CLASS_ID");

                entity.Property(e => e.BookKeeper)
                    .HasMaxLength(12)
                    .HasColumnName("BOOK_KEEPER");

                entity.Property(e => e.BookName)
                    .HasMaxLength(200)
                    .HasColumnName("BOOK_NAME");

                entity.Property(e => e.BookNote)
                    .HasMaxLength(1200)
                    .HasColumnName("BOOK_NOTE");

                entity.Property(e => e.BookPublisher)
                    .HasMaxLength(20)
                    .HasColumnName("BOOK_PUBLISHER");

                entity.Property(e => e.BookStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("BOOK_STATUS")
                    .IsFixedLength();

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(12)
                    .HasColumnName("CREATE_USER");

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("datetime")
                    .HasColumnName("MODIFY_DATE");

                entity.Property(e => e.ModifyUser)
                    .HasMaxLength(12)
                    .HasColumnName("MODIFY_USER");
            });

            modelBuilder.Entity<BookLendRecord>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("BOOK_LEND_RECORD");

                entity.Property(e => e.BookId).HasColumnName("BOOK_ID");

                entity.Property(e => e.CreDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CRE_DATE");

                entity.Property(e => e.CreUsr)
                    .HasMaxLength(12)
                    .HasColumnName("CRE_USR");

                entity.Property(e => e.IdentityFiled)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("IDENTITY_FILED");

                entity.Property(e => e.KeeperId)
                    .HasMaxLength(12)
                    .HasColumnName("KEEPER_ID");

                entity.Property(e => e.LendDate)
                    .HasColumnType("datetime")
                    .HasColumnName("LEND_DATE");

                entity.Property(e => e.ModDate)
                    .HasColumnType("datetime")
                    .HasColumnName("MOD_DATE");

                entity.Property(e => e.ModUsr)
                    .HasMaxLength(12)
                    .HasColumnName("MOD_USR");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ChineseName)
                    .HasMaxLength(50)
                    .HasColumnName("Chinese_Name");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_At")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EnglishName)
                    .HasMaxLength(50)
                    .HasColumnName("English_Name");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_At")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("CUSTOMERS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .HasColumnName("CREATE_USER");

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.EmailVerifiedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("EMAIL_VERIFIED_AT");

                entity.Property(e => e.Fax)
                    .HasMaxLength(50)
                    .HasColumnName("FAX");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("FIRST_NAME");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("LAST_NAME");

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("datetime")
                    .HasColumnName("MODIFY_DATE");

                entity.Property(e => e.ModifyUser)
                    .HasMaxLength(50)
                    .HasColumnName("MODIFY_USER");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.RememberToken)
                    .HasMaxLength(100)
                    .HasColumnName("REMEMBER_TOKEN");

                entity.Property(e => e.Role).HasColumnName("ROLE");

                entity.Property(e => e.Salt).HasMaxLength(30);

                entity.Property(e => e.Telephone)
                    .HasMaxLength(50)
                    .HasColumnName("TELEPHONE");
            });

            modelBuilder.Entity<MemberM>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("MEMBER_M");

                entity.Property(e => e.UserId)
                    .HasMaxLength(12)
                    .HasColumnName("USER_ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(12)
                    .HasColumnName("CREATE_USER");

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("datetime")
                    .HasColumnName("MODIFY_DATE");

                entity.Property(e => e.ModifyUser)
                    .HasMaxLength(12)
                    .HasColumnName("MODIFY_USER");

                entity.Property(e => e.UserCname)
                    .HasMaxLength(50)
                    .HasColumnName("USER_CNAME");

                entity.Property(e => e.UserEname)
                    .HasMaxLength(50)
                    .HasColumnName("USER_ENAME");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.ProductVersion).HasMaxLength(32);
            });

            modelBuilder.Entity<SpanTable>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SPAN_TABLE");

                entity.Property(e => e.CreDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CRE_DATE");

                entity.Property(e => e.CreUsr)
                    .HasMaxLength(12)
                    .HasColumnName("CRE_USR");

                entity.Property(e => e.IdentityFiled)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("IDENTITY_FILED");

                entity.Property(e => e.ModDate)
                    .HasColumnType("datetime")
                    .HasColumnName("MOD_DATE");

                entity.Property(e => e.ModUsr)
                    .HasMaxLength(12)
                    .HasColumnName("MOD_USR");

                entity.Property(e => e.Note)
                    .HasMaxLength(12)
                    .HasColumnName("NOTE");

                entity.Property(e => e.SpanEnd)
                    .HasMaxLength(12)
                    .HasColumnName("SPAN_END");

                entity.Property(e => e.SpanStart)
                    .HasMaxLength(12)
                    .HasColumnName("SPAN_START");

                entity.Property(e => e.SpanYear)
                    .HasMaxLength(12)
                    .HasColumnName("SPAN_YEAR");
            });

            modelBuilder.Entity<Vocabulary>(entity =>
            {
                entity.ToTable("Vocabulary");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Analyze)
                    .HasMaxLength(1)
                    .HasColumnName("analyze")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ChineseName)
                    .HasMaxLength(300)
                    .HasColumnName("Chinese_Name");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_At")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Editor)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EnglishName)
                    .HasMaxLength(50)
                    .HasColumnName("English_Name");

                entity.Property(e => e.ExampleSentences)
                    .HasColumnType("text")
                    .HasColumnName("Example_Sentences")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ExampleSentencesTranslation)
                    .HasColumnType("text")
                    .HasColumnName("Example_Sentences_Translation")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ExtraMatters)
                    .HasMaxLength(100)
                    .HasColumnName("Extra_Matters")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ImageId)
                    .HasColumnName("Image_Id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.KenyonAndKnott)
                    .HasMaxLength(100)
                    .HasColumnName("Kenyon_And_Knott")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Language).HasDefaultValueSql("((0))");

                entity.Property(e => e.PartOfSpeech)
                    .HasMaxLength(40)
                    .HasColumnName("Part_Of_Speech")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PartOfSpeechDetial)
                    .HasMaxLength(50)
                    .HasColumnName("Part_Of_Speech_Detial")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PerfectTense)
                    .HasMaxLength(100)
                    .HasColumnName("Perfect_Tense")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ProfessionalField)
                    .HasMaxLength(50)
                    .HasColumnName("Professional_Field")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Provenance)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Remark)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tense)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_At")
                    .HasDefaultValueSql("('')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
