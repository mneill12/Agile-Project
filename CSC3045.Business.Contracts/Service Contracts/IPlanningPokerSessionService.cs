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
        [FaultContract(typeof(NotFoundException))]
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
        [FaultContract(typeof(NotFoundException))]
        ICollection<ChatMessage> GetChatMessages();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ICollection<Dictionary<int, PlanningPokerSelectionStatus>> GetSelectionStatuses();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void SendMessage(ChatMessage chatMessage);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void SendVote(int accountId);





    }
}