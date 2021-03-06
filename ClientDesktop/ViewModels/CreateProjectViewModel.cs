﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Windows.Data;
using ClientDesktop.Views;
using Core.Common;
using Core.Common.Contracts;
using Core.Common.UI.Core;
using CSC3045.Agile.Client.Contracts;
using CSC3045.Agile.Client.Entities;
using Prism.Regions;

namespace ClientDesktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProjectOverviewViewModel : ViewModelBase 
    {
        private DateTime _ProjectStartDate;
        private string _ProjectName;

        private string _ProductOwnerSearchText;
        private string _ScrumMasterSearchText;
        private string _DeveloperSearchText;
        private string _DeveloperSkillSearchText;

        private List<Account> _ProductOwners;
        private List<Account> _ScrumMasters;
        private List<Account> _Developers;

        private Account _SelectedProductOwner;

        private readonly IServiceFactory _ServiceFactory;
        private readonly IRegionManager _RegionManager;

        [ImportingConstructor]
        public ProjectOverviewViewModel(IServiceFactory serviceFactory, IRegionManager regionManager)
        {
            _ServiceFactory = serviceFactory;
            _RegionManager = regionManager;

            CreateProjectCommand = new DelegateCommand<object>(OnCreateProject);
            SearchScrumMasterCommand = new DelegateCommand<object>(SearchScrumMaster);
            SearchProductOwnerCommand = new DelegateCommand<object>(SearchProductOwner);
            SearchDeveloperCommand = new DelegateCommand<object>(SearchDeveloper);
            NavigateDashboardCommand = new DelegateCommand<object>(NavigateDashboard);

            ProductOwners = new List<Account>();
            ScrumMasters = new List<Account>();
            Developers = new List<Account>();

            ProjectStartDate = DateTime.Now;
        }

        public DelegateCommand<object> CreateProjectCommand { get; private set; }
        public DelegateCommand<object> SearchProductOwnerCommand { get; private set; }
        public DelegateCommand<object> SearchScrumMasterCommand { get; private set; }
        public DelegateCommand<object> SearchDeveloperCommand { get; private set; }
        public DelegateCommand<object> NavigateDashboardCommand { get; set; }

        private void NavigateDashboard(object parameter)
        {
            _RegionManager.RequestNavigate(RegionNames.Content, typeof(DashboardView).FullName);
        }


        #region View Bindings

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

        public DateTime ProjectStartDate
        {
            get { return _ProjectStartDate; }
            set
            {
                if (_ProjectStartDate == value) return;
                _ProjectStartDate = value;
                OnPropertyChanged("ProjectStartDate");
            }
        }

        public string ProductOwnerSearchText
        {
            get
            {
                return _ProductOwnerSearchText;
            }
            set
            {
                if (_ProductOwnerSearchText == value) return;
                _ProductOwnerSearchText = value;
                OnPropertyChanged("ProductOwnerSearchText");
            }
        }

        public string ScrumMasterSearchText
        {
            get
            {
                return _ScrumMasterSearchText;
            }
            set
            {
                if (_ScrumMasterSearchText == value) return;
                _ScrumMasterSearchText = value;
                OnPropertyChanged("ScrumMasterSearchText");
            }
        }

        public string DeveloperSearchText
        {
            get
            {
                return _DeveloperSearchText;
            }
            set
            {
                if (_DeveloperSearchText == value) return;
                _DeveloperSearchText = value;
                OnPropertyChanged("DeveloperSearchText");
            }
        }

        public string DeveloperSkillSearchText
        {
            get
            {
                return _DeveloperSkillSearchText;
            }
            set
            {
                if (_DeveloperSkillSearchText == value) return;
                _DeveloperSkillSearchText = value;
                OnPropertyChanged("DeveloperSkillSearchText");
            }
        }

        public List<Account> ProductOwners
        {
            get { return _ProductOwners; }
            set
            {
                if (_ProductOwners == value) return;
                _ProductOwners = value;
                OnPropertyChanged("ProductOwners");
            }
        }

        public List<Account> Developers
        {
            get { return _Developers; }
            set
            {
                if (_Developers == value) return;
                _Developers = value;
                OnPropertyChanged("Developers");
            }
        }

        public List<Account> ScrumMasters
        {
            get { return _ScrumMasters; }
            set
            {
                if (_ScrumMasters == value) return;
                _ScrumMasters = value;
                OnPropertyChanged("ScrumMasters");
            }
        }

        public Account SelectedProductOwner
        {
            get { return _SelectedProductOwner; }
            set
            {
                if (_SelectedProductOwner == value) return;
                _SelectedProductOwner = value;
                OnPropertyChanged("SelectedProductOwner");
            }
        }

        #endregion

        public event EventHandler<ErrorMessageEventArgs> ErrorOccured;

        // This gets hit every time the page is loaded while the constructor only gets loaded initially, use for getting up-to-data from the database
        protected override void OnViewLoaded()
        {
            GetInitialUsers();
        }

        protected void SearchScrumMaster(object obj)
        {
            WithClient(_ServiceFactory.CreateClient<IAccountService>(), accountClient =>
            {
                var searchedScrumMasters = accountClient.GetByRoleAndEmail(ViewModelConstants.Scrummaster, ScrumMasterSearchText);

                ScrumMasters.Clear();

                if (null != searchedScrumMasters && searchedScrumMasters.Count != 0)
                {
                    ScrumMasters.AddRange(searchedScrumMasters);
                }
                else
                {
                    ScrumMasters.AddRange(accountClient.GetByUserRole(ViewModelConstants.Scrummaster));
                }

                CollectionViewSource.GetDefaultView(ScrumMasters).Refresh();
            });
        }

        protected void SearchProductOwner(object obj)
        {
            WithClient(_ServiceFactory.CreateClient<IAccountService>(), accountClient =>
            {
                var searchedProductOwners = accountClient.GetByRoleAndEmail(ViewModelConstants.ProductOwner, ProductOwnerSearchText);

                ProductOwners.Clear();

                if (null != searchedProductOwners && searchedProductOwners.Count != 0)
                {
                    ProductOwners.AddRange(searchedProductOwners);
                }
                else
                {
                    ProductOwners.AddRange(accountClient.GetByUserRole(ViewModelConstants.ProductOwner));
                }

                CollectionViewSource.GetDefaultView(ProductOwners).Refresh();
            });
        }

        /// <summary>
        /// Special developer search method to allow searching by skill set.
        /// </summary>
        protected void SearchDeveloper(object obj)
        {
            WithClient(_ServiceFactory.CreateClient<IAccountService>(), accountClient =>
            {
                var searchedDevelopers = accountClient.GetByRoleAndEmail(ViewModelConstants.Developer, DeveloperSearchText);

                Developers.Clear();

                if (null != searchedDevelopers && searchedDevelopers.Count != 0)
                {
                    Developers.AddRange(searchedDevelopers);
                }
                else
                {
                    Developers.AddRange(accountClient.GetByUserRole(ViewModelConstants.Developer));
                }

                CollectionViewSource.GetDefaultView(Developers).Refresh();

            });
        }

        protected void GetInitialUsers()
        {
            WithClient(_ServiceFactory.CreateClient<IAccountService>(), accountClient =>
            {
                var productOwners = accountClient.GetByUserRole(ViewModelConstants.ProductOwner);
                var scrumMasters = accountClient.GetByUserRole(ViewModelConstants.Scrummaster);
                var developers = accountClient.GetByUserRole(ViewModelConstants.Developer);
                
                if (null != productOwners && productOwners.Any())
                {
                    ProductOwners.AddRange(productOwners);
                }

                if (scrumMasters != null && scrumMasters.Any())
                {
                    ScrumMasters.AddRange(scrumMasters);
                }

                if (developers != null && developers.Any())
                {
                    Developers.AddRange(developers);
                }
            });
        }

        protected void OnCreateProject(object parameter)
        {
            // Hackiest code ever, WPF does not support passing multiple arrays in one command with MVVM so we
            // use a shallow copy of an array converted using the MultiValueConverter to allow a deletgate
            // command then convert back here to an array, values changed here must be updated in the View.xaml
            var values = (object[])parameter;

            IList selectedScrumMasters = values[0] as IList;
            IList selectedDevelopers = values[1] as IList;

            List<Account> scrumMasters = new List<Account>();
            List<Account> developers = new List<Account>();

            if (selectedScrumMasters != null && selectedDevelopers != null)
            {
                scrumMasters = selectedScrumMasters.Cast<Account>().ToList();
                developers = selectedDevelopers.Cast<Account>().ToList();
            }

            if (ProjectName != null && SelectedProductOwner != null && scrumMasters.Any() && developers.Any())
            {
                // Add PO, PM, SMs and Devs to all project users
                var allUsers = new Collection<Account> {SelectedProductOwner, GlobalCommands.MyAccount};
                allUsers.AddRange(scrumMasters);
                allUsers.AddRange(developers);

                try
                {
                    var project = new Project
                    {
                        ProjectName = ProjectName,
                        ProjectStartDate = ProjectStartDate,
                        ProjectManager = GlobalCommands.MyAccount,
                        ProductOwner = SelectedProductOwner,
                        ScrumMasters = scrumMasters,
                        Developers = developers,
                        AllUsers = allUsers,
                        BacklogStories = new List<UserStory>()
                    };

                    Project createdProject = null;

                    WithClient(_ServiceFactory.CreateClient<IProjectService>(), projectClient =>
                    {
                        createdProject = projectClient.CreateProject(project);
                    });
                    
                    //if (createdProject != null)
                    //{
                    //    ServiceLocator.Current.GetInstance<DashboardViewModel>().CurrentProjectId = createdProject.ProjectId;
                    //}
                    
                    _RegionManager.RequestNavigate(RegionNames.Content, typeof(DashboardView).FullName);
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

    }
}