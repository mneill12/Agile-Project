using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel;
using System.Data.Entity;
using CSC3045.Agile.Business.Entities;

namespace CSC3045.Agile.Data
{
    class CSC3045AgileCustomDatabaseInitialiser : DropCreateDatabaseIfModelChanges<CSC3045AgileContext>
    {
        protected override void Seed(CSC3045AgileContext context)
        {
            IList<Account> defaultAccounts = new List<Account>();

            defaultAccounts.Add(new Account() { AccountId = 1, LoginEmail = "jflynn@qub.ac.uk", Password = "4nt1t7!"});
            defaultAccounts.Add(new Account() { AccountId = 2, LoginEmail = "zeadie@qub.ac.uk", Password = "4nt1t7!"});
            defaultAccounts.Add(new Account() { AccountId = 3, LoginEmail = "rmeharg@qub.ac.uk", Password = "4nt1t7!" });
            defaultAccounts.Add(new Account() { AccountId = 4, LoginEmail = "nreid@qub.ac.uk", Password = "4nt1t7!" });
            defaultAccounts.Add(new Account() { AccountId = 5, LoginEmail = "zshen@qub.ac.uk", Password = "4nt1t7!" });
            defaultAccounts.Add(new Account() { AccountId = 6, LoginEmail = "mmcann@qub.ac.uk", Password = "4nt1t7!" });
            defaultAccounts.Add(new Account() { AccountId = 7, LoginEmail = "mneil@qub.ac.uk", Password = "4nt1t7!" });

            foreach (Account acc in defaultAccounts)
            {
                context.AccountSet.Add(acc);
            }


            IList<UserRole> defaultRoles = new List<UserRole>();

            defaultRoles.Add(new UserRole() { UserRoleId = 1, UserRoleName = "Developer", PermissionLevel = 0 });
            defaultRoles.Add(new UserRole() { UserRoleId = 2, UserRoleName = "Scrum Master", PermissionLevel = 1 });
            defaultRoles.Add(new UserRole() { UserRoleId = 3, UserRoleName = "Project Manager", PermissionLevel = 2 });
            defaultRoles.Add(new UserRole() { UserRoleId = 4, UserRoleName = "Product Owner", PermissionLevel = 3 });


            foreach (UserRole usr in defaultRoles) 
            { 
                context.UserRoleSet.Add(usr);
            }

            base.Seed(context);
        }
    }
}
