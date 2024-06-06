using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web.Controllers;


namespace LogisticCompanyTest
{
    [TestClass]
    public class WarehouseControllerTest
    {
        private Mock<IWarehouseRepository> _mockWarehouseRepository;
        private WarehouseController _warehouseController;

        [TestInitialize]
        public void Setup()
        {
            _mockWarehouseRepository = new Mock<IWarehouseRepository>();
            _warehouseController = new WarehouseController(_mockWarehouseRepository.Object);
        }

        [TestMethod]
        public async Task GetAll_ReturnsOkResult_WithListOfWarehouses()
        {
            // Arrange
            var warehouses = new List<Warehouse> { new Warehouse { Id = 1 }, new Warehouse { Id = 2 } };
            _mockWarehouseRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(warehouses);

            // Act
            var result = await _warehouseController.GetAll();

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedWarehouses = okResult.Value as IEnumerable<Warehouse>;
            Assert.AreEqual(2, returnedWarehouses.Count());
        }

        [TestMethod]
        public async Task GetById_ReturnsNotFound_WhenWarehouseDoesNotExist()
        {
            // Arrange
            _mockWarehouseRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Warehouse)null);

            // Act
            var result = await _warehouseController.GetById(1);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetById_ReturnsOkResult_WithWarehouse()
        {
            // Arrange
            var warehouse = new Warehouse { Id = 1 };
            _mockWarehouseRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(warehouse);

            // Act
            var result = await _warehouseController.GetById(1);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedWarehouse = okResult.Value as Warehouse;
            Assert.AreEqual(1, returnedWarehouse.Id);
        }

        [TestMethod]
        public async Task GetWarehousesByCountry_ReturnsWarehousesForGivenCountry()
        {
            // Arrange
            var country = "Poland";
            var warehouses = new List<Warehouse> { new Warehouse { Id = 1, Country = country }, new Warehouse { Id = 2, Country = country } };
            _mockWarehouseRepository.Setup(repo => repo.GetByCountryAsync(country)).ReturnsAsync(warehouses);

            // Act
            var result = await _warehouseController.GetWarehousesByCountry(country);

            // Assert
            var okResult = result.Result as OkObjectResult;
            var model = okResult.Value as IEnumerable<Warehouse>;
            Assert.IsNotNull(model);
            Assert.AreEqual(2, model.Count());
        }

        [TestMethod]
        public async Task GetWarehousesByCity_ReturnsWarehousesForGivenCity()
        {
            // Arrange
            var city = "Warsaw";
            var warehouses = new List<Warehouse> { new Warehouse { Id = 1, City = city }, new Warehouse { Id = 2, City = city } };
            _mockWarehouseRepository.Setup(repo => repo.GetByCityAsync(city)).ReturnsAsync(warehouses);

            // Act
            var result = await _warehouseController.GetWarehousesByCity(city);

            // Assert
            var okResult = result.Result as OkObjectResult;
            var model = okResult.Value as IEnumerable<Warehouse>;
            Assert.IsNotNull(model);
            Assert.AreEqual(2, model.Count());
        }

        [TestMethod]
        public async Task CreateWarehouse_ReturnsCreatedAtActionResult_WithCreatedWarehouse()
        {
            // Arrange
            var warehouse = new Warehouse { Id = 1 };
            _mockWarehouseRepository.Setup(repo => repo.AddAsync(It.IsAny<Warehouse>())).ReturnsAsync(warehouse);

            // Act
            var result = await _warehouseController.CreateWarehouse(warehouse);

            // Assert
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            var createdWarehouse = createdAtActionResult.Value as Warehouse;
            Assert.AreEqual(1, createdWarehouse.Id);
        }

        [TestMethod]
        public async Task UpdateWarehouse_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var warehouse = new Warehouse { Id = 2 };

            // Act
            var result = await _warehouseController.UpdateWarehouse(1, warehouse);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task UpdateWarehouse_ReturnsOkResult_WhenUpdateIsSuccessful()
        {
            // Arrange
            var warehouse = new Warehouse { Id = 1, City = "Poznań" };

            _mockWarehouseRepository.Setup(repo => repo.UpdateAsync(warehouse))
                .ReturnsAsync(warehouse);

            // Act
            var result = await _warehouseController.UpdateWarehouse(1, warehouse);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task DeleteWarehouse_ReturnsOkResult_WhenDeletionIsSuccessful()
        {
            // Arrange
            var warehouse = new Warehouse { Id = 1, City = "Katowice" };

            _mockWarehouseRepository.Setup(repo => repo.DeleteAsync(1))
                .ReturnsAsync(warehouse);

            // Act
            var result = await _warehouseController.DeleteWarehouse(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
    }
}
