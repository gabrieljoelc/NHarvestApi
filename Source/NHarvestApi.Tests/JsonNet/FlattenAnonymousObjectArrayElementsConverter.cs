using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHarvestApi.JsonNet;

namespace NHarvestApi.Tests.JsonNet
{
    /// <summary>
    /// Summary description for FlattenAnonymousObjectArrayElementsConverter
    /// </summary>
    [TestClass]
    public class FlattenAnonymousObjectArrayElementsConverterTests
    {
        // TODO: write ReadJson() tests

        [TestMethod]
        public void Can_can_convert_returns_true_when_same_type()
        {
            var target = new FlattenAnonymousObjectArrayElementsConverter<Collection<FooBar>, FooBar>();

            var actual = target.CanConvert(typeof (Collection<FooBar>));

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void Can_can_convert_returns_false_when_not_same_type()
        {
            var target = new FlattenAnonymousObjectArrayElementsConverter<Collection<FooBar>, FooBar>();

            var actual = target.CanConvert(typeof(IEnumerable<FooBar>));

            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Write_json_throws()
        {
            var target = new FlattenAnonymousObjectArrayElementsConverter<Collection<FooBar>, FooBar>();

            target.WriteJson(null, null, null);
        }
    }

    public class FooBar
    {
    }
}
