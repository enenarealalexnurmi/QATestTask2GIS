using NUnit.Framework;
using RegionsAPI.Data;
using RegionsAPI.User;
using System;
using System.Collections.ObjectModel;

namespace RegionsAPI.Tests
{
    [TestFixture]
    public class RegionsAPITest
    {
        private RegionsAPIUser _regionsAPIUser;
        private readonly int defaultPageSize = 15;
        [SetUp]
        public void SetUp()
        {
            _regionsAPIUser = new RegionsAPIUser();
        }
        [Test]
        public void DefaultPageSize_EmptyQueryParameters_ExpectedEqualDefaultPageSize()
        {
            var call = _regionsAPIUser.GetCall("");

            if (call.Total >= defaultPageSize)
                Assert.That(call.Items.ToArray().Length, Is.EqualTo(defaultPageSize));
        }
        [Test]
        [TestCase(5)]
        [TestCase(10)]
        [TestCase(15)]
        public void PageSize_CorrectValuePageSize_ExpectedEqualInputValue(int value)
        {
            var call = _regionsAPIUser.GetCall(string.Format("?page_size={0}", value));

            while (call.Items.ToArray().Length >= value)
            {
                Assert.That(call.Items.ToArray().Length, Is.EqualTo(value));
                call = _regionsAPIUser.NextPage();
            }
        }
        [Test]
        [TestCase("ru")]
        [TestCase("kg")]
        [TestCase("kz")]
        [TestCase("cz")]
        public void CountryCode_CorrectValuesCountryCode_ExpectedAllItemsHasOnlyInputValue(string value)
        {
            var call = _regionsAPIUser.GetCall(string.Format("?country_code={0}", value));

            while (call.Items.ToArray().Length != 0)
            {
                call.Items.ForEach(delegate (Region region)
                {
                    Assert.That(region.Country.Code, Is.EqualTo(value));
                });
                call = _regionsAPIUser.NextPage();
            }
        }
        [Test]
        public void Total_CountingUniqueItems_ExpectedTotalEqualCountUniqueItems()
        {

        }
    }
}
