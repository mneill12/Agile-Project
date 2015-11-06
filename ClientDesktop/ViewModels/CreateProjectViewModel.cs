using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using Core.Common;
using Core.Common.Contracts;
using Core.Common.Core;
using Core.Common.UI.Core;
using CSC3045.Agile.Client.Contracts;
using CSC3045.Agile.Client.Entities;
using System.Globalization;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Data;

namespace ClientDesktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CreateProjectViewModel : ViewModelBase
    {
        IServiceFactory _ServiceFactory;

        [ImportingConstructor]
        public CreateProjectViewModel(IServiceFactory serviceFactory)
        {
            _ServiceFactory = serviceFactory;
            CreateProject = new DelegateCommand<TextBox>(OnCreateProject);
            _ProductOwners = new List<ProductOwnerScrumMasterInfo>();
            _ScrumMasters = new List<ProductOwnerScrumMasterInfo>();
        }

        public DelegateCommand<TextBox> CreateProject {get; private set;}

        private string _ProjectName;
        private string _ProjectDeadline;
        private List<ProductOwnerScrumMasterInfo> _ProductOwners;
        private List<ProductOwnerScrumMasterInfo> _ScrumMasters;

        public string ProjectName
        {
            get
            {
                return _ProjectName;
            }
            set
            {
                if (_ProjectName == value) return;
                _ProjectName = value;
                OnPropertyChanged("ProjectName");
            }
        }

        public string ProjectDeadline
        {
            get
            {
                return _ProjectDeadline;
            }
            set
            {
                if (_ProjectDeadline == value) return;
                _ProjectDeadline = value;
                OnPropertyChanged("ProjectDeadline");
            }
        }

        public List<ProductOwnerScrumMasterInfo> ProductOwners
        {
            get
            {
                return _ProductOwners;
            }
            set
            {
                if (_ProductOwners == value) return;
                _ProductOwners = value;
                OnPropertyChanged("ProductOwners");
            }
        }

        public List<ProductOwnerScrumMasterInfo> ScrumMasters
        {
            get
            {
                return _ScrumMasters;
            }
            set
            {
                if (_ScrumMasters == value) return;
                _ScrumMasters = value;
                OnPropertyChanged("ScrumMasters");
            }
        }

        public class ProductOwnerScrumMasterInfo
        {
            public string FirstName { get; set; }
            public string Surname { get; set; }
            public string EmailAddress { get; set; }

            public ProductOwnerScrumMasterInfo(string fname, string sname, string email)
            {
                FirstName = fname;
                Surname = sname;
                EmailAddress = email;
            }
        }

        public event EventHandler<ErrorMessageEventArgs> ErrorOccured;

        public override string ViewTitle
        {
            get { return "Create Project"; }
        }

        // This gets hit every time the page is loaded while the constructor only gets loaded initially, use for getting up-to-data from the database
        protected override void OnViewLoaded()
        {
            getProductOwnersAndScrumMasters();
        }

        protected void getProductOwnersAndScrumMasters()
        {
            WithClient<IAccountService>(_ServiceFactory.CreateClient<IAccountService>(), accountClient =>
            {
                ICollection<Account> accounts = accountClient.GetAllAccountsWithUserRoles();
                UserRole productOwnerRole = new UserRole() { UserRoleId = 5, UserRoleName = "Product Owner", PermissionLevel = 3 };
                UserRole scrumMasterRole = new UserRole() { UserRoleId = 3, UserRoleName = "Scrum Master", PermissionLevel = 1 };

                IEnumerable<Account> projOwners = accountClient.GetByUserRole(3);
                IEnumerable<Account> scrumMasters = accountClient.GetByUserRole(1);

                if (projOwners != null)
                {
                    foreach (Account a in projOwners)
                    {
                        _ProductOwners.Add(new ProductOwnerScrumMasterInfo(a.FirstName, a.LastName, a.LoginEmail));
                    }
                }

                if (scrumMasters != null)
                {
                    foreach (Account a in scrumMasters)
                    {
                        _ScrumMasters.Add(new ProductOwnerScrumMasterInfo(a.FirstName, a.LastName, a.LoginEmail));
                    }
                }
            });
        }

        

        protected void OnCreateProject(TextBox textBox)
        {
            if(ProjectName != null && ProjectDeadline != null)
            {
                try
                {
                    Project _Project = new Project()
                    {
                        ProjectName = textBox.Text,
                        ProjectDeadline = DateTime.ParseExact(ProjectDeadline, "dd/mm/yyyy", CultureInfo.InvariantCulture)
                    };

                    WithClient<IProjectService>(_ServiceFactory.CreateClient<IProjectService>(), projectClient =>
                    {
                        Project myProject = projectClient.AddProject(_Project);

                        if (myProject != null)
                        {
                            //ObjectBase.Container.GetExportedValue<DashboardViewModel>();
                        }
                    });
                }
                catch(FaultException ex)
                {
                    if (ErrorOccured != null)
                        ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));
                }
                catch(Exception ex)
                {
                    if (ErrorOccured != null)
                        ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));
                }
            }
        }
    }
}
