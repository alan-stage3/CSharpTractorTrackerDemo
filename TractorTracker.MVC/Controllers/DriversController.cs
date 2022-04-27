using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TractorTracker.Core;
using TractorTracker.Core.Entities;
using TractorTracker.Core.Interfaces;

namespace TractorTracker.MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IDriverRepository _driverRepo;

        public DriversController(IDriverRepository driverRepo)
        {
            _driverRepo = driverRepo;
        }

        [HttpGet]
        public IActionResult GetDrivers()
        {
            Result<List<Driver>> result = _driverRepo.GetAll();

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Messages);
            }

        }

    }
}
