using FluentValidation;

namespace Application.Features.Members.Commands.Update;

public class UpdateMemberCommandValidator : AbstractValidator<UpdateMemberCommand>
{
    public UpdateMemberCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
        RuleFor(c => c.Phone).NotEmpty();
        RuleFor(c => c.Email).NotEmpty();
        RuleFor(c => c.Status).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
    }
}
