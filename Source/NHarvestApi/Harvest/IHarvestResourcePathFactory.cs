using System;

namespace NHarvestApi.Harvest
{
    public interface IHarvestResourcePathFactory
    {
        string WhoAmI();

        string GetAllTimeEntriesLoggedByUserForGivenTimeframe(int userId, DateTime @from, DateTime to, bool? billable);
    }
}