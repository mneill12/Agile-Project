using CSC3045.Agile.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Utils;
using CSC3045.Agile.Client.Contracts;

namespace CSC3045.Agile.Client.Proxies
{
    class PlanningPokerSessionClient : ClientBase<IPlanningPokerSessionService>, IPlanningPokerSessionService
    {
        public void CreatePlanningPokerSession(PlanningPokerSession planningPokerSession)
        {
            Channel.CreatePlanningPokerSession(planningPokerSession);
        }

        public PlanningPokerSession StartPlanningPokerSession(int planningPokerSessionId)
        {
            return Channel.StartPlanningPokerSession(planningPokerSessionId);
        }

        public void AssignPointsToStory(int pointValue, string userStoryId)
        {
            Channel.AssignPointsToStory(pointValue,userStoryId);
        }

        public void ResetStoryStatus()
        {
            Channel.ResetStoryStatus();
        }

        public void SelectCurrentStory(int userStoryId)
        {
            Channel.SelectCurrentStory(userStoryId);
        }

        public void EndSession()
        {
            Channel.EndSession();
        }

        public ICollection<ChatMessage> GetChatMessages()
        {
            return Channel.GetChatMessages();
        }

        public ICollection<Dictionary<int, PlanningPokerSelectionStatus>> GetSelectionStatuses()
        {
            return Channel.GetSelectionStatuses();
        }

        public void SendMessage(ChatMessage chatMessage)
        {
            Channel.SendMessage(chatMessage);
        }

        public void SendVote(int accountId, int pointNumber)
        {
            Channel.SendVote(accountId, pointNumber);
        }
    }
}
