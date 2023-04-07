using HallOfFame.Core.Entities;
using HallOfFame.Core.Exceptions;
using HallOfFame.Core.Extensions;
using HallOfFame.Core.ExternalServices;
using HallOfFame.Core.Services.PersonService.Dtos;
using HallOfFame.Core.Services.PersonService.Validators;
using HallOfFame.Core.Services.PersonService.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HallOfFame.Core.Services.PersonService;

public class PersonService : IPersonService
{
    private readonly IAppDbContext _dbContext;
    private readonly PersonDtoValidator _valiadator;

    public PersonService(IAppDbContext dbContext, PersonDtoValidator valiadator)
    {
        _dbContext = dbContext;
        _valiadator = valiadator;
    }

    public async Task<List<PersonVm>> GetAll()
    {
        return await _dbContext.Persons
            .Select(p => new PersonVm(
                p.Id,
                p.Name,
                p.DisplayName,
                p.Skills.Select(s => new SkillVm(s.Name, s.Level)).ToList()))
            .ToListAsync();
    }

    public async Task<PersonVm> GetPersonById(long id)
    {
        var person = await GetPersonFromDbOrThrowIfNotExistAsync(id);

        return new PersonVm(
            person.Id,
            person.Name,
            person.DisplayName,
            person.Skills.Select(s => new SkillVm(s.Name, s.Level)).ToList());
    }

    public async Task Create(PersonDto personDto)
    {
        _valiadator.ValidateAndThrowIfInvalid(personDto);

        var person = CreatePerson(personDto);

        _dbContext.Persons.Add(person);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(long id, PersonDto personDto)
    {
        _valiadator.ValidateAndThrowIfInvalid(personDto);

        var person = await GetPersonFromDbOrThrowIfNotExistAsync(id);

        UpdatePersonData(person, personDto);

        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(long id)
    {
        var person = await GetPersonFromDbOrThrowIfNotExistAsync(id);

        _dbContext.Persons.Remove(person);
        await _dbContext.SaveChangesAsync();
    }

    private Person CreatePerson(PersonDto personDto)
    {
        return new Person
        {
            Name = personDto.Name,
            DisplayName = personDto.DisplayName,
            Skills = personDto.Skills?.Select(s => new Skill
            {
                Name = s.Name,
                Level = s.Level
            }).ToList()
        };
    }

    private async Task<Person> GetPersonFromDbOrThrowIfNotExistAsync(long id)
    {
        return await _dbContext.Persons
                   .Where(p => p.Id == id)
                   .Include(p => p.Skills)
                   .FirstOrDefaultAsync()
               ?? throw new HallOfFameNotFoundException(nameof(Person));
    }

    private void UpdatePersonData(Person person, PersonDto personDto)
    {
        person.Name = personDto.Name;
        person.DisplayName = personDto.DisplayName;

        UpdateTrackingAndRemoveNotExistingSkills(person, personDto);

        AddNewSkills(person, personDto);
    }

    private void AddNewSkills(Person person, PersonDto personDto)
    {
        var currentSkillNames = person.Skills.Select(s => s.Name).ToArray();

        var skillsToAdd = new List<Skill>();

        foreach (var incomingSkill in personDto.Skills)
        {
            if (!currentSkillNames.Contains(incomingSkill.Name))
            {
                skillsToAdd.Add(new Skill
                {
                    PersonId = person.Id,
                    Name = incomingSkill.Name,
                    Level = incomingSkill.Level
                });
            }
        }

        _dbContext.Skills.AddRange(skillsToAdd);
    }

    private void UpdateTrackingAndRemoveNotExistingSkills(Person person, PersonDto personDto)
    {
        var incomingSkillNames = personDto.Skills.Select(s => s.Name).ToArray();

        var skillsToRemove = new List<Skill>();

        foreach (var currentSkill in person.Skills)
        {
            if (incomingSkillNames.Contains(currentSkill.Name))
            {
                var incomingSkillLevel = personDto.Skills
                    .First(s => s.Name == currentSkill.Name).Level;

                currentSkill.Level = incomingSkillLevel;

                continue;
            }

            skillsToRemove.Add(currentSkill);
        }

        _dbContext.Skills.RemoveRange(skillsToRemove);
    }
}