using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TruckController : ControllerBase
    {
        private readonly ITruckRepository _truckRepository;

        public TruckController(ITruckRepository truckRepository)
        {
            _truckRepository = truckRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Truck>>> GetAll()
        {
            var trucks = await _truckRepository.GetAllAsync();
            return Ok(trucks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Truck>> GetById(int id)
        {
            var truck = await _truckRepository.GetByIdAsync(id);

            if (truck == null)
                return NotFound();

            return Ok(truck);
        }

        [HttpGet("licensePlate/{licensePlate}")]
        public async Task<ActionResult<Truck>> GetTruckByLicensePlate(string licensePlate)
        {
            var truck = await _truckRepository.GetByLicensePlateAsync(licensePlate);
            if (truck == null)
            {
                return NotFound();
            }
            return truck;
        }

        [HttpGet("driverCard/{driverCardNumber}")]
        public async Task<ActionResult<Truck>> GetTruckByDriverCardNumber(string driverCardNumber)
        {
            var truck = await _truckRepository.GetByDriverCardNumberAsync(driverCardNumber);
            if (truck == null)
            {
                return NotFound();
            }
            return truck;
        }

        [HttpPost]
        public async Task<ActionResult<Truck>> CreateTruck(Truck truck)
        {
            var createdTruck = await _truckRepository.AddAsync(truck);
            return CreatedAtAction(nameof(GetById), new { id = createdTruck.Id }, createdTruck);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTruck(int id, Truck truck)
        {
            if (id != truck.Id)
                return BadRequest();

            await _truckRepository.UpdateAsync(truck);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTruck(int id)
        {
            await _truckRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
