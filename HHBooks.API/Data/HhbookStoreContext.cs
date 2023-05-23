using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HHBooks.API.Data;

public partial class HhbookStoreContext : IdentityDbContext<ApiUser>
{
    public HhbookStoreContext()
    {
    }

    public HhbookStoreContext(DbContextOptions<HhbookStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorsId).HasName("PK__Authors__CF8F070A64F97240");

            entity.Property(e => e.FirstName).HasMaxLength(60);
            entity.Property(e => e.LastName).HasMaxLength(60);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Books__3214EC07EE120945");

            entity.HasIndex(e => e.Isbn, "UQ__Books__9271CED0BE6BC1ED").IsUnique();

            entity.Property(e => e.Isbn).HasMaxLength(20);
            entity.Property(e => e.Summary).HasMaxLength(60);
            entity.Property(e => e.Title).HasMaxLength(30);

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK_Books_ToTable");
        });

        modelBuilder.Entity<IdentityRole>().HasData(
             new IdentityRole
             {
                 Name ="User",
                 NormalizedName ="USER",
                 Id = "a83529aa-bf63-414d-8d6f-a8dcf25ffc13"
             },
             new IdentityRole
             {
                 Name ="Administrator",
                 NormalizedName ="ADMINISTRATOR",
                 Id = "f1986e71-d5d2-442f-a6c1-a9aa935cc3b2"
             }
            ); 


        var  hasher = new PasswordHasher<ApplicationBuilder>();

        modelBuilder.Entity<ApiUser>().HasData(
             new ApiUser
             {
                 Id = "0e2f0b81-f935-4761-8585-b1cf3e2c6827",
                 Email = "admin@hhbookstore.com",
                 NormalizedEmail ="ADMIN@HHBOOKSTORE.COM",
                 UserName = "admin@hhbookstore.com",
                 NormalizedUserName = "ADMIN@HHBOOKSTORE.COM",
                 FirstName =  "System",
                 LastName = "Admin",
                 PasswordHash = hasher.HashPassword(null, "P@ssword1")


             },
             new ApiUser
             {
                 Id = "333bee64-2460-4a6e-a275-6a67f88a714b",
                 Email = "user@hhbookstore.com",
                 NormalizedEmail = "USER@HHBOOKSTORE.COM",
                 UserName = "user@hhbookstore.com",
                 NormalizedUserName = "USERHH@BOOKSTORE.COM",
                 FirstName = "System",
                 LastName = "User",
                 PasswordHash = hasher.HashPassword(null, "P@ssword1")
             }
            );

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
              new IdentityUserRole<string>
              {
                  RoleId = "a83529aa-bf63-414d-8d6f-a8dcf25ffc13",
                  UserId = "333bee64-2460-4a6e-a275-6a67f88a714b"
              },
               new IdentityUserRole<string>
               {
                   RoleId = "f1986e71-d5d2-442f-a6c1-a9aa935cc3b2",
                   UserId = "0e2f0b81-f935-4761-8585-b1cf3e2c6827"
               }
            );

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
