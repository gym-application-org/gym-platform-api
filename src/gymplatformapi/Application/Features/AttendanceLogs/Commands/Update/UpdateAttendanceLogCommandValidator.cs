using FluentValidation;

namespace Application.Features.AttendanceLogs.Commands.Update;

public class UpdateAttendanceLogCommandValidator : AbstractValidator<UpdateAttendanceLogCommand>
{
    public UpdateAttendanceLogCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Result).NotEmpty();
        RuleFor(c => c.DenyReason).NotEmpty();
        RuleFor(c => c.MemberId).NotEmpty();
        RuleFor(c => c.GateId).NotEmpty();
    }
}
