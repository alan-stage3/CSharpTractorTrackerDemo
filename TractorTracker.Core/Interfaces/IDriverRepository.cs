using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TractorTracker.Core.Entities;

namespace TractorTracker.Core.Interfaces
{
    public interface IDriverRepository
    {
        Result<Driver> Get(int driverId);
        Result<List<Driver>> GetAll();
        Result<Driver> Add(Driver driver);
        Result<Driver> Edit(Driver driver);
        Result<Driver> Delete(int driverId);
    }
}
