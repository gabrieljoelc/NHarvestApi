using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHarvestApi.Harvest;

namespace NHarvestApi.Tests.Harvest
{
    [TestClass]
    public class DefaultHarvestResourcePathFactoryTests
    {
        private readonly Uri _mockBaseUri = new Uri("https://subdomain.harvestapp.com");

        [TestMethod]
        public void Can_create_full_uri_from_WhoAmI()
        {
            var factory = new DefaultHarvestResourcePathFactory();

            var actual = new Uri(_mockBaseUri, factory.WhoAmI());
            var expected = new Uri(_mockBaseUri, "account/who_am_i");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Can_create_full_uri_from_GetAllTimeEntriesLoggedByUserForGivenTimeframe_when_billabe_not_specified()
        {
            var factory = new DefaultHarvestResourcePathFactory();

            var actual = new Uri(_mockBaseUri,
                factory.GetAllTimeEntriesLoggedByUserForGivenTimeframe(1001, DateTime.Parse("10-1-2013"),
                    DateTime.Parse("12-31-2013"), null));
            var expected = new Uri(_mockBaseUri, "people/1001/entries?from=20131001&to=20131231");
            
            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod]
        public void Can_create_full_uri_from_GetAllTimeEntriesLoggedByUserForGivenTimeframe_when_billabe_true()
        {
            var factory = new DefaultHarvestResourcePathFactory();

            var actual = new Uri(_mockBaseUri,
                factory.GetAllTimeEntriesLoggedByUserForGivenTimeframe(1001, DateTime.Parse("10-1-2013"),
                    DateTime.Parse("12-31-2013"), true));
            var expected = new Uri(_mockBaseUri, "people/1001/entries?from=20131001&to=20131231&billable=yes");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Can_create_full_uri_from_GetAllTimeEntriesLoggedByUserForGivenTimeframe_when_billabe_false()
        {
            var factory = new DefaultHarvestResourcePathFactory();

            var actual = new Uri(_mockBaseUri,
                factory.GetAllTimeEntriesLoggedByUserForGivenTimeframe(1001, DateTime.Parse("10-1-2013"),
                    DateTime.Parse("12-31-2013"), false));
            var expected = new Uri(_mockBaseUri, "people/1001/entries?from=20131001&to=20131231&billable=no");

            Assert.AreEqual(expected, actual);
        }


    }
}
