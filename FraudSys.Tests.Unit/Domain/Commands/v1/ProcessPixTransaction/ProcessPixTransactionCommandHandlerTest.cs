using FraudSys.Domain.Commands.v1.ProcessPixTransaction;
using FraudSys.Domain.Interfaces.v1.Repositories;
using Moq;

namespace FraudSys.Tests.Unit.Domain.Commands.v1.ProcessPixTransaction
{
    public class ProcessPixTransactionCommandHandlerTest
    {
        private readonly Mock<IAccountLimitRepository> _accountLimitRepositoryMock;
        private readonly ProcessPixTransactionCommandHandler _handler;

        public ProcessPixTransactionCommandHandlerTest()
        {
            _accountLimitRepositoryMock = new Mock<IAccountLimitRepository>();
            _handler = new ProcessPixTransactionCommandHandler(_accountLimitRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_TransactionWithinLimit_ShouldReturnTrue()
        {
            // Arrange
            var command = new ProcessPixTransactionCommand
            {
                CPF = "12345678901",
                AgencyNumber = "0001",
                AccountNumber = "12345",
                TransactionAmount = 500.00
            };

            _accountLimitRepositoryMock
                .Setup(repo => repo.ProcessPixTransactionAsync(command.CPF, command.AgencyNumber, command.AccountNumber, command.TransactionAmount))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            _accountLimitRepositoryMock.Verify(repo => repo.ProcessPixTransactionAsync(command.CPF, command.AgencyNumber, command.AccountNumber, command.TransactionAmount), Times.Once);
        }

        [Fact]
        public async Task Handle_TransactionExceedsLimit_ShouldReturnFalse()
        {
            // Arrange
            var command = new ProcessPixTransactionCommand
            {
                CPF = "12345678901",
                AgencyNumber = "0001",
                AccountNumber = "12345",
                TransactionAmount = 5000.00
            };

            _accountLimitRepositoryMock
                .Setup(repo => repo.ProcessPixTransactionAsync(command.CPF, command.AgencyNumber, command.AccountNumber, command.TransactionAmount))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
            _accountLimitRepositoryMock.Verify(repo => repo.ProcessPixTransactionAsync(command.CPF, command.AgencyNumber, command.AccountNumber, command.TransactionAmount), Times.Once);
        }

        [Fact]
        public async Task Handle_RepositoryThrowsException_ShouldThrowException()
        {
            // Arrange
            var command = new ProcessPixTransactionCommand
            {
                CPF = "12345678901",
                AgencyNumber = "0001",
                AccountNumber = "12345",
                TransactionAmount = 1000.00
            };

            _accountLimitRepositoryMock
                .Setup(repo => repo.ProcessPixTransactionAsync(command.CPF, command.AgencyNumber, command.AccountNumber, command.TransactionAmount))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Contains("Database error", exception.Message);
        }
    }
}