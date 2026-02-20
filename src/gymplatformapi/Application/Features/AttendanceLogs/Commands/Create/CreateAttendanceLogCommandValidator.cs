using FluentValidation;

namespace Application.Features.AttendanceLogs.Commands.Create;

public class CreateAttendanceLogCommandValidator : AbstractValidator<CreateAttendanceLogCommand>
{
    public CreateAttendanceLogCommandValidator()
    {
        RuleFor(c => c.Result).NotEmpty();
        RuleFor(c => c.DenyReason).NotEmpty();
        RuleFor(c => c.MemberId).NotEmpty();
        RuleFor(c => c.GateId).NotEmpty();
    }
}
