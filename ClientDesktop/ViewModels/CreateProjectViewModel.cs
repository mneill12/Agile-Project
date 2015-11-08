using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using System.ServiceModel;
using System.Windows.Controls;
using Core.Common;
using Core.Common.Contracts;
using Core.Common.UI.Core;
using CSC3045.Agile.Client.Contracts;
using CSC3045.Agile.Client.Entities;

namespace ClientDesktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CreateProjectViewModel : ViewModelBase
    {
        private List<ProductOwnerScrumMasterInfo> _ProductOwners;
        private string _ProjectDeadline;

        private string _ProjectName;
        private List<ProductOwnerScrumMasterInfo> _ScrumMasters;
        private readonly IServiceFactory _ServiceFactory;

        [ImportingConstructor]
        public CreateProjectViewModel(IServiceFactory serviceFactory)
        {
            _ServiceFactory = serviceFactory;
            CreateProject = new DelegateCommand<TextBox>(OnCreateProject);
            _ProductOwners = new List<ProductOwnerScrumMasterInfo>();
            _ScrumMasters = new List<ProductOwnerScrumMasterInfo>();
        }

        public DelegateCommand<TextBox> CreateProject { get; private set; }

        public string ProjectName
        {
            get { return _ProjectName; }
            set
            {
                if (_ProjectName == value) return;
                _ProjectName = value;
                OnPropertyChanged("ProjectName");
            }
        }

        public string ProjectDeadline
        {
            get { return _ProjectDeadline; }
            set
            {
                if (_ProjectDeadline == value) return;
                _ProjectDeadline = value;
                OnPropertyChanged("ProjectDeadline");
            }
        }

        public List<ProductOwnerScrumMasterInfo> ProductOwners
        {
            get { return _ProductOwners; }
            set
            {
                if (_ProductOwners == value) return;
                _ProductOwners = value;
                OnPropertyChanged("ProductOwners");
            }
        }

        public List<ProductOwnerScrumMasterInfo> ScrumMasters
        {
            get { return _ScrumMasters; }
            set
            {
                if (_ScrumMasters == value) return;
                _ScrumMasters = value;
                OnPropertyChanged("ScrumMasters");
            }
        }

        public event EventHandler<ErrorMessageEventArgs> ErrorOccured;

        // This gets hit every time the page is loaded while the constructor only gets loaded initially, use for getting up-to-data from the database
        protected override void OnViewLoaded()
        {
            getProductOwnersAndScrumMasters();
        }

        protected void getProductOwnersAndScrumMasters()
        {
            WithClient(_ServiceFactory.CreateClient<IAccountService>(), accountClient =>
            {
                var productOwnerRole = new UserRole {UserRoleName = "Product Owner"};
                var scrumMasterRole = new UserRole {UserRoleName = "Scrum Master"};

                var productOwners = accountClient.GetByUserRole(1);
                var scrumMasters = accountClient.GetByUserRole(2);

                if (productOwners != null)
                {
                    foreach (var a in productOwners)
                    {
                        _ProductOwners.Add(new ProductOwnerScrumMasterInfo(a.FirstName, a.LastName, a.LoginEmail));
                    }
                }

                if (scrumMasters != null)
                {
                    foreach (var a in scrumMasters)
                    {
                        _ScrumMasters.Add(new ProductOwnerScrumMasterInfo(a.FirstName, a.LastName, a.LoginEmail));
                    }
                }
            });
        }


        protected void OnCreateProject(TextBox textBox)
        {
            if (ProjectName != null && ProjectDeadline != null)
            {
                try
                {
                    var _Project = new Project
                    {
                        ProjectName = textBox.Text,
                        ProjectDeadline =
                            DateTime.ParseExact(ProjectDeadline, "dd/mm/yyyy", CultureInfo.InvariantCulture)
                    };

                    WithClient(_ServiceFactory.CreateClient<IProjectService>(), projectClient =>
                    {
                        var myProject = projectClient.AddProject(_Project);

                        if (myProject != null)
                        {
                            //ObjectBase.Container.GetExportedValue<DashboardViewModel>();
                        }
                    });
                }
                catch (FaultException ex)
                {
                    if (ErrorOccured != null)
                        ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));
                }
                catch (Exception ex)
                {
                    if (ErrorOccured != null)
                        ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));
                }
            }
        }

        public class ProductOwnerScrumMasterInfo
        {
            public ProductOwnerScrumMasterInfo(string fname, string sname, string email)
            {
                FirstName = fname;
                Surname = sname;
                EmailAddress = email;
            }

            public string FirstName { get; set; }
            public string Surname { get; set; }
            public string EmailAddress { get; set; }
        }
    }
}