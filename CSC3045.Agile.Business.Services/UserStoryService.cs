using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using CSC3045.Agile.Business.Contracts.Service_Contracts;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;

namespace CSC3045.Agile.Business.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple,
        ReleaseServiceInstanceOnTransactionComplete = false)]
    public class UserStoryService : ServiceBase, IUserStoryService
    {

        [Import]
        private IDataRepositoryFactory _DataRepositoryFactory;

        public UserStoryService()
        {
        }

        public UserStoryService(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        public UserStory AddNewUserStory(UserStory userStory)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var userStoryRepository = _DataRepositoryFactory.GetDataRepository<IUserStoryRepository>();

                return userStoryRepository.Add(userStory);
            });
        }
    }
}
