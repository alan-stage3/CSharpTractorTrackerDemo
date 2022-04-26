using NUnit.Framework;
using System;
using TractorTracker.Core.Entities;
using TractorTracker.Core.Interfaces;
using TractorTracker.DAL.Repositories;
using static TractorTracker.DAL.ConfigurationManager;

namespace TractorTracker.DAL.Tests
{
    [TestFixture]
    public class DriverTests
    {
        private IDriverRepository repo;
        [SetUp]
        public void SetUp()
        {
            DriverEFRepository setup = new DriverEFRepository(ConfigurationMode.TEST);
            setup.SetKnownGoodState();
            repo = setup;
        }

        [Test]
        public void GetAllDriversTest()
        {
            var driver = repo.GetAll();
            Assert.IsTrue(driver.Data.Count == 2);
        }

        [Test]
        public void GetDriverTest()
        {
            var driver = repo.Get(1);
            Assert.IsTrue(driver.Data.driverId == 1);
            Assert.IsTrue(driver.Data.driverName.Equals("Jon Silsby TEST"));
        }

        [Test]
        public void AddDriverTest()
        {
            Driver newDriver = new Driver
            {
                driverName = "Test McTest",
                driverHometown = "Testylvania"
            };
            var expected = repo.Add(newDriver);
            var actual = repo.Get(expected.Data.driverId);
            Assert.IsTrue(expected.Success);
            Assert.AreEqual(expected.Data.ToString(), actual.Data.ToString());
        }

        [Test]
        public void EditDriverTest()
        {
            var driver = repo.Get(2);
            Driver expected = driver.Data;
            expected.driverName = "TEST EDITED";
            var result = repo.Edit(expected);
            Driver actual = repo.Get(2).Data;

            Assert.IsTrue(actual.driverId == 2);
            Assert.IsTrue(actual.driverName.Equals("TEST EDITED"));
        }

        [Test]
        public void DeleteDriverTest()
        {

            var deleteResult = repo.Delete(1);
            var result = repo.Get(1);
            Assert.IsTrue(deleteResult.Success);
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Messages[0].Contains("not found"));

        }
    }
}
