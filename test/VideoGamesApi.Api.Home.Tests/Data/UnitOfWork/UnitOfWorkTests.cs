using Moq;
using VideoGamesApi.Api.Home.Data;
using VideoGamesApi.Api.Home.Data.Contracts;
using VideoGamesApi.Api.Home.Tests.Data.Fakes;
using Xunit;

namespace VideoGamesApi.Api.Home.Tests.Data.UnitOfWork
{
    public class UnitOfWorkTests
    {
        private IUnitOfWork _unitOfWork;
        private readonly Mock<Context> _mockContext = new();

        [Fact]
        public void GetOneRepository_SeveralTimes_ReturnsOneRepository()
        {
            //Arrange
            _unitOfWork = new Home.Data.UnitOfWork(_mockContext.Object);

            //Act
            var firstRepository = _unitOfWork.GetRepository<FakeEntity<int>, int>();

            var secondRepository = _unitOfWork.GetRepository<FakeEntity<int>, int>();

            //Assert
            Assert.Equal(firstRepository, secondRepository);
        }
    }
}
