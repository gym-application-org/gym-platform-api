using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Auth.Rules;
using Application.Services.Members;
using Application.Services.Staffs;
using Application.Services.UserActionTokens;
using Application.Services.UsersService;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Core.Security.Hashing;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Auth.Commands.ActivateInvitedUser;

public class ActivateInvitedUserCommand : IRequest
{
    public string Token { get; set; }
    public string NewPassword { get; set; }

    public ActivateInvitedUserCommand()
    {
        Token = string.Empty;
        NewPassword = string.Empty;
    }

    public ActivateInvitedUserCommand(string token, string newPassword)
    {
        Token = token;
        NewPassword = newPassword;
    }

    public class ActivateInvitedUserCommandHandler : IRequestHandler<ActivateInvitedUserCommand>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IUserService _userService;
        private readonly IMemberService _memberService;
        private readonly IStaffService _staffService;
        private readonly IUserActionTokenService _userActionTokenService;

        public ActivateInvitedUserCommandHandler(
            AuthBusinessRules authBusinessRules,
            IUserService userService,
            IMemberService memberService,
            IStaffService staffService,
            IUserActionTokenService userActionTokenService
        )
        {
            _authBusinessRules = authBusinessRules;
            _userService = userService;
            _memberService = memberService;
            _staffService = staffService;
            _userActionTokenService = userActionTokenService;
        }

        public async Task Handle(ActivateInvitedUserCommand command, CancellationToken cancellationToken)
        {
            HashingHelper.CreateActionTokenHash(command.Token, out byte[] tokenHash);

            UserActionToken? token = await _userActionTokenService.GetAsync(
                x => x.TokenHash == tokenHash,
                cancellationToken: cancellationToken
            );
            await _authBusinessRules.ActionTokenShouldBeExist(token);
            await _authBusinessRules.ActionTokenShouldNotBeRevoked(token!);
            await _authBusinessRules.ActionTokenShouldNotBeUsed(token!);
            await _authBusinessRules.ActionTokenShouldNotBeExpired(token!);

            User? user = await _userService.GetAsync(x => x.Id == token!.UserId, cancellationToken: cancellationToken);
            await _authBusinessRules.UserShouldBeExistsWhenSelected(user);

            if (token!.TargetType == null || token!.TargetEntityId == null)
            {
                throw new BusinessException("TargetNotSelected");
            }

            Member? member = null;
            Staff? staff = null;
            if (token.TargetType == UserActionTargetType.Member)
            {
                member = await _memberService.GetAsync(x => x.Id == token.TargetEntityId, cancellationToken: cancellationToken);
                await _authBusinessRules.MemberShouldBeExistWhenSelected(member);
            }
            else
            {
                staff = await _staffService.GetAsync(x => x.Id == token.TargetEntityId, cancellationToken: cancellationToken);
                await _authBusinessRules.StaffShouldBeExistWhenSelected(staff);
            }

            if (staff != null)
            {
                staff.IsActive = true;
                await _staffService.UpdateAsync(staff);
            }
            if (member != null)
            {
                member.Status = MemberStatus.Active;
                await _memberService.UpdateAsync(member);
            }

            user!.Status = true;
            user!.MustChangePassword = true;
            await _userService.UpdateAsync(user);

            token.UsedAt = DateTime.UtcNow;
            token.UpdatedDate = DateTime.UtcNow;
            await _userService.UpdateAsync(user);
        }
    }
}
