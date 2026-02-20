using FluentValidation;

namespace Application.Features.ProgressEntries.Commands.Create;

public class CreateProgressEntryCommandValidator : AbstractValidator<CreateProgressEntryCommand>
{
    public CreateProgressEntryCommandValidator()
    {
        RuleFor(c => c.Date).NotEmpty();
        RuleFor(c => c.WeightKg).NotEmpty();
        RuleFor(c => c.BodyFatPercent).NotEmpty();
        RuleFor(c => c.MuscleMassKg).NotEmpty();
        RuleFor(c => c.ChestCm).NotEmpty();
        RuleFor(c => c.WaistCm).NotEmpty();
        RuleFor(c => c.HipCm).NotEmpty();
        RuleFor(c => c.ArmCm).NotEmpty();
        RuleFor(c => c.LegCm).NotEmpty();
        RuleFor(c => c.Note).NotEmpty();
        RuleFor(c => c.MemberId).NotEmpty();
    }
}
