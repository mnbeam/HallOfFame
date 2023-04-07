namespace HallOfFame.Core.Services.PersonService.ViewModels;

public record PersonVm(long Id, string Name, string DisplayName, List<SkillVm> Skills);