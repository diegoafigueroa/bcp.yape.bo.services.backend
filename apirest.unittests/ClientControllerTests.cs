using Moq;
using System.Threading.Tasks;
using bcp.yape.bo.core.Ports.Driving;
using bcp.yape.bo.services.apirest.Controllers;
using bcp.yape.bo.services.apirest.DTO;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using bcp.yape.bo.core.Entities;

namespace bcp.yape.bo.services.apirest.Tests
{
    public class ClientControllerTests
    {
        private readonly Mock<IClientService> _mockClientService;
        private readonly ClientController _controller;

        // Constructor para inicializar el mock y el controlador
        public ClientControllerTests()
        {
            _mockClientService = new Mock<IClientService>();
            _controller = new ClientController(_mockClientService.Object);
        }

        // Prueba para crear un cliente con éxito
        [Fact]
        public async Task CreateClient_ShouldReturnCreatedAtAction_WhenClientIsSuccessfullyCreated()
        {
            // Arrange: Prepara los datos de entrada y el comportamiento del mock
            var request = new ClientRegistrationRequest
            {
                Name = "Juan",
                LastName = "Pérez",
                CellPhoneNumber = "67654321",
                DocumentType = DocumentTypeEnum.IdentityCard,
                DocumentNumber = "12345678",
                ReasonOfUse = "Compra"
            };

            var expectedGuid = Guid.NewGuid();
            var expectedValidationResult = new ValidationResult(true, "", expectedGuid);
            _mockClientService.Setup(service => service.AddClientAsync(It.IsAny<ClientRegistrationData>())).Returns(Task.FromResult(expectedValidationResult));

            // Act: Llama al método que deseas probar
            var result = await _controller.CreateClient(request);

            // Assert: Verifica que el resultado sea el esperado
            var actionResult = result as CreatedAtActionResult;
            Assert.NotNull(actionResult);
            Assert.Equal(201, actionResult.StatusCode);
        }

    }
}
