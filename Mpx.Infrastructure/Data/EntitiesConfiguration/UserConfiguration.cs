using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mpx.Domain.Entities;

namespace Mpx.Infrastructure.Data.EntitiesConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.ToTable("User");
    builder.HasKey(b => b.Id);
    builder.Property(b => b.Name).IsRequired().HasMaxLength(150);
    builder.Property(b => b.Email).IsRequired().HasMaxLength(150);
    builder.Property(b => b.EmailConfirmed).IsRequired().HasDefaultValue(false);
    builder.Property(b => b.PhoneConfirmed).IsRequired().HasDefaultValue(false);
    builder.Property(b => b.TermsConfirmed).IsRequired().HasDefaultValue(false);
    builder.Property(b => b.UserStatusId).IsRequired();
    builder.Property(b => b.CorporationId).IsRequired();

    builder.HasOne(b => b.Corporation)
           .WithMany(b => b.Users)
           .HasForeignKey(b => b.CorporationId)
           .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(b => b.UserStatus)
           .WithMany(b => b.Users)
           .HasForeignKey(b => b.UserStatusId)
           .OnDelete(DeleteBehavior.Restrict);
  }
}
