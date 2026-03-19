using Domain.Enums;
using FluentValidation;

namespace Application.Features.AttendanceLogs.Commands.Create;

public class CreateAttendanceLogCommandValidator : AbstractValidator<CreateAttendanceLogCommand>
{
    public CreateAttendanceLogCommandValidator()
    {
        RuleFor(c => c.Result).IsInEnum();

        RuleFor(c => c.DenyReason)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(500)
            .When(c => c.Result == AttendanceResult.Denied)
            .WithMessage("Deny reason is required when attendance is denied");

        RuleFor(c => c.GateId).GreaterThan(0);
    }
}
