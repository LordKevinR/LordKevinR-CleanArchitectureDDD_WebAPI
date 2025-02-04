using CleanArchitecture.Domain.Rentals;
using CleanArchitecture.Domain.Reviews;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations
{
    internal sealed class ReviewContiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("reviews");
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Rating)
                   .HasConversion(rating => rating.Value, value => Rating.Create(value).Value);

            builder.Property(r => r.Comment)
                   .HasMaxLength(500)
                   .HasConversion(c => c.Text, value => new Comment(value));

            builder.HasOne<Vehicle>()
                   .WithMany()
                   .HasForeignKey(r => r.VehicleId);

            builder.HasOne<Rental>()
                   .WithMany()
                   .HasForeignKey(r => r.RentalId);

            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(r => r.UserId);
        }
    }
}