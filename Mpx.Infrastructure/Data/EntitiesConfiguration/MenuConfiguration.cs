
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mpx.Domain.Entities;

namespace Mpx.Infrastructure.Data.EntitiesConfiguration;

public class MenuConfiguration : IEntityTypeConfiguration<Menu>
{
  public void Configure(EntityTypeBuilder<Menu> builder)
  {
    builder.HasKey(b => b.Id);
    builder.Property(b => b.Label).IsRequired().HasMaxLength(25);
    builder.Property(b => b.Application).IsRequired().HasMaxLength(1);
    builder.Property(b => b.System).IsRequired().HasDefaultValue(false);
    builder.Property(b => b.CorporationId).IsRequired();
    builder.Property(b => b.Description).IsRequired(false).HasColumnType("text");

    builder.HasOne(b => b.Corporation)
           .WithMany(b => b.Menus)
           .HasForeignKey(b => b.CorporationId)
           .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(b => b.ParentMenu)
           .WithMany(b => b.SubMenus)
           .HasForeignKey(b => b.MenuId)
           .OnDelete(DeleteBehavior.Restrict);
  }
}
