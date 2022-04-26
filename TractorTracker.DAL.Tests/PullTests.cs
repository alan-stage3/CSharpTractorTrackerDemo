using NUnit.Framework;
using System;
using TractorTracker.Core.Entities;
using TractorTracker.Core.Interfaces;
using TractorTracker.DAL.Repositories;
using static TractorTracker.DAL.ConfigurationManager;

namespace TractorTracker.DAL.Tests
{
    [TestFixture]
    public class PullTests
    {
        private IPullRepository repo;
        [SetUp]
        public void SetUp()
        {
            PullEFRepository setup = new PullEFRepository(ConfigurationMode.TEST);
            setup.SetKnownGoodState();
            repo = setup;
        }

        [Test]
        public void GetAllPullsTest()
        {
            Console.WriteLine("In the test");
            var pulls = repo.GetAll();
            Assert.IsTrue(pulls.Data.Count == 3);
        }

        [Test]
        public void GetPullTest()
        {
            var pull = repo.Get(1);
            Assert.IsTrue(pull.Data.pullId == 1);
            Assert.IsTrue(pull.Data.pullName.Equals("Brandenburg County Fair TEST"));
        }

        [Test]
        public void AddPullTest()
        {
            Pull newPull = new Pull
            {
                pullName = "Test Insert Pull",
                pullPromoter = "Test Pull Promoter",
                pullCity = "Testville",
                pullState = "TT"
            };
            var expected = repo.Add(newPull);
            var actual = repo.Get(expected.Data.pullId);
            Assert.IsTrue(expected.Success);
            Assert.AreEqual(expected.Data.ToString(), actual.Data.ToString());
        }

        [Test]
        public void EditPullTest()
        {
            var pull = repo.Get(2);
            Pull expected = pull.Data;
            expected.pullName = "NFMS EDITED";
            var result = repo.Edit(expected);
            Pull actual = repo.Get(2).Data;

            Assert.IsTrue(actual.pullId == 2);
            Assert.IsTrue(actual.pullName.Equals("NFMS EDITED"));
        }

        [Test]
        public void DeletePullTest()
        {
            
            var deleteResult = repo.Delete(1);
            var result = repo.Get(1);
            Assert.IsTrue(deleteResult.Success);
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Messages[0].Contains("not found"));

        }
    }
}
