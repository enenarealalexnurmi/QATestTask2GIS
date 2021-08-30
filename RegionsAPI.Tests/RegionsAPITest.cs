using System.Collections.Generic;
using NUnit.Framework;
using RegionsAPI.Data;
using RegionsAPI.User;
using System;

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
            List<Region> actualUnique = new List<Region> { };

            while (call.Items.ToArray().Length != 0)
            {
                call.Items.ForEach(delegate (Region region)
                {
                    if (!actualUnique.Contains(region))
                    {
                        actualUnique.Add(region);
                    }
                });
                call = _regionsAPIUser.NextPage();
            }
            Assert.That(actualUnique.ToArray().Length, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("-1")]
        [TestCase("0")]
        [TestCase("a")]
        [TestCase("1a")]
        [TestCase("1+1")]
        public void Page_IncorrectValues_ExpectedErrorMessage(string value)
        {
            var call = _regionsAPIUser.GetCall(string.Format("?page={0}", value));

            Assert.That(_regionsAPIUser.StatusCode.ToString(), Is.EqualTo("OK"));
            Assert.That(call.Error.Message, !Is.Empty);
        }
        [Test]
        [TestCase("a")]
        [TestCase("aa")]
        [TestCase("")]
        public void FuzzySearch_IncorrectValues_ExpectedErrorMessage(string value)
        {
            var call = _regionsAPIUser.GetCall(string.Format("?page={0}", value));

            Assert.That(_regionsAPIUser.StatusCode.ToString(), Is.EqualTo("OK"));
            Assert.That(call.Error.Message, !Is.Empty);
        }
        [Test]
        [TestCase("1")]
        [TestCase("-1")]
        public void PageSize_IncorrectValues_ExpectedErrorMessage(string value)
        {
            var call = _regionsAPIUser.GetCall(string.Format("?page={0}", value));

            Assert.That(_regionsAPIUser.StatusCode.ToString(), Is.EqualTo("OK"));
            Assert.That(call.Error.Message, !Is.Empty);
        }
//Bad, very bad idea.
        //[Test, TestCaseSource("GenerateThreeCharSequence")]
        //[Ignore("Too many tests")]
        //public void FuzzySearch_BruteForceValues_ExpectedAllReturnedItemsHasValue(string searchString)
        //{
        //    var call = _regionsAPIUser.GetCall(string.Format("?q={0}", searchString));
        //    if (call.Items.ToArray().Length != 0)
        //    {
        //        call.Items.ForEach(delegate (Region region)
        //        {
        //            Assert.That(region.Name.ToLower().Contains(searchString), Is.True);
        //        });
        //    }
        //}
        //private static string[] GenerateThreeCharSequence()
        //{
        //    string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        //    var q = alphabet.Select(x => x.ToString());
        //    int size = 3;
        //    for (int i = 1; i < size; ++i)
        //        q = q.SelectMany(x => alphabet, (x, y) => x + y);
        //    return q.ToArray();
        //}
    }
}
