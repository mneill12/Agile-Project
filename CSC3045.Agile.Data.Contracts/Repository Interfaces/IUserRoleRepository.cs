﻿using System.Collections.Generic;
using Core.Common.Contracts;
using CSC3045.Agile.Business.Entities;

namespace CSC3045.Agile.Data.Contracts.Repository_Interfaces
{
    // Interface for custom methods of UserRoleRepository
    public interface IUserRoleRepository : IDataRepository<UserRole>
    {
        IList<UserRole> GetAllUserRoles();
    }
}