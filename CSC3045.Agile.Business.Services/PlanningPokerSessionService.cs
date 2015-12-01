using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Security.Authentication;
using System.ServiceModel;
using Core.Common.Contracts;
using Core.Common.Exceptions;
using Core.Common.Utils;
using CSC3045.Agile.Business.Common;
using CSC3045.Agile.Business.Contracts;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;

namespace CSC3045.Agile.Business.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple,
        ReleaseServiceInstanceOnTransactionComplete = false)]
    public class PlanningPokerSessionService : ServiceBase, IPlanningPokerSessionService
    {
        [Import]
        private IBusinessEngineFactory _BusinessEngineFactory;

        [Import]
        private IDataRepositoryFactory _DataRepositoryFactory;

        public PlanningPokerSessionService()
        {
        }

        /// <summary>
        /// Used for testing, this service has the data repository initialized through dependency injection
        /// </summary>
        /// <param name="dataRepositoryFactory"></param>
        public PlanningPokerSessionService(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        /// <summary>
        /// Used for testing, this service has the business engine initialized through dependency injection
        /// </summary>
        /// <param name="businessEngineFactory"></param>
        public PlanningPokerSessionService(IBusinessEngineFactory businessEngineFactory)
        {
            _BusinessEngineFactory = businessEngineFactory;
        }

        /// <summary>
        /// Used for testing, this service has the data repository & business engine initialized through dependency injection
        /// </summary>
        /// <param name="dataRepositoryFactory"></param>
        /// <param name="businessEngineFactory"></param>
        public PlanningPokerSessionService(IDataRepositoryFactory dataRepositoryFactory, IBusinessEngineFactory businessEngineFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
            _BusinessEngineFactory = businessEngineFactory;
        }

        public void CreatePlanningPokerSession(PlanningPokerSession planningPokerSession)
        {
            throw new System.NotImplementedException();
        }

        public PlanningPokerSession StartPlanningPokerSession(int planningPokerSessionId)
        {
            throw new System.NotImplementedException();
        }

        public void AssignPointsToStory(int pointValue, string userStoryId)
        {
            throw new System.NotImplementedException();
        }

        public void ResetStoryStatus()
        {
            throw new System.NotImplementedException();
        }

        public void SelectCurrentStory(int userStoryId)
        {
            throw new System.NotImplementedException();
        }

        public void EndSession()
        {
            throw new System.NotImplementedException();
        }

        public ICollection<ChatMessage> GetChatMessages()
        {
            throw new System.NotImplementedException();
        }

        public ICollection<Dictionary<int, PlanningPokerSelectionStatus>> GetSelectionStatuses()
        {
            throw new System.NotImplementedException();
        }

        public void SendMessage(ChatMessage chatMessage)
        {
            throw new System.NotImplementedException();
        }

        public void SendVote(int accountId)
        {
            throw new System.NotImplementedException();
        }
    }
}