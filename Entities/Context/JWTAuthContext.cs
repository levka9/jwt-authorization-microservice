using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using JWT_Auth.Microservice.Entities;

namespace JWT_Auth.Microservice.Entities.Context
{
    public partial class JWTAuthContext : DbContext
    {
        public JWTAuthContext()
        {
        }

        public JWTAuthContext(DbContextOptions<JWTAuthContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<SecurityType> SecurityType { get; set; }
        public virtual DbSet<Token> Token { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserEmail> UserEmail { get; set; }
        public virtual DbSet<UserField> UserField { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserUserRole> UserUserRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-I8BM850\\SQLEXPRESS;AttachDbFilename=F:\\Microservices\\JWT-Auth.Microservice\\LocalDB\\jwt_auth.mdf;Integrated Security=True;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>(entity =>
            {
                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SecurityType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Ip)
                    .HasColumnName("IP")
                    .HasMaxLength(50);

                entity.Property(e => e.SystemName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TokenKey).IsRequired();

                entity.HasOne(d => d.SecurityType)
                    .WithMany(p => p.Token)
                    .HasForeignKey(d => d.SecurityTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Token_SecurityType");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Token)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Token_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Birthdate).HasColumnType("date");

                entity.Property(e => e.Comments).HasMaxLength(200);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.FirstName).HasMaxLength(20);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastName).HasMaxLength(20);

                entity.Property(e => e.LastPasswordChangedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MiddleName).HasMaxLength(20);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PassportIdentity)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PicturePath)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Application");
            });

            modelBuilder.Entity<UserEmail>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(50);
            });

            modelBuilder.Entity<UserField>(entity =>
            {
                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FieldData).IsRequired();

                entity.Property(e => e.FieldName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FieldType)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasComment("int, string, bool etc.");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserField)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserField_User");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserUserRole>(entity =>
            {
                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserUserRole)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserUserRole_User");

                entity.HasOne(d => d.UserRole)
                    .WithMany(p => p.UserUserRole)
                    .HasForeignKey(d => d.UserRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserUserRole_UserRole");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
