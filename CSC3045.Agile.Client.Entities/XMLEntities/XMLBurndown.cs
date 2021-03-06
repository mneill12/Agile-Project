﻿using System.Collections.Generic;
using Core.Common.Core;

namespace CSC3045.Agile.Client.Entities.XMLEntities
{
    public class XMLBurndown 
    {
        private int _BurndownId;
        private string _BurndownName;
        private List<BurndownPoint> _BurndownPoints;

        public int BurndownId
        {
            get { return _BurndownId; }
            set
            {
                if (_BurndownId != value)
                {
                    _BurndownId = value;
                   
                }
            }
        }

        public string BurndownName
        {
            get { return _BurndownName; }
            set
            {
                if (_BurndownName != value)
                {
                    _BurndownName = value;
                  
                }
            }
        }

        public List<BurndownPoint> BurndownPoints
        {
            get { return _BurndownPoints; }
            set
            {
                if (_BurndownPoints != value)
                {
                    _BurndownPoints = value;
                    
                }
            }
        }
    }
}