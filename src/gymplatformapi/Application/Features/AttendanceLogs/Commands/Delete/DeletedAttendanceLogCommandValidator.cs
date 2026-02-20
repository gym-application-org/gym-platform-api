using FluentValidation;

namespace Application.Features.AttendanceLogs.Commands.Delete;

public class DeleteAttendanceLogCommandValidator : AbstractValidator<DeleteAttendanceLogCommand>
{
    public DeleteAttendanceLogCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
