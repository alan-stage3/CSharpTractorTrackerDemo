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
    public class PullEFRepository : IPullRepository
    {
        private DbContextOptions dbco;

        public PullEFRepository(ConfigurationMode mode = ConfigurationMode.PROD)
        {
            dbco = ConfigurationManager.GetDbContext(mode);
        }
        public Result<Pull> Add(Pull pull)
        {
            Result<Pull> result = new Result<Pull>();
            try
            {
                using (var context = new AppDbContext(dbco))
                {
                    context.Pull.Add(pull);
                    context.SaveChanges();
                    result.Success = true;
                    result.Data = pull;
                }


            }
            catch (Exception ex)
            {
                result.Messages.Add(ex.Message);
            }
            return result;
        }

        public Result Delete(int pullId)
        {
            Result result = new Result();
            try
            {
                using (var context = new AppDbContext(dbco))
                {
                    Pull item = context.Pull.Find(pullId);
                    context.Entry(item).Collection(p => p.pullTractorDrivers).Load();

                    if (item != null)
                    {
                        foreach (PullTractorDriver ptd in item.pullTractorDrivers)
                        {
                            context.PullTractorDriver.Remove(ptd);
                        }
                        context.Pull.Remove(item);
                        context.SaveChanges();
                        result.Success = true;
                    }
                    else
                    {
                        result.Messages.Add($"Pull not found for Id: {pullId}");
                    }
                }
            } 
            catch (Exception ex)
            {
                result.Messages.Add(ex.Message);
            }
            return result;
        }

        public Result<Pull> Edit(Pull pull)
        {
            Result<Pull> result = new Result<Pull>();
            try
            {
                using (var context = new AppDbContext(dbco))
                {
                    context.Pull.Update(pull);
                    context.SaveChanges();
                    result.Success = true;
                    result.Data = pull;
                }
            }
            catch (Exception ex)
            {
                result.Messages.Add(ex.Message);
            }
            return result;
        }

        public Result<Pull> Get(int pullId)
        {
            var result = new Result<Pull>();
            try
            {
                using (var context = new AppDbContext(dbco))
                {
                    result.Data = context.Pull
                        .Find(pullId);
                    if(result.Data == null)
                    {
                        result.Messages.Add($"Pull not found for ID: {pullId}");
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

        public Result<List<Pull>> GetAll()
        {
            var result = new Result<List<Pull>>();
            try
            {
                using(var context = new AppDbContext(dbco))
                {
                    result.Data = context.Pull
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
