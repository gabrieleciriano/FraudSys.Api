using FraudSys.Domain.Entities.v1;
using FraudSys.Domain.Interfaces.v1.Repositories;
using FraudSys.Domain.Queries.v1.GetAccountLimit;
using Moq;

namespace FraudSys.Tests.Unit.Domain.Queries.v1.GetAccountLimit
{
    public class GetAccountLimitQueryHandlerTest
    {
        private readonly Mock<IAccountLimitRepository> _mockAccountLimitRepository;
        private readonly GetAccountLimitQueryHandler _handler;

        public GetAccountLimitQueryHandlerTest()
        {
            _mockAccountLimitRepository = new Mock<IAccountLimitRepository>();
            _handler = new GetAccountLimitQueryHandler(_mockAccountLimitRepository.Object);
        }

      
        [Fact]
        public async Task Handle_ShouldReturnAccountLimit_WhenAccountLimitIsFound()
        {
            // Arrange
            var request = new GetAccountLimitQuery("12345678901", "1234"); 
            var expectedAccountLimit = new AccountLimit(); 

            _mockAccountLimitRepository
                .Setup(repo => repo.GetAccountLimitAsync(request.CPF, request.AgencyNumber))
                .ReturnsAsync(expectedAccountLimit);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Handle_ShouldThrowKeyNotFoundException_WhenAccountLimitIsNotFound()
        {
            // Arrange
            var request = new GetAccountLimitQuery("12345678901", "1234");

            _mockAccountLimitRepository
                .Setup(repo => repo.GetAccountLimitAsync(request.CPF, request.AgencyNumber))
                .ReturnsAsync((AccountLimit)null); // Nenhum limite encontrado

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(request, CancellationToken.None));
            Assert.Contains("The limit was not found for this account.", exception.Message);
        }


        [Fact]
        public async Task Handle_ShouldThrowException_WhenAnErrorOccurs()
        {
            // Arrange
            var request = new GetAccountLimitQuery("12345678901", "1234");

            _mockAccountLimitRepository
                .Setup(repo => repo.GetAccountLimitAsync(request.CPF, request.AgencyNumber))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(request, CancellationToken.None));
            Assert.Contains("Error Message:", exception.Message);
        }
    }
}