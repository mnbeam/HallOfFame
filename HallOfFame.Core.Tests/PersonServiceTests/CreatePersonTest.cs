using HallOfFame.Core.Exceptions;
using HallOfFame.Core.Services.PersonService.Dtos;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace HallOfFame.Core.Tests.PersonServiceTests;

public class CreatePersonTest : PersonServiceTestsBase
{
    [TestCase("pName", "dName", "sName1", "sName1", 4, 6)]
    public async Task Create__ShouldCreateNewPersonInDatabase(
        string personName,
        string displayName,
        string skillName1,
        string skillName2,
        byte skillLevel1,
        byte skillLevel2)
    {
        //arrange;
        var personDto = new PersonDto
        {
            Name = personName,
            DisplayName = displayName,
            Skills = new List<SkillDto>
            {
                new()
                {
                    Name = skillName1,
                    Level = skillLevel1
                },
                new()
                {
                    Name = skillName2,
                    Level = skillLevel2
                }
            }
        };

        var expectedPersonId = 3;

        //act
        await PersonService.Create(personDto);

        //assert
        var person = await DbContext.Persons.Include(p => p.Skills)
            .FirstAsync(p => p.Name == personDto.Name);

        Assert.AreEqual(expectedPersonId, person.Id);
        Assert.AreEqual(personDto.Name, person.Name);
        Assert.AreEqual(personDto.DisplayName, person.DisplayName);
        Assert.AreEqual(personDto.Skills.Count, person.Skills.Count);

        for (var i = 0; i < personDto.Skills.Count; i++)
        {
            Assert.AreEqual(personDto.Skills[i].Name, person.Skills[i].Name);
            Assert.AreEqual(personDto.Skills[i].Level, person.Skills[i].Level);
        }
    }

    [TestCase("", "word", "word", 7)]
    [TestCase("word", "", "word", 7)]
    [TestCase("word", "word", "", 7)]
    [TestCase("word", "word", "word", 11)]
    public async Task Create__ShouldThrowValidationException(
        string personName,
        string displayName,
        string skillName,
        byte skillLevel)
    {
        //arrange;
        var personDto = new PersonDto
        {
            Name = personName,
            DisplayName = displayName,
            Skills = new List<SkillDto>
            {
                new()
                {
                    Name = skillName,
                    Level = skillLevel
                }
            }
        };

        //act
        //assert
        Assert.ThrowsAsync<HallOfFameValidationException>(async () =>
            await PersonService.Create(personDto));
    }
}