﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using CSC3045.Agile.Business.Entities;

namespace CSC3045.Agile.Data.Contracts.Repository_Interfaces
{
    // Interface for custom methods of StoryStatusRepository
    public interface IStoryStatusRepository : IDataRepository<CurrentStatus>
    {
       
    }
}