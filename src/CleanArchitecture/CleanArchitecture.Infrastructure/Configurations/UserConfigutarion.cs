using CleanArchitecture.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations
{
    internal sealed class UserConfigutarion : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                   .HasMaxLength(200)
                   .HasConversion(name => name!.Value, value => new Name(value));

            builder.Property(u => u.LastName)
                   .HasMaxLength(200)
                   .HasConversion(lastName => lastName!.Value, value => new LastName(value));

            builder.Property(u => u.Email)
                   .HasMaxLength(400)
                   .HasConversion(email => email!.Value, value => new Domain.Users.Email(value));

            builder.HasIndex(u => u.Email)
                   .IsUnique();
        }
    }
}