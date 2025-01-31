using bcp.yape.bo.core.Entities;
using bcp.yape.bo.core.Ports.Driven;
using bcp.yape.bo.core.Ports.Driving;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.unittests
{
    [TestClass]
    public class ClientServiceTests
    {
        private Mock<IClientRepositoryPort> _clientRepositoryMock;
        private Mock<IPeopleServicePort> _peopleServiceMock;
        private IClientService _clientService;

        [TestInitialize]
        public void Setup()
        {
            _clientRepositoryMock = new Mock<IClientRepositoryPort>();
            _peopleServiceMock = new Mock<IPeopleServicePort>();
            _clientService = new ClientService(_clientRepositoryMock.Object, _peopleServiceMock.Object);
        }

        [TestMethod]
        public async Task AddClientAsync_PhoneNumberAlreadyRegistered_ReturnsValidationError()
        {
            // Arrange
            var registrationData = new ClientRegistrationData("John", "Doe", "61234567", DocumentTypeEnum.IdentityCard, "12345678", "Reason of use", "", "", "", DateTime.UtcNow);
            _clientRepositoryMock.Setup(x => x.IsPhoneNumberRegisteredAsync(registrationData.CellPhoneNumber)).ReturnsAsync(true);

            // Act
            var result = await _clientService.AddClientAsync(registrationData);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("El número de celular ya está registrado.", result.Message);
            _clientRepositoryMock.Verify(x => x.IsPhoneNumberRegisteredAsync(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task AddClientAsync_NoMatchingPeople_ReturnsValidationError()
        {
            // Arrange
            var registrationData = new ClientRegistrationData("John", "Doe", "61234567", DocumentTypeEnum.IdentityCard, "12345678", "Reason of use", "", "", "", DateTime.UtcNow);
            _clientRepositoryMock.Setup(x => x.IsPhoneNumberRegisteredAsync(registrationData.CellPhoneNumber)).ReturnsAsync(false);
            _peopleServiceMock.Setup(x => x.GetPeopleByPhoneNumberAsync(registrationData.CellPhoneNumber)).ReturnsAsync(new List<Person>());

            // Act
            var result = await _clientService.AddClientAsync(registrationData);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("La persona no esta registrada (número de documento y tipo de documento)", result.Message);
            _peopleServiceMock.Verify(x => x.GetPeopleByPhoneNumberAsync(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task AddClientAsync_ValidData_AddsClientSuccessfully()
        {
            // Arrange
            var registrationData = new ClientRegistrationData("John", "Doe", "61234567", DocumentTypeEnum.IdentityCard, "12345678", "Reason of use", "", "", "", DateTime.UtcNow);
            var person = new Person(registrationData.Name, registrationData.LastName, registrationData.CellPhoneNumber, registrationData.DocumentType, registrationData.DocumentNumber);

            _clientRepositoryMock.Setup(x => x.IsPhoneNumberRegisteredAsync(registrationData.CellPhoneNumber)).ReturnsAsync(false);
            _peopleServiceMock.Setup(x => x.GetPeopleByPhoneNumberAsync(registrationData.CellPhoneNumber)).ReturnsAsync(new List<Person> { person });
            _clientRepositoryMock.Setup(x => x.AddClientAsync(registrationData)).ReturnsAsync(Guid.NewGuid());

            // Act
            var result = await _clientService.AddClientAsync(registrationData);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(string.Empty, result.Message);
            _clientRepositoryMock.Verify(x => x.AddClientAsync(It.IsAny<ClientRegistrationData>()), Times.Once);
        }
    }
}
