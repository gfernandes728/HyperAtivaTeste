using AutoBogus;
using Moq;
using HyperAtivaTeste.Infra;
using HyperAtivaTeste.Infra.Repository;
using Xunit;
using HyperAtivaTeste.Domains.Models;
using System.Data.Common;
using Moq.Dapper;
using Dapper;

namespace HyperAtivaTeste.Tests.UnitTests.HyperAtivaTeste.Infra.Repository
{
    public class UserRepositoryTest
    {
        private readonly IAutoFaker _autoFaker;
        private readonly Mock<DbDapperContext> _dapperMock;
        private readonly UserRepository _userRepository;

        public UserRepositoryTest()
        {
            _autoFaker = AutoFaker.Create();
            _dapperMock = new Mock<DbDapperContext>(new Mock<IConfiguration>().Object);
            _userRepository = new UserRepository(_dapperMock.Object);
        }

        [Fact]
        public async Task GetUserByEmail_Test()
        {
            var dbConnection = new Mock<DbConnection>();
            dbConnection.SetupDapperAsync(x => x.QueryFirstOrDefaultAsync<UserModel>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(_autoFaker.Generate<UserModel>());

            _dapperMock.Setup(x => x.DapperConnection).Returns(dbConnection.Object);
            Assert.IsType<UserModel>(await _userRepository.GetUserByEmail(_autoFaker.Generate<string>()));
        }

    }
}
