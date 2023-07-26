using Microsoft.AspNetCore.Mvc;
using ParkingManager.Interface;
using ParkingManager.Model.Entities;
using System.Globalization;

namespace ParkingManager.Controllers
{
    public class ParkingController : ControllerBase
    {
        private readonly IParkingManagerRepository _parkingService;

        public ParkingController(IParkingManagerRepository parkingService)
        {
            _parkingService = parkingService;
        }

        [HttpGet("CalculateTotalCost")]
        public IActionResult CalculateTotalCost(string entryTime, string exitTime)
        {
            var parkingTotalCost =  _parkingService.CalculateTotalCost(entryTime, exitTime);
            if (parkingTotalCost < 0)
            {
                return BadRequest("Parking Total Cost cannot be less than 0");
            }
            return Ok(parkingTotalCost);
        }

        [HttpGet("GetTicketsByDate")]
        public IActionResult GetTicketsByDate(DateTime date)
        {
            var getTicket = _parkingService.GetTicketsByDate(date);
            if (getTicket == null)
            {
                return NotFound("Sorry we could not get Ticket at thsi time");
            }
            return Ok(getTicket);
        }
    }
}
