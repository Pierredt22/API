using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MyContacts.Models
{
    public partial class ContactsContext : DbContext
    {
        public ContactsContext()
        {
        }

        public ContactsContext(DbContextOptions<ContactsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<WebUser> WebUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FkUserId).HasColumnName("FK_UserId");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MobileNumber)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkUser)
                    .WithMany(p => p.Contact)
                    .HasForeignKey(d => d.FkUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Id");
            });

            modelBuilder.Entity<WebUser>(entity =>
            {
                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
