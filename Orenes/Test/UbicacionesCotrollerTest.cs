using Moq;
using NUnit.Framework;
using Orenes.Controllers;
using Orenes.DTO;
using Orenes.Models;
using Orenes.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Orenes.Test
{
    [TestFixture]
    public class UbicacionesControllerTests
    {
        private UbicacionesController _controller;
        private Mock<IUbicacionService> _ubicacionesServiceMock;

        [SetUp]
        public void Setup()
        {
            _ubicacionesServiceMock = new Mock<IUbicacionService>();
            _controller = new UbicacionesController(_ubicacionesServiceMock.Object);
        }

        [Test]
        public async Task GetUbicaciones_DeberiaRetornarListaDeUbicaciones()
        {
            // Arrange
            var ubicacionesMock = new List<Ubicacion> { new Ubicacion { UbicacionId = 1 }, new Ubicacion { UbicacionId = 2 } };
            _ubicacionesServiceMock.Setup(u => u.ObtenerUbicaciones()).ReturnsAsync(ubicacionesMock);

            // Act
            var resultado = await _controller.GetUbicaciones();

            // Assert
            Assert.NotNull(resultado.Result);
            var okResult = resultado.Result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(ubicacionesMock, okResult.Value);
        }

        [Test]
        public async Task GetUbicacion_UbicacionExistente_DeberiaRetornarUbicacion()
        {
            // Arrange
            var ubicacionId = 3;
            var ubicacionMock = new Ubicacion { UbicacionId = ubicacionId };
            _ubicacionesServiceMock.Setup(u => u.ObtenerUbicacion(ubicacionId)).ReturnsAsync(ubicacionMock);

            // Act
            var resultado = await _controller.GetUbicacion(ubicacionId);

            // Assert
            Assert.NotNull(resultado.Value);
            Assert.AreEqual(ubicacionMock, resultado.Value);
        }

        [Test]
        public async Task GetUbicacion_UbicacionNoExistente_DeberiaRetornarNotFound()
        {
            // Arrange
            var ubicacionId = 10;
            _ubicacionesServiceMock.Setup(u => u.ObtenerUbicacion(ubicacionId)).ReturnsAsync((Ubicacion)null);

            // Act
            var resultado = await _controller.GetUbicacion(ubicacionId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(resultado.Result);
        }

        [Test]
        public async Task PutUbicacion_UbicacionExistente_DeberiaRetornarNoContent()
        {
            // Arrange
            var ubicacionId = 1;
            var ubicacionMock = new Ubicacion { UbicacionId = ubicacionId };
            _ubicacionesServiceMock.Setup(u => u.ActualizarUbicacion(ubicacionId, ubicacionMock)).ReturnsAsync(true);

            // Act
            var resultado = await _controller.PutUbicacion(ubicacionId, ubicacionMock);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(resultado);
        }

        [Test]
        public async Task PutUbicacion_UbicacionNoExistente_DeberiaRetornarBadRequest()
        {
            // Arrange
            var ubicacionId = 1;
            var ubicacionMock = new Ubicacion { UbicacionId = ubicacionId };
            _ubicacionesServiceMock.Setup(u => u.ActualizarUbicacion(ubicacionId, ubicacionMock)).ReturnsAsync(false);

            // Act
            var resultado = await _controller.PutUbicacion(ubicacionId, ubicacionMock);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(resultado);
        }

        [Test]
        public async Task PostUbicacion_DeberiaRetornarUbicacionCreada()
        {
            // Arrange
            var ubicacionDTO = new UbicacionDTO { UbicacionId = 1 };
            var ubicacionCreada = new UbicacionDTO { UbicacionId = ubicacionDTO.UbicacionId };
            _ubicacionesServiceMock.Setup(u => u.AgregarUbicacion(ubicacionDTO)).ReturnsAsync(ubicacionCreada);

            // Act
            var resultado = await _controller.PostUbicacion(ubicacionDTO);

            // Assert
            Assert.NotNull(resultado.Result);
            var createdResult = resultado.Result as CreatedAtActionResult;
            Assert.NotNull(createdResult);
            Assert.AreEqual("GetUbicacion", createdResult.ActionName);
            Assert.AreEqual(ubicacionCreada, createdResult.Value);
        }

        [Test]
        public async Task DeleteUbicacion_UbicacionExistente_DeberiaRetornarNoContent()
        {
            // Arrange
            var ubicacionId = 1;
            _ubicacionesServiceMock.Setup(u => u.EliminarUbicacion(ubicacionId)).ReturnsAsync(true);

            // Act
            var resultado = await _controller.DeleteUbicacion(ubicacionId);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(resultado);
        }

        [Test]
        public async Task DeleteUbicacion_UbicacionNoExistente_DeberiaRetornarNotFound()
        {
            // Arrange
            var ubicacionId = 1;
            _ubicacionesServiceMock.Setup(u => u.EliminarUbicacion(ubicacionId)).ReturnsAsync(false);

            // Act
            var resultado = await _controller.DeleteUbicacion(ubicacionId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(resultado);
        }

        [Test]
        public async Task ObtenerUbicacionMasReciente_UbicacionExistente_DeberiaRetornarUbicacion()
        {
            // Arrange
            var pedidoId = 1;
            var ubicacionMock = new Ubicacion { UbicacionId = 1 };
            _ubicacionesServiceMock.Setup(u => u.ObtenerUbicacionMasReciente(pedidoId)).ReturnsAsync(ubicacionMock);

            // Act
            var resultado = await _controller.ObtenerUbicacionMasReciente(pedidoId);

            // Assert
            Assert.NotNull(resultado.Value);
            Assert.AreEqual(ubicacionMock, resultado.Value);
        }

        [Test]
        public async Task ObtenerUbicacionMasReciente_UbicacionNoExistente_DeberiaRetornarNotFound()
        {
            // Arrange
            var pedidoId = 1;
            _ubicacionesServiceMock.Setup(u => u.ObtenerUbicacionMasReciente(pedidoId)).ReturnsAsync((Ubicacion)null);

            // Act
            var resultado = await _controller.ObtenerUbicacionMasReciente(pedidoId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(resultado.Result);
        }
    }
}
