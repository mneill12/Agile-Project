using System.Collections.Generic;
using System.ServiceModel;
using Core.Common.Exceptions;
using Core.Common.Utils;
using CSC3045.Agile.Business.Entities;

namespace CSC3045.Agile.Business.Contracts
{
    [ServiceContract]
    public interface IPlanningPokerSessionService
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void CreatePlanningPokerSession(PlanningPokerSession planningPokerSession);

        [OperationContract]
        PlanningPokerSession StartPlanningPokerSession(int planningPokerSessionId);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void AssignPointsToStory(int pointValue, string userStoryId);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void ResetStoryStatus();

        [OperationContract]
        void SelectCurrentStory(int userStoryId);

        [OperationContract]
        void EndSession();

        [OperationContract]
        ICollection<ChatMessage> GetChatMessages();

        [OperationContract]
        PlanningPokerSelectionStatus GetSelectionStates();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void SendMessage();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void SendVote();





    }
}