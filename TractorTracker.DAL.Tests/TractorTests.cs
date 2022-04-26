using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TractorTracker.Core.Entities;
using TractorTracker.Core.Interfaces;
using TractorTracker.DAL.Repositories;
using static TractorTracker.DAL.ConfigurationManager;

namespace TractorTracker.DAL.Tests
{
    [TestFixture]
    class TractorTests
    {
        private ITractorRepository repo;
        [SetUp]
        public void SetUp()
        {
            TractorEFRepository setup = new TractorEFRepository(ConfigurationMode.TEST);
            setup.SetKnownGoodState();
            repo = setup;
        }

        [Test]
        public void GetAllTractorsTest()
        {
            var tractor = repo.GetAll();
            Assert.IsTrue(tractor.Data.Count == 2);
        }

        [Test]
        public void GetTractorTest()
        {
            var tractor = repo.Get(1);
            Assert.IsTrue(tractor.Data.tractorId == 1);
            Assert.IsTrue(tractor.Data.tractorName.Equals("The Crop Doctor TEST"));
        }

        [Test]
        public void AddTractorTest()
        {
            Tractor newTractor = new Tractor
            {
                tractorName = "Testylvania Thumper",
                tractorOwner = "Test McTester"
            };
            var expected = repo.Add(newTractor);
            var actual = repo.Get(expected.Data.tractorId);
            Assert.IsTrue(expected.Success);
            Assert.AreEqual(expected.Data.ToString(), actual.Data.ToString());
        }

        [Test]
        public void EditTractorTest()
        {
            var tractor = repo.Get(2);
            Tractor expected = tractor.Data;
            expected.tractorName = "TEST EDITED";
            var result = repo.Edit(expected);
            Tractor actual = repo.Get(2).Data;

            Assert.IsTrue(actual.tractorId == 2);
            Assert.IsTrue(actual.tractorName.Equals("TEST EDITED"));
        }

        [Test]
        public void DeleteTractorTest()
        {

            var deleteResult = repo.Delete(1);
            var result = repo.Get(1);
            Assert.IsTrue(deleteResult.Success);
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Messages[0].Contains("not found"));

        }
    }
}
