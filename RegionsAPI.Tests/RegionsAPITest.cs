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
            var call = _regionsAPIUser.GetCall("");
            int expected = call.Total;

            while (call.Items.ToArray().Length != 0)
            {
                //AddUnique
            }
            Assert.That(AddUnique.Length, Is.EqualTo(expected));
        }
        [Test]
        public void FuzzySearch_BruteForceValues_ExpectedAllReturnedItemsHasValue()
        {
            while (/*Has*/)
            {
                var call = _regionsAPIUser.GetCall(string.Format("?q={0}", searchString))

                Assert.That
            }
        }
        [Test]
        [TestCase("-1")]
        [TestCase("0")]
        [TestCase("a")]
        [TestCase("1a")]
        [TestCase("1000000000000000")]
        [TestCase("1+1")]
        [TestCase("1&page=2")]
        public void Page_IncorrectValues_ExpectedErrorMessage(string value)
        {

        }
        [Test]
        [TestCase("&page=2")]
        [TestCase("&page=a")]
        [TestCase("&page=0")]
        [TestCase("&page_size=-1")]
        [TestCase("&page_size=5")]
        [TestCase("country_code=kz")]
        [TestCase("country_code=kg")]
        [TestCase("country_code=ru")]
        [TestCase("country_code=cz")]
        public void FuzzySearch_AnotherQueryParametrs_ExpectedIgnoringAnotherParametrs()
        {

        }
        [Test]
        [TestCase("a")]
        [TestCase("aa")]
        [TestCase("")]
        public void FuzzySearch_IncorrectValues_ExpectedErrorMessage(string value)
        {

        }
        [Test]
        [TestCase("1")]
        [TestCase("-1")]
        public void PageSize_IncorrectValues_ExpectedErrorMessage(string value)
        {

        }
    }
}
