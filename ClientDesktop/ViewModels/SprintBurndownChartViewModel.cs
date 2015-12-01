using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Data;
using Core.Common.Contracts;
using Core.Common.UI.Core;
using CSC3045.Agile.Client.Contracts;
using Prism.Regions;

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
            ChangeGraphData = new DelegateCommand<object>(GetSprintBurndownData);

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

        #endregion delegateCommands

        protected void GetSprintBurndownData(object obj)
        {
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