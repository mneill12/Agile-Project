using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using CSC3045.Agile.Business.Entities;

namespace CSC3045.Agile.Data
{
    /// <summary>
    ///     ORM Rules
    ///     DropCreateDB on model change by default, if specificed create sample data
    /// </summary>
    public class Csc3045AgileContext : DbContext
    {
        public Csc3045AgileContext()
            : base("CSC3045GeneratedDB")
        {
            Database.SetInitializer(new Csc3045AgileCustomDatabaseInitialiser());
        }

        public DbSet<Account> AccountSet { get; set; }

        public DbSet<Backlog> BacklogSet { get; set; }

        public DbSet<Project> ProjectSet { get; set; }

        public DbSet<Sprint> SprintSet { get; set; }

        public DbSet<AcceptanceCriteria> AcceptanceCriteriaSet { get; set; }

        public DbSet<Criteria> CriteriaSet { get; set; }

        public DbSet<Burndown> BurndownSet { get; set; }

        public DbSet<BurndownPoint> BurndownPointSet { get; set; }

        public DbSet<PlanningPokerSession> PlanningPokerSessionSet { get; set; }

        public DbSet<CurrentStatus> StoryStatusSet { get; set; }

        public DbSet<StoryTask> StoryTaskSet { get; set; }

        public DbSet<UserRole> UserRoleSet { get; set; }

        public DbSet<UserStory> UserStorySet { get; set; }

        // Entity by default links to plural tables (e.g. Account to Accounts) so we disable this
        // We also want ignore EntityId
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Ignore<PropertyChangedEventHandler>();
            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();

            modelBuilder.Entity<Account>()
                .HasKey(e => e.AccountId).Ignore(e => e.EntityId);

            modelBuilder.Entity<Backlog>()
                .HasKey(e => e.BacklogId).Ignore(e => e.EntityId);

            modelBuilder.Entity<Project>()
                .HasKey(e => e.ProjectId).Ignore(e => e.EntityId);

            modelBuilder.Entity<Sprint>()
                .HasKey(e => e.SprintId).Ignore(e => e.EntityId);

            modelBuilder.Entity<Criteria>()
                .HasKey(e => e.CriteriaId).Ignore(e => e.EntityId);

            modelBuilder.Entity<AcceptanceCriteria>()
                .HasKey(e => e.AcceptanceCriteriaId).Ignore(e => e.EntityId);

            modelBuilder.Entity<Burndown>()
                .HasKey(e => e.BurndownId).Ignore(e => e.EntityId);

            modelBuilder.Entity<BurndownPoint>()
                .HasKey(e => e.BurndownPointId).Ignore(e => e.EntityId);

            modelBuilder.Entity<PlanningPokerSession>()
                .HasKey(e => e.PlanningPokerSessionId).Ignore(e => e.EntityId);

            modelBuilder.Entity<CurrentStatus>()
                .HasKey(e => e.StoryStatusId).Ignore(e => e.EntityId);

            modelBuilder.Entity<StoryTask>()
                .HasKey(e => e.StoryTaskId).Ignore(e => e.EntityId);

            modelBuilder.Entity<UserRole>()
                .HasKey(e => e.UserRoleId).Ignore(e => e.EntityId);

            modelBuilder.Entity<UserStory>()
                .HasKey(e => e.UserStoryId).Ignore(e => e.EntityId);

            modelBuilder.Entity<UserStory>()
                .HasKey(e => e.UserStoryId).Ignore(e => e.EntityId);
        }
    }
}