using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TractorTracker.MVC.Models
{
    public class ViewDriver
    {
        public int driverId { get; set; }

        [Required(ErrorMessage ="Driver name is required")]
        [StringLength(75, ErrorMessage ="Driver name cannot exceed 75 characters")]
        public string driverName { get; set; }

        [Required(ErrorMessage = "Driver Hometown is required")]
        [StringLength(100, ErrorMessage = "Driver Hometown cannot exceed 100 characters")]
        public string driverHomeTown { get; set; }
    }
}
