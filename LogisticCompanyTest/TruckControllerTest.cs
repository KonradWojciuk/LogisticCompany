using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Controllers;

namespace LogisticCompanyTest
{
    [TestClass]
    public class TruckControllerTest
    {
        private Mock<ITruckRepository> _mockTruckRepository;
        private TruckController _truckController;

        [TestInitialize]
        public void Setup()
        {
            _mockTruckRepository = new Mock<ITruckRepository>();
            _truckController = new TruckController(_mockTruckRepository.Object);
        }

        [TestMethod]
        public async Task GetAll_ReturnsOkResult_WithListOfTrucks()
        {
            // Arrange
            var trucks = new List<Truck> { new Truck { Id = 1 }, new Truck { Id = 2 } };
            _mockTruckRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(trucks);

            // Act
            var result = await _truckController.GetAll();

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedTrucks = okResult.Value as IEnumerable<Truck>;
            Assert.AreEqual(2, returnedTrucks.Count());
        }

        [TestMethod]
        public async Task GetById_ReturnsNotFound_WhenTruckDoesNotExist()
        {
            // Arrange
            _mockTruckRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Truck)null);

            // Act
            var result = await _truckController.GetById(1);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetById_ReturnsOkResult_WithTruck()
        {
            // Arrange
            var truck = new Truck { Id = 1 };
            _mockTruckRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(truck);

            // Act
            var result = await _truckController.GetById(1);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedTruck = okResult.Value as Truck;
            Assert.AreEqual(1, returnedTruck.Id);
        }

        [TestMethod]
        public async Task GetTruckByLicensePlate_ReturnsNotFound_WhenTruckDoesNotExist()
        {
            // Arrange
            _mockTruckRepository.Setup(repo => repo.GetByLicensePlateAsync(It.IsAny<string>())).ReturnsAsync((Truck)null);

            // Act
            var result = await _truckController.GetTruckByLicensePlate("ABC123");

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetTruckByLicensePlate_ReturnsTruck_WhenFound()
        {
            // Arrange
            string licensePlate = "ABC123";
            var expectedTruck = new Truck { LicensePlate = licensePlate };
            _mockTruckRepository.Setup(repo => repo.GetByLicensePlateAsync(licensePlate)).ReturnsAsync(expectedTruck);

            // Act
            var result = await _truckController.GetTruckByLicensePlate(licensePlate);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedTruck, result.Value);
        }

        [TestMethod]
        public async Task GetTruckByDriverCardNumber_ReturnsTruck_WhenFound()
        {
            // Arrange
            string driverCardNumber = "1234567890";
            var expectedTruck = new Truck { DriverCardNumber = driverCardNumber };
            _mockTruckRepository.Setup(repo => repo.GetByDriverCardNumberAsync(driverCardNumber)).ReturnsAsync(expectedTruck);

            // Act
            var result = await _truckController.GetTruckByDriverCardNumber(driverCardNumber);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedTruck, result.Value);
        }


        [TestMethod]
        public async Task GetTruckByDriverCardNumber_ReturnsNotFound_WhenTruckDoesNotExist()
        {
            // Arrange
            _mockTruckRepository.Setup(repo => repo.GetByDriverCardNumberAsync(It.IsAny<string>())).ReturnsAsync((Truck)null);

            // Act
            var result = await _truckController.GetTruckByDriverCardNumber("123456");

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }


            [TestMethod]
        public async Task CreateTruck_ReturnsCreatedAtActionResult_WithCreatedTruck()
        {
            // Arrange
            var truck = new Truck { Id = 1 };
            _mockTruckRepository.Setup(repo => repo.AddAsync(It.IsAny<Truck>())).ReturnsAsync(truck);

            // Act
            var result = await _truckController.CreateTruck(truck);

            // Assert
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            var createdTruck = createdAtActionResult.Value as Truck;
            Assert.AreEqual(1, createdTruck.Id);
        }

        [TestMethod]
        public async Task UpdateTruck_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var truck = new Truck { Id = 2 };

            // Act
            var result = await _truckController.UpdateTruck(1, truck);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task UpdateTruck_ReturnsOkResult_WhenUpdateIsSuccessful()
        {
            // Arrange
            var truck = new Truck { Id = 1, DriverFirstName = "UpdatedFirstName" };
            _mockTruckRepository.Setup(repo => repo.UpdateAsync(truck)).ReturnsAsync(truck);

            // Act
            var result = await _truckController.UpdateTruck(1, truck);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task DeleteTruck_ReturnsOkResult_WhenDeletionIsSuccessful()
        {
            // Arrange
            var truck = new Truck { Id = 1, LicensePlate = "ABC123" };

            _mockTruckRepository.Setup(repo => repo.DeleteAsync(1))
                .ReturnsAsync(truck);

            // Act
            var result = await _truckController.DeleteTruck(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }
    }
}
