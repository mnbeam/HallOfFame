using HallOfFame.Core.Services.PersonService.Dtos;
using HallOfFame.Core.Services.PersonService.ViewModels;

namespace HallOfFame.Core.Services.PersonService;

public interface IPersonService
{
    Task<List<PersonVm>> GetAll();

    Task<PersonVm> GetPersonById(long id);

    Task Create(PersonDto personDto);

    Task Update(long id, PersonDto personDto);

    Task Delete(long id);
}