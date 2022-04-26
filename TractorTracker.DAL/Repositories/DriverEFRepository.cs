using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TractorTracker.Core;
using TractorTracker.Core.Entities;
using TractorTracker.Core.Interfaces;
using static TractorTracker.DAL.ConfigurationManager;

namespace TractorTracker.DAL.Repositories
{
    public class DriverEFRepository : IDriverRepository
    {
        private DbContextOptions dbco;

        public DriverEFRepository(ConfigurationMode mode = ConfigurationMode.PROD)
        {
            dbco = ConfigurationManager.GetDbContext(mode);
        }

        public Result<Driver> Add(Driver driver)
        {
            Result<Driver> result = new Result<Driver>();
            try
            {
                using (var context = new AppDbContext(dbco))
                {
                    context.Driver.Add(driver);
                    context.SaveChanges();
                    result.Success = true;
                    result.Data = driver;
                }


            }
            catch (Exception ex)
            {
                result.Messages.Add(ex.Message);
            }
            return result;
        }

        public Result<Driver> Delete(int driverId)
        {
            Result<Driver> result = new Result<Driver>();
          
            try
            {
                using (var context = new AppDbContext(dbco))
                {
                    Driver item = context.Driver.Find(driverId);
                    result.Data = item;
                    context.Entry(item).Collection(d => d.tractors).Load();

                    if (item != null)
                    {
                        var ptds = context.PullTractorDriver.Where(ptd => ptd.driverId == driverId);

                        foreach(var ptd in ptds)
                        {
                            context.PullTractorDriver.Remove(ptd);
                        }

                        context.Driver.Remove(item);
                        context.SaveChanges();
                        result.Success = true;
                    }
                    else
                    {
                        result.Messages.Add($"Driver not found for Id: {driverId}");
                    }
                }
            }
            catch (Exception ex)
            {
                result.Messages.Add(ex.Message);
            }
            return result;
        }

        public Result<Driver> Edit(Driver driver)
        {
            Result<Driver> result = new Result<Driver>();
            try
            {
                using (var context = new AppDbContext(dbco))
                {
                    context.Driver.Update(driver);
                    context.SaveChanges();
                    result.Success = true;
                    result.Data = driver;
                }
            }
            catch (Exception ex)
            {
                result.Messages.Add(ex.Message);
            }
            return result;
        }

        public Result<Driver> Get(int driverId)
        {
            var result = new Result<Driver>();
            try
            {
                using (var context = new AppDbContext(dbco))
                {
                    result.Data = context.Driver
                        .Find(driverId);
                    if (result.Data == null)
                    {
                        result.Messages.Add($"Driver not found for ID: {driverId}");
                    }
                    result.Success = true;
                }
            }
            catch (Exception e)
            {
                result.Messages.Add(e.Message);
                result.Success = false;
            }
            return result;
        }

        public Result<List<Driver>> GetAll()
        {
            var result = new Result<List<Driver>>();
            try
            {
                using (var context = new AppDbContext(dbco))
                {
                    result.Data = context.Driver
                        .ToList();
                    result.Success = true;
                }
            }
            catch (Exception e)
            {
                result.Messages.Add(e.Message);
                result.Success = false;
            }

            return result;
        }

        public void SetKnownGoodState()
        {
            using (var context = new AppDbContext(dbco))
            {
                context.Database.ExecuteSqlInterpolated($"SetKnownGoodState");
            }
        }
    }
}
