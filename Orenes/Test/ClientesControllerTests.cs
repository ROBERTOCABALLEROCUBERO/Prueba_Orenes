using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NuGet.ContentModel;
using Orenes.Controllers;
using Orenes.Models;
using Orenes.Services.Interfaces;


namespace Orenes.Test
{

    [TestFixture]
    public class ClientesControllerTests
    {
        private ClientesController _controller;
        private Mock<IClienteService> _clienteServiceMock;
        private Mock<ISecurityService> _securityServiceMock;

        [SetUp]
        public void Setup()
        {
            _clienteServiceMock = new Mock<IClienteService>();
            _securityServiceMock = new Mock<ISecurityService>();
            _controller = new ClientesController(_clienteServiceMock.Object, _securityServiceMock.Object);
        }

        [Test]
        public async Task GetCliente_Existente_DeberiaRetornarCliente()
        {
            // Arrange: Configurar el escenario de prueba
            var clienteId = 1;
            var clienteMock = new Cliente { ClienteId = clienteId };
            _clienteServiceMock.Setup(c => c.ObtenerClientePorId(clienteId)).ReturnsAsync(clienteMock);

            // Act: Ejecutar el método bajo prueba
            var resultado = await _controller.GetCliente(clienteId);

            // Assert: Verificar el resultado esperado
            Assert.NotNull(resultado.Result);
            var okResult = resultado.Result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(clienteMock, okResult.Value);
        }

        [Test]
        public async Task GetCliente_NoExistente_DeberiaRetornarNotFound()
        {
            // Arrange: Configurar el escenario de prueba
            var clienteId = 10;
            _clienteServiceMock.Setup(c => c.ObtenerClientePorId(clienteId)).ReturnsAsync((Cliente)null);

            // Act: Ejecutar el método bajo prueba
            var resultado = await _controller.GetCliente(clienteId);

            // Assert: Verificar el resultado esperado
            Assert.IsInstanceOf<NotFoundResult>(resultado.Result);
        }
    }

}