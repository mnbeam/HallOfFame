using HallOfFame.Core.Services.PersonService;
using HallOfFame.Core.Services.PersonService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HallOfFame.Api.Controllers;

[ApiController]
[Route("api/v1")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet("persons")]
    public async Task<IActionResult> GetAll()
    {
        var vm = await _personService.GetAll();

        return Ok(vm);
    }

    [HttpGet("person/{id}")]
    public async Task<IActionResult> GetById([FromRoute] long id)
    {
        var vm = await _personService.GetPersonById(id);

        return Ok(vm);
    }

    [HttpPost("person")]
    public async Task<IActionResult> Create([FromBody] PersonDto personDto)
    {
        await _personService.Create(personDto);

        return Ok();
    }

    [HttpPut("person/{id}")]
    public async Task<IActionResult> Update([FromRoute] long id, [FromBody] PersonDto personDto)
    {
        await _personService.Update(id, personDto);

        return Ok();
    }

    [HttpDelete("person/{id}")]
    public async Task<IActionResult> Delete([FromRoute] long id)
    {
        await _personService.Delete(id);

        return Ok();
    }
}