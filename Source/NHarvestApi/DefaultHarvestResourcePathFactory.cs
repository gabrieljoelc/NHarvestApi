using System;

namespace NHarvestApi
{
    // this would get factored out if we ever make the API wrapper portion generic
    class DefaultHarvestResourcePathFactory : IHarvestResourcePathFactory
    {
        private const string DateFormat = "yyyyMMdd";

        public string WhoAmI()
        {
            return "account/who_am_i";
        }

        public string GetAllTimeEntriesLoggedByUserForGivenTimeframe(int userId, DateTime from, DateTime to, bool? billable)
        {
            var uri = string.Format("people/{0}/entries?from={1}&to={2}", userId, from.ToString(DateFormat), to.ToString(DateFormat));
            if (billable.HasValue)
            {
                uri += "&billable=" + (billable.Value ? "yes" : "no");
            }
            return uri;
        }

        // TODO: add rest of uris
    }
}