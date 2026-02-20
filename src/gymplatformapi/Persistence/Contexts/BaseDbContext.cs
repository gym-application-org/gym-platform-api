using System.Reflection;
using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Contexts;

public class BaseDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }
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

    public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration)
        : base(dbContextOptions)
    {
        Configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}
