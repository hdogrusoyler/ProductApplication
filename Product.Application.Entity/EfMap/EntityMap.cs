using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Entity.EfMap
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(e => e.ProductCode)
                   .IsRequired();
            builder.HasIndex(e => e.ProductCode)
                   .IsUnique();
            builder.HasMany(b => b.OrderProducts)
                .WithOne(d => d.Product)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
                        
        }
    }

    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(e => e.OrderNumber)
                   .IsRequired();
            builder.HasIndex(e => e.OrderNumber)
                   .IsUnique();
            builder.HasMany(b => b.OrderProducts)
                .WithOne(d => d.Order)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }

    public class OrderProductMap : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            //builder.HasNoKey();
            builder.Property(e => e.OrderId)
                   .IsRequired();

            builder.HasOne(c => c.Product)
                .WithMany(u => u.OrderProducts)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Cascade); //NoAction //Cascade //Restrict 

            builder.HasOne(c => c.Order)
                .WithMany(u => u.OrderProducts)
                .HasForeignKey(c => c.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.UserName)
                   .IsRequired();
            builder.HasIndex(e => e.UserName)
                   .IsUnique();
        }
    }
    //dotnet ef migrations add ...
    //dotnet ef database update
}
