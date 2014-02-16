using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using NHarvestApi.JsonNet;

namespace NHarvestApi.KeepinItSimpleSmokeSignalConsoleApp
{
    class Program
    {
        private static readonly HarvestApi<ApiBasicAuthSettings> BasicAuthApi = new HarvestApi<ApiBasicAuthSettings>(new BasicAuthHttpClientFactory());

        static void Main(string[] args)
        {
            When_basic_auth_defaults(args).Wait();

            Console.WriteLine("Press any key to close...");
            Console.ReadLine();
        }

        private static async Task When_basic_auth_defaults(string[] args)
        {
            if (args.Length != 3 || args.Any(string.IsNullOrEmpty))
                throw new ArgumentException("Need all three Harvest args for basic auth smoke test.");

            var subdomain = args[0];
            var username = args[1];
            var password = args[2];

            var apiBasicAuthSettings = new ApiBasicAuthSettings(subdomain);
            apiBasicAuthSettings.SetCredentials(username, password);

            var hash = await Can_get_result_from_WhoAmI(apiBasicAuthSettings);
            await Can_get_result_from_GetAllTimeEntriesLoggedByUserForGivenTimeframe(apiBasicAuthSettings, hash.User.Id);
        }

        private static async Task<Hash> Can_get_result_from_WhoAmI(ApiBasicAuthSettings apiBasicAuthSettings)
        {
            var hash = await BasicAuthApi.Get<Hash>(apiBasicAuthSettings, factory => factory.WhoAmI());

            if (hash == null || hash.User == null || hash.User.Id <= 0)
            {
                Console.WriteLine("WhoAmI returned null.");
                Debug.Fail("WhoAmI returned null.");
            }

            Console.WriteLine(hash);

            return hash;
        }

        private static async Task Can_get_result_from_GetAllTimeEntriesLoggedByUserForGivenTimeframe(ApiBasicAuthSettings apiBasicAuthSettings, int userId,
            DateTime? from = null, DateTime? to = null)
        {
            var timeEntries = await BasicAuthApi.Get<DayEntries>(apiBasicAuthSettings,
                factory =>
                    factory.GetAllTimeEntriesLoggedByUserForGivenTimeframe(userId, @from ?? DateTime.Parse("10-1-2013"),
                        to ?? DateTime.Parse("12-31-2013")), HarvestResourceConverterDefaults.Create(new FlattenAnonymousObjectArrayElementsConverter<DayEntries, DayEntry>()));

            if (timeEntries == null)
            {
                Console.WriteLine("GetAllTimeEntriesLoggedByUserForGivenTimeframe returned null.");
                Debug.Fail("GetAllTimeEntriesLoggedByUserForGivenTimeframe returned null.");
            }

            Console.WriteLine(timeEntries);
        }
        
        class Hash
        {
            public User User { get; set; }
            public override string ToString()
            {
                return User != null ? User.ToString() : "No data";
            }
        }

        public class User
        {
            public int Id { get; set; }
            
            public string FirstName { get; set; }
            
            public string LastName { get; set; }

            public override string ToString()
            {
                return string.Format("User with id: {0}, first name: {1}, last name: {2}", Id, FirstName, LastName);
            }
        }

        // https://github.com/harvesthq/api/blob/master/Sections/Reports.md
        private class DayEntries : HashSet<DayEntry>
        {
            public decimal TotalHours { get { return this.Sum(d => d.Hours); } }

            public override string ToString()
            {
                return this.Aggregate("", (s, entry) => s + "; " + entry.ToString() + "\n") + "Total hours: " + TotalHours;
            }
        }

        //[
        //  {
        //    "day_entry": {
        //      "adjustment_record": false,
        //      "created_at": "2013-10-09T04:11:50Z",
        //      "hours": 4,
        //      "id": 176506066,
        //      "is_closed": false,
        //      "notes": null,
        //      "project_id": 2998845,
        //      "spent_at": "2013-10-01",
        //      "task_id": 1523546,
        //      "timer_started_at": null,
        //      "updated_at": "2013-10-09T04:11:50Z",
        //      "user_id": 530906,
        //      "is_billed": true,
        //      "invoice_id": 3611842
        //    }
        //  },
        //  {
        //    "day_entry": {
        //      "adjustment_record": false,
        //      "created_at": "2013-10-09T04:11:50Z",
        //      "hours": 5,
        //      "id": 176506067,
        //      "is_closed": false,
        //      "notes": null,
        //      "project_id": 2998845,
        //      "spent_at": "2013-10-02",
        //      "task_id": 1523546,
        //      "timer_started_at": null,
        //      "updated_at": "2013-10-09T04:11:50Z",
        //      "user_id": 530906,
        //      "is_billed": true,
        //      "invoice_id": 3611842
        //    }
        //  }
        //    ]
        private class DayEntry
        {
            public int Id { get; set; }

            public decimal Hours { get; set; }

            public int ProjectId { get; set; }

            public override string ToString()
            {
                return string.Format("TimeEntry with Id: {0}, Hours: {1}, ProjectId: {2}", Id, Hours, ProjectId);
            }

            public override bool Equals(object obj)
            {
                var otherTimeEntry = obj as DayEntry;

                return obj != null && otherTimeEntry != null && Id == otherTimeEntry.Id && base.Equals(obj);
                
            }

            public override int GetHashCode()
            {
                if (Id.Equals(default(int)))
                    return base.GetHashCode();

                return GetType().GetHashCode() ^ Id.GetHashCode();
            }
        }
    }
}
