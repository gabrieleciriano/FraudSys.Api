using FraudSys.Domain.Commands.v1.CreateAccountLimit;
using FraudSys.Domain.Entities.v1;
using FraudSys.Domain.Interfaces.v1.Repositories;
using Moq;

namespace FraudSys.Tests.Unit.Domain.Commands.v1.CreateAccountLimit
{
    public class CreateAccountLimitCommandHandlerTest
    {
        private readonly Mock<IAccountLimitRepository> _accountLimitRepositoryMock;
        private readonly CreateAccountLimitCommandHandler _handler;

        public CreateAccountLimitCommandHandlerTest()
        {
            _accountLimitRepositoryMock = new Mock<IAccountLimitRepository>();
            _handler = new CreateAccountLimitCommandHandler(_accountLimitRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldCreateAccountLimit()
        {
            // Arrange
            var command = new CreateAccountLimitCommand
            {
                CPF = "12345678901",
                AgencyNumber = "0001",
                AccountNumber = "12345",
                PixLimit = 2000.00
            };

            _accountLimitRepositoryMock.Setup(repo => repo.GetAccountLimitAsync(command.CPF, command.AgencyNumber))
                .ReturnsAsync((AccountLimit)null);

            _accountLimitRepositoryMock.Setup(repo => repo.SaveAsync(It.IsAny<AccountLimit>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            _accountLimitRepositoryMock.Verify(repo => repo.SaveAsync(It.IsAny<AccountLimit>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ExistingAccountLimit_ShouldThrowArgumentException()
        {
            // Arrange
            var command = new CreateAccountLimitCommand
            {
                CPF = "12345678901",
                AgencyNumber = "0001",
                AccountNumber = "12345",
                PixLimit = 2000.00
            };

            var existingAccountLimit = new AccountLimit(command.CPF, command.AgencyNumber, command.AccountNumber, 1000.00);

            _accountLimitRepositoryMock.Setup(repo => repo.GetAccountLimitAsync(command.CPF, command.AgencyNumber))
                .ReturnsAsync(existingAccountLimit);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Contains("The limit for this account has already been created.", exception.Message);
        }
    }
}