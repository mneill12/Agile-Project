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

        // Entity by default links to plural tables (e.g. Account to Accounts) so we disable this
        // We also want ignore EntityId
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Ignore<PropertyChangedEventHandler>();
            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();

            modelBuilder.Entity<Account>().HasKey<int>(e => e.AccountId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Backlog>().HasKey<int>(e => e.BacklogId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Project>().HasKey<int>(e => e.ProjectId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Sprint>().HasKey<int>(e => e.SprintId).Ignore(e => e.EntityId);
        }
    }
}
