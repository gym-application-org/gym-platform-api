using Application.Features.ProgressEntries.Commands.Create;
using FluentValidation;

namespace Application.Features.ProgressEntries.Commands.Update;

public class UpdateProgressEntryCommandValidator : AbstractValidator<UpdateProgressEntryCommand>
{
    public UpdateProgressEntryCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(x => x.Date).NotEmpty().LessThanOrEqualTo(DateTime.UtcNow.Date.AddDays(1)).WithMessage("Date cannot be in the future.");

        RuleFor(x => x).Must(HasAnyProgressData).WithMessage("At least one measurement or a note must be provided.");

        RuleFor(x => x.WeightKg).GreaterThan(0).When(x => x.WeightKg.HasValue).LessThanOrEqualTo(500).When(x => x.WeightKg.HasValue);

        RuleFor(x => x.BodyFatPercent).InclusiveBetween(1, 80).When(x => x.BodyFatPercent.HasValue);

        RuleFor(x => x.MuscleMassKg)
            .GreaterThan(0)
            .When(x => x.MuscleMassKg.HasValue)
            .LessThanOrEqualTo(200)
            .When(x => x.MuscleMassKg.HasValue);

        RuleFor(x => x.ChestCm).InclusiveBetween(30, 250).When(x => x.ChestCm.HasValue);

        RuleFor(x => x.WaistCm).InclusiveBetween(30, 250).When(x => x.WaistCm.HasValue);

        RuleFor(x => x.HipCm).InclusiveBetween(30, 250).When(x => x.HipCm.HasValue);

        RuleFor(x => x.ArmCm).InclusiveBetween(10, 100).When(x => x.ArmCm.HasValue);

        RuleFor(x => x.LegCm).InclusiveBetween(20, 150).When(x => x.LegCm.HasValue);

        RuleFor(x => x.Note).MaximumLength(1000).When(x => x.Note is not null);
    }

    private static bool HasAnyProgressData(UpdateProgressEntryCommand x)
    {
        return x.WeightKg.HasValue
            || x.BodyFatPercent.HasValue
            || x.MuscleMassKg.HasValue
            || x.ChestCm.HasValue
            || x.WaistCm.HasValue
            || x.HipCm.HasValue
            || x.ArmCm.HasValue
            || x.LegCm.HasValue
            || !string.IsNullOrWhiteSpace(x.Note);
    }
}
