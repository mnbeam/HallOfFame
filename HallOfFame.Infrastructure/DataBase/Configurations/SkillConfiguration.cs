using HallOfFame.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HallOfFame.Infrastructure.DataBase.Configurations;

public class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.HasOne(s => s.Person)
            .WithMany(p => p.Skills)
            .HasForeignKey(s => s.PersonId);
    }
}