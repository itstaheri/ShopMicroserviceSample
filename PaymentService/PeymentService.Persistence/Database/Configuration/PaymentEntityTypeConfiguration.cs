using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentService.Domain.Entities;

namespace PeymentService.Persistence.Database.Configuration
{
    public class PaymentEntityTypeConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasOne(x=>x.Order).WithMany(x=>x.Payments).HasForeignKey(x=>x.OrderId);
        }
    }
}
