using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin,User")]
    [ApiController]
    [Route("api/[controller]")]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentRepository _shipmentRepository;

        public ShipmentController(IShipmentRepository shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shipment>>> GetAll()
        {
            var trucks = await _shipmentRepository.GetAllAsync();
            return Ok(trucks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Shipment>> GetById(int id)
        {
            var shipment = await _shipmentRepository.GetByIdAsync(id);

            if (shipment == null)
                return NotFound();

            return Ok(shipment);
        }

        [HttpGet("truck/{truckId}")]
        public async Task<ActionResult<IEnumerable<Shipment>>> GetShipmentsByTruckId(int truckId)
        {
            var shipments = await _shipmentRepository.GetShipmentsByTruckIdAsync(truckId);
            return Ok(shipments);
        }

        [HttpGet("warehouse/{warehouseId}")]
        public async Task<ActionResult<IEnumerable<Shipment>>> GetShipmentsByWarehouseId(int warehouseId)
        {
            var shipments = await _shipmentRepository.GetShipmentsByWarehouseIdAsync(warehouseId);
            return Ok(shipments);
        }

        [HttpPost]
        public async Task<ActionResult<Shipment>> CreateShipment(Shipment shipment)
        {
            var createdShipment = await _shipmentRepository.AddAsync(shipment);
            return CreatedAtAction(nameof(GetById), new { id = shipment.Id }, createdShipment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShipment(int id, Shipment shipment)
        {
            if (id != shipment.Id)
                return BadRequest();

            await _shipmentRepository.UpdateAsync(shipment);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShipment(int id)
        {
            await _shipmentRepository.DeleteAsync(id);
            return NoContent();
        }

    }
}
