using Application.Features.AttendanceLogs.Constants;
using Application.Features.AttendanceLogs.Rules;
using Application.Services.Members;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Abstractions.Security;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Constants;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using static Application.Features.AttendanceLogs.Constants.AttendanceLogsOperationClaims;

namespace Application.Features.AttendanceLogs.Queries.GetMyList;

public class GetMyListAttendanceLogQuery : IRequest<GetListResponse<GetMyListAttendanceLogListItemDto>>, ISecuredRequest, ITenantRequest
{
    public PageRequest PageRequest { get; set; }
    public int? GateId { get; set; }
    public AttendanceResult? Result { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }

    public string[] Roles => [GeneralOperationClaims.Member];

    public class GetMyListAttendanceLogQueryHandler
        : IRequestHandler<GetMyListAttendanceLogQuery, GetListResponse<GetMyListAttendanceLogListItemDto>>
    {
        private readonly IAttendanceLogRepository _attendanceLogRepository;
        private readonly IMapper _mapper;
        private readonly AttendanceLogBusinessRules _attendanceLogBusinessRules;
        private readonly IMemberService _memberService;
        private readonly ICurrentUser _currentUser;

        public GetMyListAttendanceLogQueryHandler(
            IAttendanceLogRepository attendanceLogRepository,
            IMapper mapper,
            ICurrentUser currentUser,
            IMemberService memberService,
            AttendanceLogBusinessRules attendanceLogBusinessRules
        )
        {
            _attendanceLogRepository = attendanceLogRepository;
            _mapper = mapper;
            _currentUser = currentUser;
            _memberService = memberService;
            _attendanceLogBusinessRules = attendanceLogBusinessRules;
        }

        public async Task<GetListResponse<GetMyListAttendanceLogListItemDto>> Handle(
            GetMyListAttendanceLogQuery request,
            CancellationToken cancellationToken
        )
        {
            Member? member = await _memberService.GetAsync(
                predicate: x => x.UserId == _currentUser.UserId,
                cancellationToken: cancellationToken
            );
            await _attendanceLogBusinessRules.MemberShouldExistWhenSelected(member);

            IPaginate<AttendanceLog> attendanceLogs = await _attendanceLogRepository.GetListAsync(
                predicate: x =>
                    x.MemberId == member!.Id
                    && (!request.GateId.HasValue || x.GateId == request.GateId)
                    && (!request.Result.HasValue || x.Result == request.Result)
                    && (!request.From.HasValue || x.CreatedDate >= request.From)
                    && (!request.To.HasValue || x.CreatedDate <= request.To),
                orderBy: q => q.OrderByDescending(x => x.CreatedDate),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                enableTracking: false,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetMyListAttendanceLogListItemDto> response = _mapper.Map<GetListResponse<GetMyListAttendanceLogListItemDto>>(
                attendanceLogs
            );
            return response;
        }
    }
}
