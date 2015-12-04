using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using Core.Common.Contracts;
using CSC3045.Agile.Business.Contracts;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;

namespace CSC3045.Agile.Business.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple,
        ReleaseServiceInstanceOnTransactionComplete = false)]
    public class BurndownService : ServiceBase, IBurndownService
    {
        [Import] private IBusinessEngineFactory _BusinessEngineFactory;
        [Import] private IDataRepositoryFactory _DataRepositoryFactory;

        public BurndownService()
        {
        }

        /// <summary>
        /// Used for testing, this service has the data repository initialized through dependency injection
        /// </summary>
        /// <param name="dataRepositoryFactory"></param>
        public BurndownService(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        /// <summary>
        /// Used for testing, this service has the business engine initialized through dependency injection
        /// </summary>
        /// <param name="businessEngineFactory"></param>
        public BurndownService(IBusinessEngineFactory businessEngineFactory)
        {
            _BusinessEngineFactory = businessEngineFactory;
        }

        /// <summary>
        /// Used for testing, this service has the data repository & business engine initialized through dependency injection
        /// </summary>
        /// <param name="dataRepositoryFactory"></param>
        /// <param name="businessEngineFactory"></param>
        public BurndownService(IDataRepositoryFactory dataRepositoryFactory, IBusinessEngineFactory businessEngineFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
            _BusinessEngineFactory = businessEngineFactory;
        }

        public IEnumerable<Entities.TaskBurndownPoint> GetTaskBurndownPointsForStoryTask(int storyTaskId)
        {
            var burndownRepository = _DataRepositoryFactory.GetDataRepository<ITaskBurndownPointRepository>();

            return burndownRepository.GetTaskBurndownPointsByStoryTaskId(storyTaskId);
        }


        public int GetTotalHoursRemainingForStory(int storyId)
        {
            var burndownRepository = _DataRepositoryFactory.GetDataRepository<ITaskBurndownPointRepository>();

            IEnumerable<Dictionary<DateTime, int>> hoursRemainingForStory = burndownRepository.GetTotalHoursRemainingByStoryId(storyId);

            int storyTotalHours = 0;

            foreach (Dictionary<DateTime, int> dictionary in hoursRemainingForStory)
            {
                int taskTotalHours = dictionary.Sum(x => x.Value);
                storyTotalHours += taskTotalHours;
            }

            return storyTotalHours;

        }


        public int GetTotalHoursRemainingForDay(DateTime date)
        {
            var burndownRepository = _DataRepositoryFactory.GetDataRepository<ITaskBurndownPointRepository>();

            IEnumerable<Dictionary<DateTime, int>> hoursRemainingForDay =
                burndownRepository.GetTotalHoursRemainingByDay(date);

            int dayTotalHours = 0;

            foreach (Dictionary<DateTime, int> dictionary in hoursRemainingForDay)
            {
                int taskTotalHours = dictionary.Sum(x => x.Value);
                dayTotalHours += taskTotalHours;
            }

            return dayTotalHours;
        }

        public int GetOrigionalTotalHoursForSprintId(int sprintId)
        {

            if (sprintId == 1)
            {
                return 100;
            }
            else if (sprintId == 2)
            {
                return 50;
            }
            else if (sprintId == 3)
            {
                return 75;
            }

            return 0;
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public Dictionary<DateTime, int> GetHourDateMapForSprintId(int sprintId)
        {
            Dictionary< DateTime, int> totalHourMap = new Dictionary<DateTime, int>();

            if (sprintId == 1)
            {

                totalHourMap.Add(new DateTime(2015, 10, 5), 100);
                totalHourMap.Add(new DateTime(2015, 10, 6), 90);
                totalHourMap.Add(new DateTime(2015, 10, 7), 87);
                totalHourMap.Add(new DateTime(2015, 10, 8), 60);
                totalHourMap.Add(new DateTime(2015, 10, 9), 50);

                totalHourMap.Add(new DateTime(2015, 10, 12), 50);
                totalHourMap.Add(new DateTime(2015, 10, 13), 45);
                totalHourMap.Add(new DateTime(2015, 10, 14), 30);
                totalHourMap.Add(new DateTime(2015, 10, 15), 20);
                totalHourMap.Add(new DateTime(2015, 10, 16), 0);

                return totalHourMap;

            }
            else if (sprintId == 2)
            {
                totalHourMap.Add(new DateTime(2015, 10, 5), 50);
                totalHourMap.Add(new DateTime(2015, 10, 6), 40);
                totalHourMap.Add(new DateTime(2015, 10, 7), 30);
                totalHourMap.Add(new DateTime(2015, 10, 8), 30);
                totalHourMap.Add(new DateTime(2015, 10, 9), 30);

                totalHourMap.Add(new DateTime(2015, 10, 12), 20);
                totalHourMap.Add(new DateTime(2015, 10, 13), 19);
                totalHourMap.Add(new DateTime(2015, 10, 14), 16);
                totalHourMap.Add(new DateTime(2015, 10, 15), 10);
                totalHourMap.Add(new DateTime(2015, 10, 16), 0);

                return totalHourMap;

            }
            else if (sprintId == 3)
            {
                totalHourMap.Add(new DateTime(2015, 11, 16), 75);
                totalHourMap.Add(new DateTime(2015, 11, 17), 60);
                totalHourMap.Add(new DateTime(2015, 11, 18), 59);
                totalHourMap.Add(new DateTime(2015, 11, 19), 55);
                totalHourMap.Add(new DateTime(2015, 11, 20), 40);

                totalHourMap.Add(new DateTime(2015, 11, 23), 35);
                totalHourMap.Add(new DateTime(2015, 11, 24), 30);
                totalHourMap.Add(new DateTime(2015, 11, 25), 30);
                totalHourMap.Add(new DateTime(2015, 11, 26), 10);
                totalHourMap.Add(new DateTime(2015, 11, 27), 0);

                return totalHourMap;
            }

            return totalHourMap;
        }
    }
}
