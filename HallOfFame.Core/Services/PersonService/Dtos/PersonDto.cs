namespace HallOfFame.Core.Services.PersonService.Dtos;

public class PersonDto
{
    public string Name { get; set; }

    public string DisplayName { get; set; }

    public List<SkillDto>? Skills { get; set; }
}