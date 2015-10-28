using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Runtime.Serialization;
using CSC3045.Agile.Business.Entities;
using Core.Common.Contracts;

namespace CSC3045.Agile.Data
{
    // ORM Rules
    public class CSC3045AgileContext : DbContext
    {
        public CSC3045AgileContext()
            : base("name=CSC3045Agile")
        {
            Database.SetInitializer<CSC3045AgileContext>(null);
        }

        public DbSet<Account> AccountSet { get; set; }

        public DbSet<Backlog> BacklogSet { get; set; }

        public DbSet<Project> ProjectSet { get; set; }

        public DbSet<Sprint> SprintSet { get; set; }

        public DbSet<AcceptanceCriteria> AcceptanceCriteriaSet { get; set; }

        public DbSet<BurndownPoint> BurndownPointSet { get; set; }

        public DbSet<PlanningPokerSession> PlanningPokerSessionSet { get; set; }

        public DbSet<PlanningPokerStory> PlanningPokerStorySet { get; set; }

        public DbSet<StoryStatus> StoryStatusSet { get; set; }

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
                .HasKey<int>(e => e.AccountId).Ignore(e => e.EntityId)
                .HasMany(x => x.UserRoles)
                .WithMany(x => x.Accounts)
                .Map(x =>
                {
                    x.ToTable("AccountUserRole");
                    x.MapLeftKey("AccountId");
                    x.MapRightKey("UserRoleId");
                });

            modelBuilder.Entity<Backlog>().HasKey<int>(e => e.BacklogId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Project>().HasKey<int>(e => e.ProjectId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Sprint>().HasKey<int>(e => e.SprintId).Ignore(e => e.EntityId);
            modelBuilder.Entity<AcceptanceCriteria>().HasKey<int>(e => e.AcceptanceCriteriaId).Ignore(e => e.EntityId);
            modelBuilder.Entity<BurndownPoint>().HasKey<int>(e => e.BurndownPointId).Ignore(e => e.EntityId);
            modelBuilder.Entity<PlanningPokerSession>().HasKey<int>(e => e.PlanningPokerSessionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<PlanningPokerStory>().HasKey<int>(e => e.PlanningPokerStoryId).Ignore(e => e.EntityId);
            modelBuilder.Entity<StoryStatus>().HasKey<int>(e => e.StoryStatusId).Ignore(e => e.EntityId);
            modelBuilder.Entity<StoryTask>().HasKey<int>(e => e.StoryTaskId).Ignore(e => e.EntityId);
            modelBuilder.Entity<UserRole>().HasKey<int>(e => e.UserRoleId).Ignore(e => e.EntityId);
            modelBuilder.Entity<UserStory>().HasKey<int>(e => e.UserStoryId).Ignore(e => e.EntityId);
        }
    }
}
