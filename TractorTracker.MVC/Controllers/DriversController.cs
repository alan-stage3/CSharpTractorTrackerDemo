using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TractorTracker.Core;
using TractorTracker.Core.Entities;
using TractorTracker.Core.Interfaces;
using TractorTracker.MVC.Models;

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

        [HttpGet]
        [Route("/api/[controller]/{id}", Name = "GetDriver")]
        public IActionResult GetDriver(int id)
        {
            Result<Driver> result = _driverRepo.Get(id);

            if(result.Success)
            {
                if(result.Messages.Count >0)
                {
                    return Ok(result.Messages);
                }
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Messages);
            }

        }

        [HttpPost]
        public IActionResult AddDriver(ViewDriver viewDriver)
        {
            if (ModelState.IsValid)
            {
                Driver driver = new Driver
                {
                    driverName = viewDriver.driverName,
                    driverHometown = viewDriver.driverHomeTown
                };

                var result = _driverRepo.Add(driver);

                if (result.Success)
                {
                    return CreatedAtRoute(nameof(GetDriver), new { id = result.Data.driverId }, result.Data);
                }
                else
                {
                    return BadRequest(result.Messages);
                }
            } else
            {
                return BadRequest(ModelState);

            }
        }

        [HttpDelete("{driverId}")]
        public IActionResult DeleteDriver(int driverId)
        {
            var findResult = _driverRepo.Get(driverId);
            if (findResult.Messages.Count > 0)
            {
                return NotFound(findResult.Messages);
            }

            var result = _driverRepo.Delete(driverId);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Messages);
            }
        }

        [HttpPut]
        public IActionResult EditDriver(ViewDriver viewDriver)
        {
            if (ModelState.IsValid && viewDriver.driverId > 0)
            {
                Driver driver = new Driver
                {
                    driverId = viewDriver.driverId,
                    driverName = viewDriver.driverName,
                    driverHometown = viewDriver.driverHomeTown
                };

                var findResult = _driverRepo.Get(driver.driverId);
                if (findResult.Messages.Count > 0)
                {
                    return NotFound(findResult.Messages);
                }

                var result = _driverRepo.Edit(driver);
                if (result.Success)
                {
                    return Ok(result.Data);
                }
                else
                {
                    return BadRequest(result.Messages);
                }
            }
            else
            {
                if (viewDriver.driverId < 1)
                    ModelState.AddModelError("driverId", "Invalid Driver ID");
                return BadRequest(ModelState);
            }
        }

    }
}
