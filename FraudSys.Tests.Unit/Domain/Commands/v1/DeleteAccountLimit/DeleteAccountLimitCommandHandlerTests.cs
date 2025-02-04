using FraudSys.Domain.Commands.v1.DeleteAccountLimit;
using FraudSys.Domain.Interfaces.v1.Repositories;
using Moq;

namespace FraudSys.Tests.Unit.Domain.Commands.v1.DeleteAccountLimit
{
    public class DeleteAccountLimitCommandHandlerTests
    {
        private readonly Mock<IAccountLimitRepository> _accountLimitRepositoryMock;
        private readonly DeleteAccountLimitCommandHandler _handler;

        public DeleteAccountLimitCommandHandlerTests()
        {
            _accountLimitRepositoryMock = new Mock<IAccountLimitRepository>();
            _handler = new DeleteAccountLimitCommandHandler(_accountLimitRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldDeleteAccountLimit()
        {
            // Arrange
            var command = new DeleteAccountLimitCommand
            {
                CPF = "12345678901",
                AgencyNumber = "0001",
                AccountNumber = "12345"
            };

            _accountLimitRepositoryMock.Setup(repo => repo.DeleteAccountLimitAsync(command.CPF, command.AgencyNumber, command.AccountNumber))
                .Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _accountLimitRepositoryMock.Verify(repo => repo.DeleteAccountLimitAsync(command.CPF, command.AgencyNumber, command.AccountNumber), Times.Once);
        }

        [Fact]
        public async Task Handle_RepositoryThrowsException_ShouldThrowException()
        {
            // Arrange
            var command = new DeleteAccountLimitCommand
            {
                CPF = "12345678901",
                AgencyNumber = "0001",
                AccountNumber = "12345"
            };

            _accountLimitRepositoryMock.Setup(repo => repo.DeleteAccountLimitAsync(command.CPF, command.AgencyNumber, command.AccountNumber))
                .ThrowsAsync(new Exception("Repository error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Contains("Repository error", exception.Message);
        }
    }
}