using Application.Features.SubscriptionPlans.Commands.Create;
using Application.Features.SubscriptionPlans.Commands.Delete;
using Application.Features.SubscriptionPlans.Commands.Update;
using Application.Features.SubscriptionPlans.Queries.GetById;
using Application.Features.SubscriptionPlans.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.SubscriptionPlans.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<SubscriptionPlan, CreateSubscriptionPlanCommand>().ReverseMap();
        CreateMap<SubscriptionPlan, CreatedSubscriptionPlanResponse>().ReverseMap();
        CreateMap<SubscriptionPlan, UpdateSubscriptionPlanCommand>().ReverseMap();
        CreateMap<SubscriptionPlan, UpdatedSubscriptionPlanResponse>().ReverseMap();
        CreateMap<SubscriptionPlan, DeleteSubscriptionPlanCommand>().ReverseMap();
        CreateMap<SubscriptionPlan, DeletedSubscriptionPlanResponse>().ReverseMap();
        CreateMap<SubscriptionPlan, GetByIdSubscriptionPlanResponse>().ReverseMap();
        CreateMap<SubscriptionPlan, GetListSubscriptionPlanListItemDto>().ReverseMap();
        CreateMap<IPaginate<SubscriptionPlan>, GetListResponse<GetListSubscriptionPlanListItemDto>>().ReverseMap();
    }
}
