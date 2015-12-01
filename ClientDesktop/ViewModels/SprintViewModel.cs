using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Windows.Controls;
using ClientDesktop.Views;
using Core.Common;
using Core.Common.Contracts;
using Core.Common.UI.Core;
using Core.Common.Utils;
using CSC3045.Agile.Client.Contracts;
using CSC3045.Agile.Client.CustomPrinciples;
using CSC3045.Agile.Client.Entities;
using Prism.Regions;


namespace ClientDesktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SprintViewModel : ViewModelBase
    {

        #region SprintView Bindings

        private int _UserStoryId;
        private string _StoryNumber;
        private string _Description;
        private int _StoryPoint;
        private string _UserNotes;

        public int UserStoryId
        {
            get
            {
                return _UserStoryId;
            }
            set
            {
                if (_UserStoryId == value) return;
                _UserStoryId = value;
                OnPropertyChanged("UserStoryId");
            }
        }

        public string StoryNumber
        {
            get
            {
                return _StoryNumber;
            }
            set
            {
                if (_StoryNumber == value) return;
                _StoryNumber = value;
                OnPropertyChanged("StoryNumber");
            }
        }

        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                if (_Description == value) return;
                _Description = value;
                OnPropertyChanged("Description");
            }
        }

        public int StoryPoint
        {
            get
            {
                return _StoryPoint;
            }
            set
            {
                if (_StoryPoint == value) return;
                _StoryPoint = value;
                OnPropertyChanged("StoryPoint");
            }
        }

        public string UserNotes
        {
            get
            {
                return _UserNotes;
            }
            set
            {
                if (_UserNotes == value) return;
                _UserNotes = value;
                OnPropertyChanged("UserNotes");
            }
        }
        //Add list of user stories + scrum members + blocked tasks

        #endregion

        private IServiceFactory _ServiceFactory;
        private IRegionManager _RegionManager;



        [ImportingConstructor]
        public SprintViewModel(IServiceFactory serviceFactory, IRegionManager regionManager)
        {
            _ServiceFactory = serviceFactory;
            _RegionManager = regionManager;
        }


    }
}

