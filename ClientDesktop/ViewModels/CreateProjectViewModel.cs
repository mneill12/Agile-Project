using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Windows.Controls;
using System.Windows.Data;
using Core.Common;
using Core.Common.Contracts;
using Core.Common.UI.Core;
using CSC3045.Agile.Client.Contracts;
using CSC3045.Agile.Client.Entities;
using ClientDesktop;

namespace ClientDesktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProjectOverviewViewModel : ViewModelBase, INotifyPropertyChanged 
    {

        private List<ProductOwnerScrumMasterInfo> _ProductOwners;
        private string _ProjectDeadline;

        private string _ProjectName;
        private List<ProductOwnerScrumMasterInfo> _ScrumMasters;
        private List<ProductOwnerScrumMasterInfo> _AllUsers;
        private readonly IServiceFactory _ServiceFactory;

        public event PropertyChangedEventHandler PropertyChanged;

        [ImportingConstructor]
        public ProjectOverviewViewModel(IServiceFactory serviceFactory)
        {
            _ServiceFactory = serviceFactory;
            CreateProject = new DelegateCommand<TextBox>(OnCreateProject);
            SearchScrumMasterCommand = new DelegateCommand<TextBox>(SearchScrumMasterByName);
            _ProductOwners = new List<ProductOwnerScrumMasterInfo>();
            _ScrumMasters = new List<ProductOwnerScrumMasterInfo>();
            _AllUsers = new List<ProductOwnerScrumMasterInfo>();
        }

        public DelegateCommand<TextBox> CreateProject { get; private set; }
        public DelegateCommand<TextBox> SearchScrumMasterCommand { get; private set; } 

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

        public List<ProductOwnerScrumMasterInfo> AllUsers
        {
            get { return _AllUsers; }
            set
            {
                if (_AllUsers == value) return;
                _AllUsers = value;
                OnPropertyChanged("AllUsers");
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
            GetProductOwnersAndScrumMasters();
            
        }

        protected void SearchScrumMasterByName(TextBox textBox)
        {
            WithClient(_ServiceFactory.CreateClient<IAccountService>(), accountClient =>
            {
                
                var searchedScrumMasters = accountClient.GetByRoleAndEmail(ViewModelConstants.Scrummaster, textBox.Text);
                
                if (null != searchedScrumMasters && searchedScrumMasters.Count != 0)
                {
                    _ScrumMasters.Clear();

                    foreach (var a in searchedScrumMasters)
                    {
                        _ScrumMasters.Add(new ProductOwnerScrumMasterInfo(a.FirstName, a.LastName, a.LoginEmail));
                    }
                }
                else
                {
                    _ScrumMasters.Clear();
                    _ScrumMasters.Add(new ProductOwnerScrumMasterInfo("No Users Found", "", ""));
                }

                ICollectionView view = CollectionViewSource.GetDefaultView(ScrumMasters);
                view.Refresh();

            });
        }


        protected void GetProductOwnersAndScrumMasters()
        {
            WithClient(_ServiceFactory.CreateClient<IAccountService>(), accountClient =>
            {
               
                var productOwners = accountClient.GetByUserRole(ViewModelConstants.ProductOwner);
                var scrumMasters = accountClient.GetByUserRole(ViewModelConstants.Scrummaster);
                var allUsers = accountClient.GetAllAccounts();

                if (allUsers != null)
                {
                    foreach (var a in allUsers)
                    {
                        _AllUsers.Add(new ProductOwnerScrumMasterInfo(a.FirstName, a.LastName, a.LoginEmail));
                    }
                }

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
                        ProjectDeadline = DateTime.Now
                            //DateTime.ParseExact(ProjectDeadline, "dd/mm/yyyy", CultureInfo.InvariantCulture)
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