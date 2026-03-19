using FluentValidation;

namespace Application.Features.ProgressEntries.Commands.Delete;

public class DeleteProgressEntryCommandValidator : AbstractValidator<DeleteProgressEntryCommand>
{
    public DeleteProgressEntryCommandValidator()
    {
        RuleFor(c => c.Id).GreaterThan(0);
    }
}
