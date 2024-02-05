using Data.Entities.Ticket.Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Persistence.Configurations;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.Property(x => x.Title).HasMaxLength(255);
        builder.Property(x => x.Description).HasMaxLength(700);

        builder.HasOne(x => x.Category).WithMany();
        builder.HasOne(x => x.Issuer).WithMany();
        builder.HasOne(x => x.Assignee).WithMany();
    }
}
