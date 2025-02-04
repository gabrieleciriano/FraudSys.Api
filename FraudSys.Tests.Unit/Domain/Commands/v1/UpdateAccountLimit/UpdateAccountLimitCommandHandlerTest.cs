using FraudSys.Domain.Commands.v1.UpdateAccountLimit;
using FraudSys.Domain.Interfaces.v1.Repositories;
using Moq;

namespace FraudSys.Tests.Unit.Domain.Commands.v1.UpdateAccountLimit
{
    public class UpdateAccountLimitCommandHandlerTest
    {
        private readonly Mock<IAccountLimitRepository> _mockAccountLimitRepository;
        private readonly UpdateAccountLimitCommandHandler _handler;

        public UpdateAccountLimitCommandHandlerTest()
        {
            _mockAccountLimitRepository = new Mock<IAccountLimitRepository>();
            _handler = new UpdateAccountLimitCommandHandler(_mockAccountLimitRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldCallUpdateAccountLimitAsync_WhenCalledWithValidRequest()
        {
            // Arrange
            var command = new UpdateAccountLimitCommand
            {
                CPF = "12345678901",
                AgencyNumber = "1234",
                NewLimit = 5000
            };

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockAccountLimitRepository
                .Verify(repo => repo.UpdateAccountLimitAsync(command.CPF, command.AgencyNumber, command.NewLimit), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenRepositoryThrowsException()
        {
            // Arrange
            var command = new UpdateAccountLimitCommand
            {
                CPF = "12345678901",
                AgencyNumber = "1234",
                NewLimit = 5000
            };

            // Setup para lançar uma exceção quando o repositório for chamado
            _mockAccountLimitRepository
                .Setup(repo => repo.UpdateAccountLimitAsync(command.CPF, command.AgencyNumber, command.NewLimit))
                .ThrowsAsync(new Exception("Test Exception"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));

            Assert.StartsWith("Error Message: System.Exception:", exception.Message);
            Assert.Contains("Test Exception", exception.Message);
        }
    }
}