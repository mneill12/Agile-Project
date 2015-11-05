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
        }

        public DelegateCommand<TextBox> CreateProject {get; private set;}

        private string _ProjectName;

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
                OnPropertyChanged("ProjectNameTextBox");
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

        }

        protected void OnCreateProject(TextBox textBox)
        {
            if(textBox.Text != null)
            {
                try
                {
                    Project _Project = new Project()
                    {
                        ProjectName = _ProjectName,
                        ProjectDeadline = new DateTime(2016,1,1)
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
