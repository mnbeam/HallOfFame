using HallOfFame.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HallOfFame.Infrastructure.DataBase.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.Property(p => p.Name).HasMaxLength(50);

        builder.Property(p => p.DisplayName).HasMaxLength(20);
    }
}