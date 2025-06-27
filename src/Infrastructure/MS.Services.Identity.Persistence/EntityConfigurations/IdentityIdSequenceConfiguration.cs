using MS.Services.Identity.Domain.EntityConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.Identity.Persistence.EntityConfigurations;

public class IdentityIdSequenceConfiguration : IEntityTypeConfiguration<IdentityIdSequence>
{
    public void Configure(EntityTypeBuilder<IdentityIdSequence> builder)
    {
        builder.ToTable(nameof(IdentityIdSequence));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .UseIdentityColumn();

        builder.HasIndex(x => x.Entity).IsUnique();

        builder.HasIndex(x => x.Prefix).IsUnique();

        builder.Property(x => x.Entity)
            .HasMaxLength(KeyEntityConst.EntityMaxLength)
            .IsRequired();

        builder.Property(x => x.Prefix)
            .HasMaxLength(KeyEntityConst.Identity.Property.PrefixMaxLength)
            .IsRequired();

        builder.Property(x => x.Counter)
            .IsConcurrencyToken()
            .IsRequired();
    }
}
