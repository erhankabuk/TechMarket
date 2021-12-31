using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Config
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.BuyerId).IsRequired();

            builder.OwnsOne(x => x.ShiptoAddress, onb =>
            {
                onb.WithOwner();
                onb.Property(a => a.City).HasMaxLength(100).IsRequired();
                onb.Property(a => a.Street).HasMaxLength(200).IsRequired();
                onb.Property(a => a.State).HasMaxLength(60).IsRequired();
                onb.Property(a => a.ZipCode).HasMaxLength(18).IsRequired();
                onb.Property(a => a.Country).HasMaxLength(90).IsRequired();
            });

            builder.Navigation(x => x.ShiptoAddress).IsRequired();
        }
    }
}
