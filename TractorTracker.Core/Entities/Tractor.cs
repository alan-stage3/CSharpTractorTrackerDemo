using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TractorTracker.Core.Entities
{
    public class Tractor
    {
        public int tractorId { get; set; }
        public string tractorName { get; set; }
        public string tractorOwner { get; set; }
        public List<Driver> drivers { get; set; }

    }
}
