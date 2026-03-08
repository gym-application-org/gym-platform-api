using Application.Features.UserActionTokens.Commands.Create;
using Application.Features.UserActionTokens.Commands.Delete;
using Application.Features.UserActionTokens.Commands.Update;
using Application.Features.UserActionTokens.Queries.GetById;
using Application.Features.UserActionTokens.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.UserActionTokens.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<UserActionToken, CreateUserActionTokenCommand>().ReverseMap();
        CreateMap<UserActionToken, CreatedUserActionTokenResponse>().ReverseMap();
        CreateMap<UserActionToken, UpdateUserActionTokenCommand>().ReverseMap();
        CreateMap<UserActionToken, UpdatedUserActionTokenResponse>().ReverseMap();
        CreateMap<UserActionToken, DeleteUserActionTokenCommand>().ReverseMap();
        CreateMap<UserActionToken, DeletedUserActionTokenResponse>().ReverseMap();
        CreateMap<UserActionToken, GetByIdUserActionTokenResponse>().ReverseMap();
        CreateMap<UserActionToken, GetListUserActionTokenListItemDto>().ReverseMap();
        CreateMap<IPaginate<UserActionToken>, GetListResponse<GetListUserActionTokenListItemDto>>().ReverseMap();
    }
}
