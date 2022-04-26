using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TractorTracker.Core.Entities;

namespace TractorTracker.Core.Interfaces
{
    public interface ITractorRepository
    {
        Result<Tractor> Get(int tractorId);

        Result<List<Tractor>> GetAll();
        Result<Tractor> Add(Tractor tractor);
        Result<Tractor> Edit(Tractor tractor);
        Result<Tractor> Delete(int tractorID);
    }
}
