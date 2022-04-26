using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TractorTracker.Core.Entities
{
    public class PullTractorDriver
    {
        public int? placement { get; set; }
        public bool isDisqualified { get; set; }

        public int pullId { get; set; }
        public int tractorId { get; set; }
        public int driverId { get; set; }


    }
}
