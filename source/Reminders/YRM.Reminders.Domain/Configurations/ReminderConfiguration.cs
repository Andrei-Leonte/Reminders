using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YRM.Reminders.Domain.Entities;

namespace YRM.Reminders.Domain.Configurations
{
    internal class ReminderConfiguration : IEntityTypeConfiguration<Reminder>
    {
        public void Configure(EntityTypeBuilder<Reminder> builder)
        {
            builder.HasKey(entity => entity.Id);

            builder
                .Property(e => e.Title)
                .HasMaxLength(128)
                .IsRequired();

            builder
                .Property(e => e.Description)
                .HasMaxLength(256)
                .IsRequired();
        }
    }
}