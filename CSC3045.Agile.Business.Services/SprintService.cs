using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using Core.Common.Contracts;
using Core.Common.Exceptions;
using Core.Common.Utils;
using CSC3045.Agile.Business.Contracts;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;
namespace CSC3045.Agile.Business.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple,
        ReleaseServiceInstanceOnTransactionComplete = false)]
   public class SprintService : ServiceBase, ISprintService
    {

        [Import] private IDataRepositoryFactory _DataRepositoryFactory;

        public SprintService()
        {

        }

        public SprintService(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        public ICollection<Sprint> GetSprintForProject(int projectId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var sprintRepository = _DataRepositoryFactory.GetDataRepository<ISprintRepository>();

                return sprintRepository.GetSprintForProject(projectId);
            });
        }


        public ICollection<Sprint> GetSprintForScrumMaster(int scrumMasterId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var sprintRepository = _DataRepositoryFactory.GetDataRepository<ISprintRepository>();

                return sprintRepository.GetSprintForScrumMaster(scrumMasterId);
            });
        }

        public ICollection<Sprint> GetSprintStartDate(int sprintId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var sprintRepository = _DataRepositoryFactory.GetDataRepository<ISprintRepository>();

                return sprintRepository.GetSprintStartDate(sprintId);
            });
        }

        public ICollection<Sprint> GetSprintEndDate(int sprintId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var sprintRepository = _DataRepositoryFactory.GetDataRepository<ISprintRepository>();

                return sprintRepository.GetSprintEndDate(sprintId);
            });
        }


        public ICollection<Sprint> GetSprintForAccount(int accountId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var sprintRepository = _DataRepositoryFactory.GetDataRepository<ISprintRepository>();

                return sprintRepository.GetSprintForAccount(accountId);
            });
        }


        public Sprint AddSprint(Sprint sprint)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var sprintRepository = _DataRepositoryFactory.GetDataRepository<ISprintRepository>();

                return sprintRepository.AddSprintWithTeam(sprint);
            });
        }


        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateSprintInfo(Sprint sprint)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var sprintRepository = _DataRepositoryFactory.GetDataRepository<ISprintRepository>();

                var updatedSprint = sprintRepository.UpdateSprintWithTeam(sprint);

                if (updatedSprint == null)
                {
                    var ex = new NotFoundException(string.Format("Sprint with id {0} could not be updated or found", sprint.SprintId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
            });
        }

        public Sprint GetSprintInfo(int sprintId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var sprintRepository = _DataRepositoryFactory.GetDataRepository<ISprintRepository>();

                var sprintEntity = sprintRepository.Get(sprintId);

                if (sprintEntity == null)
                {
                    var ex = new NotFoundException(string.Format("Sprint with id {0} is not in database", sprintId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return sprintEntity;
            });
        }

        public ICollection<Sprint> GetAllSprints()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var sprintRepository = _DataRepositoryFactory.GetDataRepository<ISprintRepository>();
                
                return sprintRepository.Get().ToList();
            });
        }


    }
}
