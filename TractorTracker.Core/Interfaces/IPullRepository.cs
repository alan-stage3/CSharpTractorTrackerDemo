using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TractorTracker.Core.Entities;

namespace TractorTracker.Core.Interfaces
{
    public interface IPullRepository
    {
        Result<Pull> Get(int pullId);
        Result<List<Pull>> GetAll();
        Result<Pull> Add(Pull pull);
        Result<Pull> Edit(Pull pull);
        Result Delete(int pullId);
    }
}
