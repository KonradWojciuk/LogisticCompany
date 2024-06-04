using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseRepository _warehouseRepository;

        public WarehouseController(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Warehouse>>> GetAll()
        {
            var werhouses = await _warehouseRepository.GetAllAsync();
            return Ok(werhouses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Truck>> GetById(int id)
        {
            var truck = await _warehouseRepository.GetByIdAsync(id);

            if (truck == null)
                return NotFound();

            return Ok(truck);
        }

        [HttpGet("country/{country}")]
        public async Task<ActionResult<IEnumerable<Warehouse>>> GetWarehousesByCountry(string country)
        {
            var warehouses = await _warehouseRepository.GetByCountryAsync(country);
            return Ok(warehouses);
        }

        [HttpGet("city/{city}")]
        public async Task<ActionResult<IEnumerable<Warehouse>>> GetWarehousesByCity(string city)
        {
            var warehouses = await _warehouseRepository.GetByCityAsync(city);
            return Ok(warehouses);
        }

        [HttpPost]
        public async Task<ActionResult<Warehouse>> CreateWarehouse(Warehouse warehouse)
        {
            var createdWarehouse = await _warehouseRepository.AddAsync(warehouse);

            return CreatedAtAction(nameof(GetById), new { id = createdWarehouse.Id }, createdWarehouse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWarehouse(int id, Warehouse warehouse)
        {
            if (id != warehouse.Id)
                return BadRequest();

            await _warehouseRepository.UpdateAsync(warehouse);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarehouse(int id)
        {
            await _warehouseRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
