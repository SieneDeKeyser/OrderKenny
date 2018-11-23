using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Order_domain.Customers;
using Order_domain.Customers.PhoneNumbers;
using Order_domain.Items;
using Order_domain.Orders;
using Order_domain.Orders.OrderItems;
using System;
using System.Collections.Generic;
using System.Text;
using static Order_domain.Customers.PhoneNumbers.PhoneNumber;

namespace Order_domain.Data
{
    public class OrderDbContext : DbContext
    {
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                        .ToTable("Customers")
                        .HasKey(cust => cust.Id);

            modelBuilder.Entity<Item>()
                        .ToTable("Items")
                        .HasKey(item => item.Id);

            modelBuilder.Entity<Order>()
                        .ToTable("Orders")
                        .HasKey(order => order.Id);

            modelBuilder.Entity<Customer>()
                        .OwnsOne(cust => cust.Address,
                            custAddress =>
                            {
                                custAddress.Property(prop => prop.StreetName).HasColumnName("StreetName");
                                custAddress.Property(prop => prop.PostalCode).HasColumnName("PostalCode");
                                custAddress.Property(prop => prop.HouseNumber).HasColumnName("HouseNumber");
                                custAddress.Property(prop => prop.Country).HasColumnName("Country");
                            });

            modelBuilder.Entity<Customer>()
                        .Property(cust => cust.PhoneNumber)
                        .HasColumnName("PhoneNumber")
                        .HasConversion(
                            phone => phone.CountryCallingCode + "/" + phone.Number,
                            phoneNumberRecord => PhoneNumberBuilder
                                                     .PhoneNumber()
                                                     .WithCountryCallingCode(phoneNumberRecord.Split('/', StringSplitOptions.None)[0])
                                                     .WithNumber(phoneNumberRecord.Split('/', StringSplitOptions.None)[1])
                                                     .Build());

            modelBuilder.Entity<Customer>()
                        .OwnsOne(cust => cust.Email,
                             custEmail =>
                             {
                                 custEmail.Property(prop => prop.Complete).HasColumnName("Email");
                             });

            modelBuilder.Entity<Item>()
                        .OwnsOne(item => item.Price,
                        itemPrice =>
                        {
                            itemPrice.Property(prop => prop.Amount).HasColumnName("Price");
                        });

            modelBuilder.Entity<OrderItem>()
            .OwnsOne(orderItem => orderItem.ItemPrice,
            itemPrice =>
            {
                itemPrice.Property(prop => prop.Amount).HasColumnName("Price");
            });

            modelBuilder.Entity<OrderItem>()
                        .ToTable("OrderItems")
                        .HasKey(orderItem => new { orderItem.OrderId, orderItem.ItemId });

            modelBuilder.Entity<OrderItem>()
                        .HasOne(orderItem => orderItem.Item)
                        .WithMany()
                        .HasForeignKey(orderItem => orderItem.ItemId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                        .HasOne(orderItem => orderItem.MainOrder)
                        .WithMany(mainOrder => mainOrder.OrderItems)
                        .HasForeignKey(orderItem => orderItem.OrderId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                        .HasOne(order => order.Customer)
                        .WithMany()
                        .HasForeignKey(order => order.CustomerId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);           

            base.OnModelCreating(modelBuilder);
        }
    }
}
