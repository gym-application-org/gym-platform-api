using Application.Features.EmailOtps.Commands.Create;
using Application.Features.EmailOtps.Commands.Delete;
using Application.Features.EmailOtps.Commands.Update;
using Application.Features.EmailOtps.Queries.GetById;
using Application.Features.EmailOtps.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.EmailOtps.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<EmailOtp, CreateEmailOtpCommand>().ReverseMap();
        CreateMap<EmailOtp, CreatedEmailOtpResponse>().ReverseMap();
        CreateMap<EmailOtp, UpdateEmailOtpCommand>().ReverseMap();
        CreateMap<EmailOtp, UpdatedEmailOtpResponse>().ReverseMap();
        CreateMap<EmailOtp, DeleteEmailOtpCommand>().ReverseMap();
        CreateMap<EmailOtp, DeletedEmailOtpResponse>().ReverseMap();
        CreateMap<EmailOtp, GetByIdEmailOtpResponse>().ReverseMap();
        CreateMap<EmailOtp, GetListEmailOtpListItemDto>().ReverseMap();
        CreateMap<IPaginate<EmailOtp>, GetListResponse<GetListEmailOtpListItemDto>>().ReverseMap();
    }
}
