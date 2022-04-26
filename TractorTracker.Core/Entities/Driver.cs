using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TractorTracker.Core.Entities
{
    public class Driver
    {
        public int driverId { get; set;}
        public string driverName { get; set; }
        public string driverHometown { get; set; }
        //public virtual List<TractorDriver> TractorDriver { get; set; }
        
        public List<Tractor> tractors { get; set; }
    }
}
