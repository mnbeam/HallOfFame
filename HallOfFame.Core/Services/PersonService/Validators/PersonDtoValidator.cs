using FluentValidation;
using HallOfFame.Core.Services.PersonService.Dtos;

namespace HallOfFame.Core.Services.PersonService.Validators;

public class PersonDtoValidator : AbstractValidator<PersonDto>
{
    public PersonDtoValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name can not be empty")
            .Must(p => p.Length >= 3 && p.Length <= 50)
            .WithMessage("Name length must be between 3 and 50");

        RuleFor(p => p.DisplayName)
            .NotEmpty().WithMessage("DisplayName can not be empty")
            .Must(p => p.Length >= 3 && p.Length <= 20)
            .WithMessage("Name length must be between 3 and 20");

        RuleForEach(p => p.Skills).SetValidator(new SkillDtoValidator());
    }
}