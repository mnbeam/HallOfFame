using FluentValidation;
using HallOfFame.Core.Services.PersonService.Dtos;

namespace HallOfFame.Core.Services.PersonService.Validators;

public class SkillDtoValidator : AbstractValidator<SkillDto>
{
    public SkillDtoValidator()
    {
        RuleFor(s => s.Level)
            .Must(l => l > 0 && l <= 10).WithMessage("level must be between 1 and 10");

        RuleFor(s => s.Name)
            .NotEmpty().WithMessage("Skill name can not be empty")
            .Must(n => n.Length >= 1 && n.Length <= 30)
            .WithMessage("Name length must be between 1 and 30");
        ;
    }
}