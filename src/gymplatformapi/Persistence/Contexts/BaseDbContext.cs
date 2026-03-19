using System.Linq.Expressions;
using System.Reflection;
using Core.Application.Abstractions.Security;
using Core.Security.Entities;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Contexts;

public class BaseDbContext : DbContext
{
    protected IConfiguration? Configuration { get; set; }
    public ICurrentTenant? CurrentTenant { get; set; }

    public Guid? CurrentTenantId => CurrentTenant?.TenantId;

    public bool IgnoreTenantFilter { get; set; }

    public DbSet<EmailAuthenticator> EmailAuthenticators { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<OtpAuthenticator> OtpAuthenticators { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    public DbSet<AttendanceLog> AttendanceLogs { get; set; }
    public DbSet<DietAssignment> DietAssignments { get; set; }
    public DbSet<DietTemplate> DietTemplates { get; set; }
    public DbSet<DietTemplateDay> DietTemplateDays { get; set; }
    public DbSet<DietTemplateMeal> DietTemplateMeals { get; set; }
    public DbSet<DietTemplateMealItem> DietTemplateMealItems { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<Gate> Gates { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<ProgressEntry> ProgressEntries { get; set; }
    public DbSet<Staff> Staffs { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
    public DbSet<SupportTicket> SupportTickets { get; set; }
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<WorkoutAssignment> WorkoutAssignments { get; set; }
    public DbSet<WorkoutTemplate> WorkoutTemplates { get; set; }
    public DbSet<WorkoutTemplateDay> WorkoutTemplateDays { get; set; }
    public DbSet<WorkoutTemplateDayExercise> WorkoutTemplateDayExercises { get; set; }
    public DbSet<EmailOtp> EmailOtps { get; set; }
    public DbSet<UserActionToken> UserActionTokens { get; set; }

    public BaseDbContext(DbContextOptions<BaseDbContext> options, IConfiguration configuration, ICurrentTenant currentTenant)
        : base(options)
    {
        Configuration = configuration;
        CurrentTenant = currentTenant;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        ApplyGlobalQueryFilters(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private void ApplyGlobalQueryFilters(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var clrType = entityType.ClrType;

            var hasDeletedDate = clrType.GetProperty("DeletedDate") != null;
            var isTenantEntity = typeof(TenantEntity<>).IsAssignableFromGenericType(clrType);

            if (hasDeletedDate || isTenantEntity)
            {
                var method = typeof(BaseDbContext)
                    .GetMethod(nameof(SetGlobalQueryFilter), BindingFlags.NonPublic | BindingFlags.Instance)
                    ?.MakeGenericMethod(clrType);

                method?.Invoke(this, new object[] { modelBuilder, hasDeletedDate, isTenantEntity });
            }
        }
    }

    private void SetGlobalQueryFilter<TEntity>(ModelBuilder modelBuilder, bool hasSoftDelete, bool isTenantEntity)
        where TEntity : class
    {
        ParameterExpression entityParameter = Expression.Parameter(typeof(TEntity), "e");
        Expression? filterExpression = null;

        if (hasSoftDelete)
        {
            MemberExpression deletedDateProperty = Expression.Property(entityParameter, "DeletedDate");
            MemberExpression deletedDateHasValue = Expression.Property(deletedDateProperty, "HasValue");

            filterExpression = Expression.Equal(deletedDateHasValue, Expression.Constant(false));
        }

        if (isTenantEntity)
        {
            ConstantExpression dbContextInstance = Expression.Constant(this);

            MemberExpression ignoreTenantFilterProperty = Expression.Property(dbContextInstance, nameof(IgnoreTenantFilter));

            MemberExpression currentTenantIdProperty = Expression.Property(dbContextInstance, nameof(CurrentTenantId));

            BinaryExpression currentTenantIsNullExpression = Expression.Equal(
                currentTenantIdProperty,
                Expression.Constant(null, typeof(Guid?))
            );

            MemberExpression tenantIdProperty = Expression.Property(entityParameter, "TenantId");

            UnaryExpression tenantIdAsNullable = Expression.Convert(tenantIdProperty, typeof(Guid?));

            BinaryExpression tenantMatchExpression = Expression.Equal(tenantIdAsNullable, currentTenantIdProperty);

            BinaryExpression tenantFilterExpression = Expression.OrElse(
                ignoreTenantFilterProperty,
                Expression.OrElse(currentTenantIsNullExpression, tenantMatchExpression)
            );

            filterExpression =
                filterExpression == null ? tenantFilterExpression : Expression.AndAlso(filterExpression, tenantFilterExpression);
        }

        if (filterExpression == null)
            return;

        Expression<Func<TEntity, bool>> lambda = Expression.Lambda<Func<TEntity, bool>>(filterExpression, entityParameter);

        modelBuilder.Entity<TEntity>().HasQueryFilter(lambda);
    }
}

internal static class TypeExtensions
{
    public static bool IsAssignableFromGenericType(this Type genericType, Type givenType)
    {
        if (givenType == null || genericType == null)
            return false;

        return givenType == genericType
            || givenType.MapsToGenericTypeDefinition(genericType)
            || givenType.HasInterfaceThatMapsToGenericTypeDefinition(genericType)
            || (givenType.BaseType != null && genericType.IsAssignableFromGenericType(givenType.BaseType));
    }

    private static bool MapsToGenericTypeDefinition(this Type givenType, Type genericType)
    {
        return genericType.IsGenericTypeDefinition && givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType;
    }

    private static bool HasInterfaceThatMapsToGenericTypeDefinition(this Type givenType, Type genericType)
    {
        return givenType.GetInterfaces().Where(it => it.IsGenericType).Any(it => it.GetGenericTypeDefinition() == genericType);
    }
}
