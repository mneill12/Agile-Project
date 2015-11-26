using System.ComponentModel.Composition;
using Core.Common.UI.Core;

namespace ClientDesktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StatusBarViewModel : ViewModelBase
    {
        [ImportingConstructor]
        public StatusBarViewModel()
        {
            UpdateApplicationStatusCommand = new DelegateCommand<string>(UpdateApplicationStatus);
            UpdateConnectionStatusCommand = new DelegateCommand<string>(UpdateConnectionStatus);

            // Links the Update Login Status Command to be accessed globally
            GlobalCommands.ApplicationStatusText.RegisterCommand(UpdateApplicationStatusCommand);
            GlobalCommands.ConnectionStatusText.RegisterCommand(UpdateConnectionStatusCommand);
        }

        #region StatusBarBindings

        private string _ApplicationStatus;
        private string _ConnectionStatus;

        public string ApplicationStatus
        {
            get { return _ApplicationStatus; }
            set
            {
                if (_ApplicationStatus == value) return;
                _ApplicationStatus = value;
                OnPropertyChanged("ApplicationStatus");
            }
        }

        public string ConnectionStatus
        {
            get { return _ConnectionStatus; }
            set
            {
                if (_ConnectionStatus == value) return;
                _ConnectionStatus = value;
                OnPropertyChanged("ConnectionStatus");
            }
        }

        #endregion

        #region Delegate Commands

        public DelegateCommand<string> UpdateApplicationStatusCommand { get; set; }
        public DelegateCommand<string> UpdateConnectionStatusCommand { get; set; }

        #endregion

        private void UpdateApplicationStatus(string applicationStatusText)
        {
            ApplicationStatus = applicationStatusText;
        }

        private void UpdateConnectionStatus(string connectionStatusText)
        {
            ConnectionStatus = connectionStatusText;
        }
    }
}