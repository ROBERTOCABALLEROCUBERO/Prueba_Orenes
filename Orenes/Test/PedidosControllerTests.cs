using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orenes.Controllers;
using Orenes.Models;
using Orenes.Services.Interfaces;
using Orenes.DTO;

namespace Orenes.Test
{
    [TestFixture]
    public class PedidosControllerTests
    {
        private PedidosController _controller;
        private Mock<IPedidoService> _pedidoServiceMock;
        private Mock<IClienteService> _clienteServiceMock;

        [SetUp]
        public void Setup()
        {
            _pedidoServiceMock = new Mock<IPedidoService>();
            _clienteServiceMock = new Mock<IClienteService>();
            _controller = new PedidosController(_pedidoServiceMock.Object, _clienteServiceMock.Object);

            // Simular usuario autenticado
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "testuser")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = claimsPrincipal
                }
            };
        }

        [Test]
        public async Task ObtenerPedido_Existente_DeberiaRetornarPedido()
        {
            // Arrange
            var pedidoId = 1;
            var pedidoMock = new Pedido { PedidoId = pedidoId };
            _pedidoServiceMock.Setup(p => p.ObtenerPedido(pedidoId)).ReturnsAsync(pedidoMock);

            // Act
            var resultado = await _controller.ObtenerPedido(pedidoId);

            // Assert
            Assert.NotNull(resultado.Result);
            var okResult = resultado.Result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(pedidoMock, okResult.Value);
        }

        [Test]
        public async Task ObtenerPedido_NoExistente_DeberiaRetornarNotFound()
        {
            // Arrange
            var pedidoId = 10;
            _pedidoServiceMock.Setup(p => p.ObtenerPedido(pedidoId)).ReturnsAsync((Pedido)null);

            // Act
            var resultado = await _controller.ObtenerPedido(pedidoId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(resultado.Result);
        }

        [Test]
        public async Task PedidosCliente_DeberiaRetornarPedidosCliente()
        {
            // Arrange
            var usuario = "testuser";
            var clienteMock = new Cliente { ClienteId = 1 };
            var pedidosMock = new List<PedidoDTO> { new PedidoDTO { PedidoId = 1 } };
            _clienteServiceMock.Setup(c => c.ObtenerDatosUsuarioPorNombre(usuario)).ReturnsAsync(clienteMock);
            _pedidoServiceMock.Setup(p => p.ObtenerPedidosPorIdUsuario(clienteMock.ClienteId)).ReturnsAsync(pedidosMock);

            // Act
            var resultado = await _controller.PedidosCliente();

            // Assert
            Assert.NotNull(resultado.Result);
            var okResult = resultado.Result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(pedidosMock, okResult.Value);
        }

        [Test]
        public async Task PedidosVehiculo_DeberiaRetornarPedidosVehiculo()
        {
            // Arrange
            var vehiculoId = 1;
            var pedidosMock = new List<PedidoDTO> { new PedidoDTO { PedidoId = 1 } };
            _pedidoServiceMock.Setup(p => p.ObtenerPedidosPorVehiculo(vehiculoId)).ReturnsAsync(pedidosMock);

            // Act
            var resultado = await _controller.PedidosVehiculo(vehiculoId);

            // Assert
            Assert.NotNull(resultado.Result);
            var okResult = resultado.Result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(pedidosMock, okResult.Value);
        }

        [Test]
        public async Task CrearPedido_DeberiaRetornarPedidoCreado()
        {
            // Arrange
            var pedidoMock = new PedidoDTO { PedidoId = 1 };
            var clienteMock = new Cliente { ClienteId = 1 };
            _clienteServiceMock.Setup(c => c.ObtenerDatosUsuarioPorNombre("testuser")).ReturnsAsync(clienteMock);
            _pedidoServiceMock.Setup(p => p.CrearPedido(pedidoMock, clienteMock)).ReturnsAsync(1);

            // Act
            var resultado = await _controller.CrearPedido(pedidoMock);

            // Assert
            Assert.NotNull(resultado.Result);
            var createdResult = resultado.Result as CreatedAtActionResult;
            Assert.NotNull(createdResult);
            Assert.AreEqual(nameof(PedidosController.ObtenerPedido), createdResult.ActionName);
            Assert.AreEqual(1, createdResult.RouteValues["pedidoId"]);
            Assert.AreEqual(pedidoMock, createdResult.Value);
        }

        [Test]
        public async Task MarcarPedidoEnProceso_PedidoExistente_DeberiaRetornarOk()
        {
            // Arrange
            var pedidoId = 1;
            var vehiculoId = 1;
            var pedidoMock = new Pedido { PedidoId = pedidoId };
            _pedidoServiceMock.Setup(p => p.ObtenerPedido(pedidoId)).ReturnsAsync(pedidoMock);

            // Act
            var resultado = await _controller.MarcarPedidoEnProceso(pedidoId, vehiculoId);

            // Assert
            Assert.IsInstanceOf<OkResult>(resultado);
            Assert.AreEqual(EstadoPedido.EnProceso, pedidoMock.status);
            Assert.AreEqual(vehiculoId, pedidoMock.VehiculoId1);
        }

        [Test]
        public async Task MarcarPedidoEnProceso_PedidoNoExistente_DeberiaRetornarNotFound()
        {
            // Arrange
            var pedidoId = 10;
            var vehiculoId = 1;
            _pedidoServiceMock.Setup(p => p.ObtenerPedido(pedidoId)).ReturnsAsync((Pedido)null);

            // Act
            var resultado = await _controller.MarcarPedidoEnProceso(pedidoId, vehiculoId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(resultado);
        }

        [Test]
        public async Task ActualizarPedido_PedidoExistente_DeberiaRetornarNoContent()
        {
            // Arrange
            var pedidoId = 1;
            var pedidoMock = new Pedido { PedidoId = pedidoId };
            _pedidoServiceMock.Setup(p => p.ActualizarPedido(pedidoMock)).ReturnsAsync(true);

            // Act
            var resultado = await _controller.ActualizarPedido(pedidoId, pedidoMock);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(resultado);
        }

        [Test]
        public async Task ActualizarPedido_PedidoNoExistente_DeberiaRetornarNotFound()
        {
            // Arrange
            var pedidoId = 1;
            var pedidoMock = new Pedido { PedidoId = pedidoId };
            _pedidoServiceMock.Setup(p => p.ActualizarPedido(pedidoMock)).ReturnsAsync(false);

            // Act
            var resultado = await _controller.ActualizarPedido(pedidoId, pedidoMock);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(resultado);
        }
    }
}
