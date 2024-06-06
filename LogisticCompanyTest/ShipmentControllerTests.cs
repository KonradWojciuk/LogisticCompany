using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web.Controllers;

namespace LogisticCompanyTest
{
    [TestClass]
    public class ShipmentControllerTests
    {
        private Mock<IShipmentRepository> _mockShipmentRepository;
        private ShipmentController _shipmentController;

        [TestInitialize]
        public void Setup()
        {
            _mockShipmentRepository = new Mock<IShipmentRepository>();
            _shipmentController = new ShipmentController(_mockShipmentRepository.Object);
        }


        [TestMethod]
        public async Task GetAll_ReturnsOkResult_WithListOfShipments()
        {
            // Arrange
            var shipments = new List<Shipment> { new Shipment { Id = 1 }, new Shipment { Id = 2 } };
            _mockShipmentRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(shipments);

            // Act
            var result = await _shipmentController.GetAll();

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedShipments = okResult.Value as IEnumerable<Shipment>;
            Assert.AreEqual(2, returnedShipments.Count());
        }

        [TestMethod]
        public async Task GetById_ReturnsNotFound_WhenShipmentDoesNotExist()
        {
            // Arrange
            _mockShipmentRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Shipment)null);

            // Act
            var result = await _shipmentController.GetById(1);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetById_ReturnsOkResult_WithShipment()
        {
            // Arrange
            var shipment = new Shipment { Id = 1 };
            _mockShipmentRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(shipment);

            // Act
            var result = await _shipmentController.GetById(1);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedShipment = okResult.Value as Shipment;
            Assert.AreEqual(1, returnedShipment.Id);
        }

        [TestMethod]
        public async Task GetShipmentsByTruckId_ReturnsShipmentsForGivenTruckId()
        {
            // Arrange
            int truckId = 1;
            var mockRepository = new Mock<IShipmentRepository>();
            var shipments = new List<Shipment> { new Shipment { Id = 1, TruckId = truckId }, new Shipment { Id = 2, TruckId = truckId } };
            mockRepository.Setup(repo => repo.GetShipmentsByTruckIdAsync(truckId)).ReturnsAsync(shipments);
            var controller = new ShipmentController(mockRepository.Object);

            // Act
            var result = await controller.GetShipmentsByTruckId(truckId);

            // Assert
            var okResult = result.Result as OkObjectResult;
            var model = okResult.Value as IEnumerable<Shipment>;
            Assert.IsNotNull(model);
            Assert.AreEqual(2, model.Count());
        }

        [TestMethod]
        public async Task GetShipmentsByWarehouseId_ReturnsShipmentsForGivenWarehouseId()
        {
            // Arrange
            int warehouseId = 1;
            var mockRepository = new Mock<IShipmentRepository>();
            var shipments = new List<Shipment> { new Shipment { Id = 1, WarehouseId = warehouseId }, new Shipment { Id = 2, WarehouseId = warehouseId } };
            mockRepository.Setup(repo => repo.GetShipmentsByWarehouseIdAsync(warehouseId)).ReturnsAsync(shipments);
            var controller = new ShipmentController(mockRepository.Object);

            // Act
            var result = await controller.GetShipmentsByWarehouseId(warehouseId);

            // Assert
            var okResult = result.Result as OkObjectResult;
            var model = okResult.Value as IEnumerable<Shipment>;
            Assert.IsNotNull(model);
            Assert.AreEqual(2, model.Count());
        }

        [TestMethod]
        public async Task CreateShipment_ReturnsCreatedAtActionResult_WithCreatedShipment()
        {
            // Arrange
            var shipment = new Shipment { Id = 1 };
            _mockShipmentRepository.Setup(repo => repo.AddAsync(It.IsAny<Shipment>())).ReturnsAsync(shipment);

            // Act
            var result = await _shipmentController.CreateShipment(shipment);

            // Assert
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            var createdShipment = createdAtActionResult.Value as Shipment;
            Assert.AreEqual(1, createdShipment.Id);
        }

        [TestMethod]
        public async Task UpdateShipment_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var shipment = new Shipment { Id = 2 };

            // Act
            var result = await _shipmentController.UpdateShipment(1, shipment);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task UpdateShipment_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var shipment = new Shipment { Id = 1, Status = ShipmentStatus.Pending };

            _mockShipmentRepository.Setup(repo => repo.UpdateAsync(shipment))
                .ReturnsAsync(shipment);

            // Act
            var result = await _shipmentController.UpdateShipment(1, shipment);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task DeleteShipment_ReturnsNoContent_WhenDeletionIsSuccessful()
        {
            // Arrange
            var shipment = new Shipment { Id = 1, Status = ShipmentStatus.Pending };

            _mockShipmentRepository.Setup(repo => repo.DeleteAsync(1))
                .ReturnsAsync(shipment);

            // Act
            var result = await _shipmentController.DeleteShipment(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
    }
}