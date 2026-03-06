using System.Reflection;
using Application.Pipelines.Authorization;
using Application.Services.AttendanceLogs;
using Application.Services.AuthenticatorService;
using Application.Services.AuthService;
using Application.Services.DietAssignments;
using Application.Services.DietTemplateDays;
using Application.Services.DietTemplateMealItems;
using Application.Services.DietTemplateMeals;
using Application.Services.DietTemplates;
using Application.Services.Exercises;
using Application.Services.Gates;
using Application.Services.MailServices.AuthMails;
using Application.Services.MailServices.EmailTemplates;
using Application.Services.MailServices.UserOnboardingMails;
using Application.Services.Members;
using Application.Services.ProgressEntries;
using Application.Services.Staffs;
using Application.Services.SubscriptionPlans;
using Application.Services.Subscriptions;
using Application.Services.SupportTickets;
using Application.Services.TenantMembershipService;
using Application.Services.Tenants;
using Application.Services.UsersService;
using Application.Services.WorkoutAssignments;
using Application.Services.WorkoutTemplateDayExercises;
using Application.Services.WorkoutTemplateDays;
using Application.Services.WorkoutTemplates;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Application.Pipelines.Validation;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.CrossCuttingConcerns.Logging.Serilog.Logger;
using Core.ElasticSearch;
using Core.Localization.Resource.Yaml.DependencyInjection;
using Core.Mailing;
using Core.Mailing.MailKitImplementations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            configuration.AddOpenBehavior(typeof(TenantAuthorizationBehavior<,>));
            configuration.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
            configuration.AddOpenBehavior(typeof(CachingBehavior<,>));
            configuration.AddOpenBehavior(typeof(CacheRemovingBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));
        });

        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddSingleton<IMailService, MailKitMailService>();
        services.AddSingleton<LoggerServiceBase, FileLogger>();
        services.AddSingleton<IElasticSearch, ElasticSearchManager>();

        services.AddScoped<IAuthService, AuthManager>();
        services.AddScoped<IAuthenticatorService, AuthenticatorManager>();
        services.AddScoped<IUserService, UserManager>();
        services.AddScoped<ITenantMembershipService, TenantMembershipManager>();

        services.AddYamlResourceLocalization();

        services.AddScoped<IAttendanceLogService, AttendanceLogManager>();
        services.AddScoped<IDietAssignmentService, DietAssignmentManager>();
        services.AddScoped<IDietTemplateService, DietTemplateManager>();
        services.AddScoped<IDietTemplateDayService, DietTemplateDayManager>();
        services.AddScoped<IDietTemplateMealService, DietTemplateMealManager>();
        services.AddScoped<IDietTemplateMealItemService, DietTemplateMealItemManager>();
        services.AddScoped<IExerciseService, ExerciseManager>();
        services.AddScoped<IGateService, GateManager>();
        services.AddScoped<IMemberService, MemberManager>();
        services.AddScoped<IProgressEntryService, ProgressEntryManager>();
        services.AddScoped<IStaffService, StaffManager>();
        services.AddScoped<ISubscriptionService, SubscriptionManager>();
        services.AddScoped<ISubscriptionPlanService, SubscriptionPlanManager>();
        services.AddScoped<ISupportTicketService, SupportTicketManager>();
        services.AddScoped<ITenantService, TenantManager>();
        services.AddScoped<IWorkoutAssignmentService, WorkoutAssignmentManager>();
        services.AddScoped<IWorkoutTemplateService, WorkoutTemplateManager>();
        services.AddScoped<IWorkoutTemplateDayService, WorkoutTemplateDayManager>();
        services.AddScoped<IWorkoutTemplateDayExerciseService, WorkoutTemplateDayExerciseManager>();

        services.AddScoped<IUserOnboardingMailService, UserOnboardingMailManager>();
        services.AddScoped<IAuthMailService, AuthMailManager>();
        services.AddScoped<IMailTemplateService, MailTemplateManager>();
        return services;
    }

    public static IServiceCollection AddSubClassesOfType(
        this IServiceCollection services,
        Assembly assembly,
        Type type,
        Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null
    )
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (Type? item in types)
            if (addWithLifeCycle == null)
                services.AddScoped(item);
            else
                addWithLifeCycle(services, type);
        return services;
    }
}
