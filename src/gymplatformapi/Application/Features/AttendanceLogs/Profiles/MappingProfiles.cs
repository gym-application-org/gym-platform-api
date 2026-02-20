using Application.Features.AttendanceLogs.Commands.Create;
using Application.Features.AttendanceLogs.Commands.Delete;
using Application.Features.AttendanceLogs.Commands.Update;
using Application.Features.AttendanceLogs.Queries.GetById;
using Application.Features.AttendanceLogs.Queries.GetList;
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
        CreateMap<AttendanceLog, UpdateAttendanceLogCommand>().ReverseMap();
        CreateMap<AttendanceLog, UpdatedAttendanceLogResponse>().ReverseMap();
        CreateMap<AttendanceLog, DeleteAttendanceLogCommand>().ReverseMap();
        CreateMap<AttendanceLog, DeletedAttendanceLogResponse>().ReverseMap();
        CreateMap<AttendanceLog, GetByIdAttendanceLogResponse>().ReverseMap();
        CreateMap<AttendanceLog, GetListAttendanceLogListItemDto>().ReverseMap();
        CreateMap<IPaginate<AttendanceLog>, GetListResponse<GetListAttendanceLogListItemDto>>().ReverseMap();
    }
}
