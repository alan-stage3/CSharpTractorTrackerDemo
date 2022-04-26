using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TractorTracker.Core.Entities
{
    public class Pull
    {
        public int pullId { get; set; }
        public string pullName { get; set; }
        public string pullPromoter { get; set; }
        public string pullCity { get; set; }
        public string pullState { get; set; }

        public virtual List<PullTractorDriver> pullTractorDrivers { get; set; }
    }
}
