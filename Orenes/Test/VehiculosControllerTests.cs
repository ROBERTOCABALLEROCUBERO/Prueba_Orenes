using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Orenes.Controllers;
using Orenes.Models;
using Orenes.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orenes.Test
{
    [TestFixture]
    public class VehiculosControllerTests
    {
        private VehiculosController _controller;
        private Mock<IVehiculoService> _vehiculosServiceMock;

        [SetUp]
        public void Setup()
        {
            _vehiculosServiceMock = new Mock<IVehiculoService>();
            _controller = new VehiculosController(_vehiculosServiceMock.Object);
        }

        [Test]
        public async Task GetVehiculos_DeberiaRetornarListaDeVehiculos()
        {
            // Arrange
            var vehiculosMock = new List<Vehiculo> { new Vehiculo(), new Vehiculo() };
            _vehiculosServiceMock.Setup(v => v.ObtenerVehiculos()).ReturnsAsync(vehiculosMock);

            // Act
            var resultado = await _controller.GetVehiculos();

            // Assert
            Assert.NotNull(resultado.Result);
            var okResult = resultado.Result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(vehiculosMock, okResult.Value);
        }

        [Test]
        public async Task GetVehiculo_VehiculoExistente_DeberiaRetornarVehiculo()
        {
            // Arrange
            var vehiculoId = 3;
            var vehiculoMock = new Vehiculo { VehiculoId = vehiculoId };
            _vehiculosServiceMock.Setup(v => v.ObtenerVehiculo(vehiculoId)).ReturnsAsync(vehiculoMock);

            // Act
            var resultado = await _controller.GetVehiculo(vehiculoId);

            // Assert
            Assert.NotNull(resultado.Value);
            Assert.AreEqual(vehiculoMock, resultado.Value);
        }

        [Test]
        public async Task GetVehiculo_VehiculoNoExistente_DeberiaRetornarNotFound()
        {
            // Arrange
            var vehiculoId = 3;
            _vehiculosServiceMock.Setup(v => v.ObtenerVehiculo(vehiculoId)).ReturnsAsync((Vehiculo)null);

            // Act
            var resultado = await _controller.GetVehiculo(vehiculoId);

            // Assert
            Assert.NotNull(resultado.Result);
            Assert.IsInstanceOf<NotFoundResult>(resultado.Result);
        }

        [Test]
        public async Task PutVehiculo_VehiculoExistente_DeberiaRetornarNoContent()
        {
            // Arrange
            var vehiculoId = 3;
            var vehiculoMock = new Vehiculo { VehiculoId = vehiculoId };
            _vehiculosServiceMock.Setup(v => v.ActualizarVehiculo(vehiculoId, vehiculoMock)).ReturnsAsync(true);

            // Act
            var resultado = await _controller.PutVehiculo(vehiculoId, vehiculoMock);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsInstanceOf<NoContentResult>(resultado);
        }

        [Test]
        public async Task PutVehiculo_VehiculoNoExistente_DeberiaRetornarBadRequest()
        {
            // Arrange
            var vehiculoId = 3;
            var vehiculoMock = new Vehiculo { VehiculoId = vehiculoId };
            _vehiculosServiceMock.Setup(v => v.ActualizarVehiculo(vehiculoId, vehiculoMock)).ReturnsAsync(false);

            // Act
            var resultado = await _controller.PutVehiculo(vehiculoId, vehiculoMock);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsInstanceOf<BadRequestResult>(resultado);
        }

        [Test]
        public async Task PostVehiculo_DeberiaCrearNuevoVehiculoYRetornarlo()
        {
            // Arrange
            var vehiculoMock = new Vehiculo { VehiculoId = 3 };
            _vehiculosServiceMock.Setup(v => v.AgregarVehiculo(vehiculoMock)).ReturnsAsync(vehiculoMock);

            // Act
            var resultado = await _controller.PostVehiculo(vehiculoMock);

            // Assert
            Assert.NotNull(resultado.Result);
            var createdResult = resultado.Result as CreatedAtActionResult;
            Assert.NotNull(createdResult);
            Assert.AreEqual(nameof(VehiculosController.GetVehiculo), createdResult.ActionName);
            Assert.AreEqual(vehiculoMock.VehiculoId, createdResult.RouteValues["id"]);
            Assert.AreEqual(vehiculoMock, createdResult.Value);
        }

        [Test]
        public async Task DeleteVehiculo_VehiculoExistente_DeberiaRetornarNoContent()
        {
            // Arrange
            var vehiculoId = 3;
            _vehiculosServiceMock.Setup(v => v.EliminarVehiculo(vehiculoId)).ReturnsAsync(true);

            // Act
            var resultado = await _controller.DeleteVehiculo(vehiculoId);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsInstanceOf<NoContentResult>(resultado);
        }

        [Test]
        public async Task DeleteVehiculo_VehiculoNoExistente_DeberiaRetornarNotFound()
        {
            // Arrange
            var vehiculoId = 3;
            _vehiculosServiceMock.Setup(v => v.EliminarVehiculo(vehiculoId)).ReturnsAsync(false);

            // Act
            var resultado = await _controller.DeleteVehiculo(vehiculoId);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsInstanceOf<NotFoundResult>(resultado);
        }
    }
}
