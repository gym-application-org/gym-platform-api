using Application.Features.AttendanceLogs.Commands.Create;
using Application.Features.AttendanceLogs.Queries.GetAdminList;
using Application.Features.AttendanceLogs.Queries.GetById;
using Application.Features.AttendanceLogs.Queries.GetByMemberList;
using Application.Features.AttendanceLogs.Queries.GetList;
using Application.Features.AttendanceLogs.Queries.GetMyList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.AttendanceLogs.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<AttendanceLog, CreateAttendanceLogCommand>().ReverseMap();
        CreateMap<AttendanceLog, CreatedAttendanceLogResponse>().ReverseMap();
        CreateMap<AttendanceLog, GetByIdAttendanceLogResponse>().ReverseMap();
        CreateMap<AttendanceLog, GetAdminListAttendanceLogListItemDto>().ReverseMap();
        CreateMap<IPaginate<AttendanceLog>, GetListResponse<GetAdminListAttendanceLogListItemDto>>().ReverseMap();
        CreateMap<AttendanceLog, GetByMemberListAttendanceLogListItemDto>().ReverseMap();
        CreateMap<IPaginate<AttendanceLog>, GetListResponse<GetByMemberListAttendanceLogListItemDto>>().ReverseMap();
        CreateMap<AttendanceLog, GetListAttendanceLogListItemDto>().ReverseMap();
        CreateMap<IPaginate<AttendanceLog>, GetListResponse<GetListAttendanceLogListItemDto>>().ReverseMap();
        CreateMap<AttendanceLog, GetMyListAttendanceLogListItemDto>().ReverseMap();
        CreateMap<IPaginate<AttendanceLog>, GetListResponse<GetMyListAttendanceLogListItemDto>>().ReverseMap();
    }
}
