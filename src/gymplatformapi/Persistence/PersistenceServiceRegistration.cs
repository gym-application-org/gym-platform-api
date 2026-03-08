using Application.Services.Repositories;
using Core.Persistence.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.AddDbContext<BaseDbContext>(options => options.UseInMemoryDatabase("BaseDb"));
        _ = services.AddDbMigrationApplier(buildServices => buildServices.GetRequiredService<BaseDbContext>());

        _ = services.AddScoped<IEmailAuthenticatorRepository, EmailAuthenticatorRepository>();
        _ = services.AddScoped<IOperationClaimRepository, OperationClaimRepository>();
        _ = services.AddScoped<IOtpAuthenticatorRepository, OtpAuthenticatorRepository>();
        _ = services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        _ = services.AddScoped<IUserRepository, UserRepository>();
        _ = services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();
        _ = services.AddScoped<ITenantMembershipRepository, TenantMembershipRepository>();

        services.AddScoped<IAttendanceLogRepository, AttendanceLogRepository>();
        services.AddScoped<IDietAssignmentRepository, DietAssignmentRepository>();
        services.AddScoped<IDietTemplateRepository, DietTemplateRepository>();
        services.AddScoped<IDietTemplateDayRepository, DietTemplateDayRepository>();
        services.AddScoped<IDietTemplateMealRepository, DietTemplateMealRepository>();
        services.AddScoped<IDietTemplateMealItemRepository, DietTemplateMealItemRepository>();
        services.AddScoped<IExerciseRepository, ExerciseRepository>();
        services.AddScoped<IGateRepository, GateRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<IProgressEntryRepository, ProgressEntryRepository>();
        services.AddScoped<IStaffRepository, StaffRepository>();
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        services.AddScoped<ISubscriptionPlanRepository, SubscriptionPlanRepository>();
        services.AddScoped<ISupportTicketRepository, SupportTicketRepository>();
        services.AddScoped<ITenantRepository, TenantRepository>();
        services.AddScoped<IWorkoutAssignmentRepository, WorkoutAssignmentRepository>();
        services.AddScoped<IWorkoutTemplateRepository, WorkoutTemplateRepository>();
        services.AddScoped<IWorkoutTemplateDayRepository, WorkoutTemplateDayRepository>();
        services.AddScoped<IWorkoutTemplateDayExerciseRepository, WorkoutTemplateDayExerciseRepository>();
        services.AddScoped<IEmailOtpRepository, EmailOtpRepository>();
        services.AddScoped<IUserActionTokenRepository, UserActionTokenRepository>();
        return services;
    }
}
