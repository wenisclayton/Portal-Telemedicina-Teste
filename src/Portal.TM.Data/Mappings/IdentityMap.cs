using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.TM.Business.Entities;

namespace Portal.TM.Data.Mappings;

public class IdentityMap : IEntityTypeConfiguration<MyUser>
{
    public void Configure(EntityTypeBuilder<MyUser> builder)
    {
        builder.Property(us => us.FullName)
            .IsRequired()
            .HasColumnType("varchar(250)");

        builder.ToTable("MyUser");
    }
}


