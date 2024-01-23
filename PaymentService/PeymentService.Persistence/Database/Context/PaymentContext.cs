using Microsoft.EntityFrameworkCore;
using PaymentService.Domain.Entities;
using PeymentService.Application.Interfaces;
using PeymentService.Persistence.Database.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeymentService.Persistence.Database.Context
{
    public class PaymentContext : DbContext, IDbContextRepository
    {
        public PaymentContext(DbContextOptions<PaymentContext> options) : base(options)
        {

        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentEntityTypeConfiguration());
                
            base.OnModelCreating(modelBuilder);
        }

    }
}
