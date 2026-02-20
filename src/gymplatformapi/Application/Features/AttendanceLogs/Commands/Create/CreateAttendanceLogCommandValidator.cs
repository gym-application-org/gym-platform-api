using Domain.Enums;
using FluentValidation;

namespace Application.Features.AttendanceLogs.Commands.Create;

public class CreateAttendanceLogCommandValidator : AbstractValidator<CreateAttendanceLogCommand>
{
    public CreateAttendanceLogCommandValidator()
    {
        RuleFor(c => c.GateId).NotEmpty();
    }
}
