using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portal.TM.Business.Entities;

#pragma warning disable CS8618

namespace Portal.TM.Data.Context;
public class MyIdentityDbContext : IdentityDbContext<MyUser, IdentityRole<Guid>, Guid>
{
    public MyIdentityDbContext(DbContextOptions<MyIdentityDbContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(e => e.GetProperties()
                         .Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyIdentityDbContext).Assembly);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        
        modelBuilder.Entity<IdentityRole<Guid>>(b =>
        {
            b.ToTable("MyRoles");
        }); 
        
        modelBuilder.Entity<IdentityUserClaim<Guid>>(b =>
        {
            b.ToTable("MyUserClaims");
        });

        modelBuilder.Entity<IdentityUserLogin<Guid>>(b =>
        {
            b.ToTable("MyUserLogins");
        });

        modelBuilder.Entity<IdentityUserToken<Guid>>(b =>
        {
            b.ToTable("MyUserTokens");
        });

        modelBuilder.Entity<IdentityRoleClaim<Guid>>(b =>
        {
            b.ToTable("MyRoleClaims");
        });

        modelBuilder.Entity<IdentityUserRole<Guid>>(b =>
        {
            b.ToTable("MyUserRoles");
        });

    }
}

