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
    public class TractorEFRepository : ITractorRepository
    {
        private DbContextOptions dbco;

        public TractorEFRepository(ConfigurationMode mode = ConfigurationMode.PROD)
        {
            dbco = ConfigurationManager.GetDbContext(mode);
        }
        public Result<Tractor> Add(Tractor tractor)
        {
            Result<Tractor> result = new Result<Tractor>();
            try
            {
                using (var context = new AppDbContext(dbco))
                {
                    context.Tractor.Add(tractor);
                    context.SaveChanges();
                    result.Success = true;
                    result.Data = tractor;
                }


            }
            catch (Exception ex)
            {
                result.Messages.Add(ex.Message);
            }
            return result;
        }

        public Result<Tractor> Delete(int tractorId)
        {
            Result<Tractor> result = new Result<Tractor>();

            try
            {
                using (var context = new AppDbContext(dbco))
                {
                    Tractor item = context.Tractor.Find(tractorId);
                    result.Data = item;
                    context.Entry(item).Collection(d => d.drivers).Load();

                    if (item != null)
                    {
                        var ptds = context.PullTractorDriver.Where(ptd => ptd.tractorId == tractorId);

                        foreach (var ptd in ptds)
                        {
                            context.PullTractorDriver.Remove(ptd);
                        }

                        context.Tractor.Remove(item);
                        context.SaveChanges();
                        result.Success = true;
                    }
                    else
                    {
                        result.Messages.Add($"Tractor not found for Id: {tractorId}");
                    }
                }
            }
            catch (Exception ex)
            {
                result.Messages.Add(ex.Message);
            }
            return result;
        }

        public Result<Tractor> Edit(Tractor tractor)
        {
            Result<Tractor> result = new Result<Tractor>();
            try
            {
                using (var context = new AppDbContext(dbco))
                {
                    context.Tractor.Update(tractor);
                    context.SaveChanges();
                    result.Success = true;
                    result.Data = tractor;
                }
            }
            catch (Exception ex)
            {
                result.Messages.Add(ex.Message);
            }
            return result;
        }

        public Result<Tractor> Get(int tractorId)
        {
            var result = new Result<Tractor>();
            try
            {
                using (var context = new AppDbContext(dbco))
                {
                    result.Data = context.Tractor
                        .Find(tractorId);
                    if (result.Data == null)
                    {
                        result.Messages.Add($"Tractor not found for ID: {tractorId}");
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

        public Result<List<Tractor>> GetAll()
        {
            var result = new Result<List<Tractor>>();
            try
            {
                using (var context = new AppDbContext(dbco))
                {
                    result.Data = context.Tractor
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
