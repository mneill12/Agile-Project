﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ClientDesktop.Views;
using Core.Common.Contracts;
using Core.Common.UI.Core;
using CSC3045.Agile.Client.Contracts;
using Prism.Regions;
using System.Drawing.Imaging;
using System.IO;
using Point = System.Windows.Point;
using Size = System.Windows.Size;

namespace ClientDesktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SprintBurndownChartViewModel : ViewModelBase
    {
        private Dictionary<DateTime, int> _RawBurndownData;
        private IRegionManager _RegionManager;
        private ObservableCollection<GraphElement> _BurndownData;
        private String _SprintId;

        private readonly IServiceFactory _ServiceFactory;

        [ImportingConstructor]
        public SprintBurndownChartViewModel(IServiceFactory serviceFactory, IRegionManager regionManager)
        {
            _ServiceFactory = serviceFactory;
            _RegionManager = regionManager;
            NavigateDashboardCommand = new DelegateCommand<object>(NavigateDashboard);
            ChangeGraphData = new DelegateCommand<object>(GetSprintBurndownData);
            SendEmailCommand = new DelegateCommand<object>(SendBurndownAsEmail);

            BurndownData = new ObservableCollection<GraphElement>();
        }

     

        #region bindings

        public Dictionary<DateTime, int> RawBurndownData
        {
            get { return _RawBurndownData; }
            set
            {
                if (_RawBurndownData == value) return;
                _RawBurndownData = value;
                OnPropertyChanged("RawBurndownData");
            }
        }

        public ObservableCollection<GraphElement> BurndownData
        {
            get { return _BurndownData; }
            set
            {
                if (_BurndownData == value) return;
                _BurndownData = value;
                OnPropertyChanged("BurndownData");
            }
        }

        public String SprintId
        {
            get { return _SprintId; }
            set
            {
                if (_SprintId == value) return;
                _SprintId = value;
                OnPropertyChanged("SprintId");
            }
        }

        #endregion

        #region delegateCommands 

        private readonly DelegateCommand<GraphElement> _GraphData;
        public DelegateCommand<GraphElement> graphData { get { return _GraphData; } }
        public DelegateCommand<object> ChangeGraphData { get; private set; }
        public DelegateCommand<object> SendChart { get; private set; }
        public DelegateCommand<object> NavigateDashboardCommand { get; set; }
        public DelegateCommand<object> SendEmailCommand { get; set; }

        private void NavigateDashboard(object parameter)
        {
            _RegionManager.RequestNavigate(RegionNames.Content, typeof(DashboardView).FullName);
        }

        #endregion delegateCommands

        protected void GetSprintBurndownData(object obj)
        {

            BurndownData.Clear();

            WithClient(_ServiceFactory.CreateClient<IBurndownService>(), burndownClient =>
            {                                          
                RawBurndownData = burndownClient.GetHourDateMapForSprintId(int.Parse(_SprintId));
                CollectionViewSource.GetDefaultView(RawBurndownData).Refresh();
            });

            foreach (var item in RawBurndownData)
            {
                BurndownData.Add(new GraphElement(item.Key, item.Value));
            }
        }

        public static void SendBurndownAsEmail(object obj)
        {
            try
            {
                List<String> emailToAddress = new List<String>();
                emailToAddress.Add("mneill12@qub.ac.uk");
                emailToAddress.Add("mmccann71@qub.ac.uk");

                MailMessage mm = new MailMessage();

                StringBuilder emailBody = new StringBuilder();
                emailBody.AppendLine("Attached is a burndown chart sent to you from CSC3045CS7.\n\n");
                emailBody.AppendLine("Regards,");
                emailBody.AppendLine("The JELLO Team.");

                mm.From = new MailAddress("csc3045cs7@gmail.com");
                foreach (String address in emailToAddress)
                {
                    mm.To.Add(address);
                }
                mm.Subject = "JELLO - Burndown Chart";
                mm.Body = emailBody.ToString();

                SmtpClient sC = new SmtpClient("smtp.gmail.com");
                sC.Port = 587;
                sC.Credentials = new NetworkCredential("csc3045cs7@gmail.com", "setphaserstoscrum");
                sC.EnableSsl = true;
                sC.Send(mm);
            }
            catch (Exception e)
            {
              
            }
        }

       

    }





    public class GraphElement
    {
        public DateTime X { get; set; }
        public double Y { get; set; }
       
        public GraphElement(DateTime x,double y)
        {
            X = x;
            Y = y;           
        }      
    }

  
}