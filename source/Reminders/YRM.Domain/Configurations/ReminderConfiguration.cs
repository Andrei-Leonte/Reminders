using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YRM.Domain.Entities;

namespace YRM.Domain.Configurations
{
    public class ReminderConfiguration : IEntityTypeConfiguration<Reminder>
    {
        public void Configure(EntityTypeBuilder<Reminder> builder)
        {
            builder.HasKey(entity => entity.Id);

            builder
                .Property(e => e.Description)
                .HasMaxLength(256)
                .IsRequired();
        }
    }
}