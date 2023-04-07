namespace HallOfFame.Core.Entities;

public class Skill
{
    public long Id { get; set; }

    public Person Person { get; set; } = null!;

    public long PersonId { get; set; }

    public string Name { get; set; } = null!;

    public byte Level { get; set; }
}