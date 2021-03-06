﻿using System.ComponentModel;
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
            base.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Account> AccountSet { get; set; }

        public DbSet<Project> ProjectSet { get; set; }

        public DbSet<Sprint> SprintSet { get; set; }

        public DbSet<AcceptanceCriteria> AcceptanceCriteriaSet { get; set; }

        public DbSet<Criteria> CriteriaSet { get; set; }

        public DbSet<Burndown> BurndownSet { get; set; }

        public DbSet<BurndownPoint> BurndownPointSet { get; set; }

        public DbSet<TaskBurndownPoint> TaskBurndownPointSet { get; set; }

        public DbSet<PlanningPokerSession> PlanningPokerSessionSet { get; set; }

        public DbSet<ChatMessage> ChatMessageSet { get; set; }

        public DbSet<CurrentStatus> CurrentStatusSet { get; set; }

        public DbSet<StoryTask> StoryTaskSet { get; set; }

        public DbSet<UserRole> UserRoleSet { get; set; }

        public DbSet<Skill> SkillSet { get; set; }

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

            modelBuilder.Entity<Project>()
                .HasKey(e => e.ProjectId).Ignore(e => e.EntityId);

            // 25/11/15 - The day I hated entity framework...
            modelBuilder.Entity<Project>()
                .HasMany(q => q.AllUsers)
                .WithMany(q => q.UserFor)
                .Map(q =>
                {
                    q.ToTable("AllUsers");
                    q.MapLeftKey("ProjectId");
                    q.MapRightKey("AccountId");
                });

            modelBuilder.Entity<Project>()
                .HasMany(q => q.ScrumMasters)
                .WithMany(q => q.ScrumMasterFor)
                .Map(q =>
                {
                    q.ToTable("ScrumMasters");
                    q.MapLeftKey("ProjectId");
                    q.MapRightKey("AccountId");
                });

            modelBuilder.Entity<Project>()
                .HasMany(q => q.Developers)
                .WithMany(q => q.DeveloperFor)
                .Map(q =>
                {
                    q.ToTable("Developers");
                    q.MapLeftKey("ProjectId");
                    q.MapRightKey("AccountId");
                });

              // 03/12/2015 - The day I agreed 
              modelBuilder.Entity<Project>()
                  .HasMany((q => q.Sprints))
                  .WithMany(q => q.SprintFor)
                  .Map(q =>
                  {
                      q.ToTable("Sprints");
                      q.MapLeftKey("ProjectId");
                      q.MapRightKey("SprintId");
                  });
    
            modelBuilder.Entity<Project>()
                .HasMany(q => q.BacklogStories);
                
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

            modelBuilder.Entity<ChatMessage>()
                .HasKey(e => e.MessageId).Ignore(e => e.EntityId);

            modelBuilder.Entity<CurrentStatus>()
                .HasKey(e => e.CurrentStatusId).Ignore(e => e.EntityId);

            modelBuilder.Entity<StoryTask>()
                .HasKey(e => e.StoryTaskId).Ignore(e => e.EntityId);

            modelBuilder.Entity<TaskBurndownPoint>()
               .HasKey(e => e.TaskBurndownPointId).Ignore(e => e.EntityId);

            modelBuilder.Entity<UserRole>()
                .HasKey(e => e.UserRoleId).Ignore(e => e.EntityId);

            modelBuilder.Entity<Skill>()
                .HasKey(e => e.SkillId).Ignore(e => e.EntityId);

            modelBuilder.Entity<UserStory>()
                .HasKey(e => e.UserStoryId).Ignore(e => e.EntityId);
        }
    }
}