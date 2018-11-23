using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Order_domain.Customers;
using Order_domain.Items;
using System;
using System.Collections.Generic;
using System.Text;


namespace Order_service.Data
{
    public class OrderDbContext : DbContext
    {
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Customer> Customers { get; set;}

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
                        .OwnsOne(cust => cust.PhoneNumber,
                            custPhone =>
                            {
                                custPhone.Property(prop => prop.CountryCallingCode + prop.Number).HasColumnName("PhoneNumber");
                            });

            modelBuilder.Entity<Customer>()
                        .OwnsOne(cust => cust.Email,
                             custEmail =>
                             {
                                 custEmail.Property(prop => prop.Complete).HasColumnName("Email");
                             });

            base.OnModelCreating(modelBuilder);
        }
    }
}
