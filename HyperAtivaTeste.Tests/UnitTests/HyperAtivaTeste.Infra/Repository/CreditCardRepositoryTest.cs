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
    public class CreditCardRepositoryTest
    {
        private readonly IAutoFaker _autoFaker;
        private readonly Mock<DbDapperContext> _dapperMock;
        private readonly CreditCardRepository _creditCardRepository;

        public CreditCardRepositoryTest()
        {
            _autoFaker = AutoFaker.Create();
            _dapperMock = new Mock<DbDapperContext>(new Mock<IConfiguration>().Object);
            _creditCardRepository = new CreditCardRepository(_dapperMock.Object);
        }

        [Fact]
        public async Task InsertCreditCardByUser_Test()
        {
            SetDapperMock<ResultModel>();
            Assert.IsType<ResultModel>(await _creditCardRepository.InsertCreditCardByUser(_autoFaker.Generate<CreditCardInsertByUserModel>()));
        }

        [Fact]
        public async Task InsertFile_Test()
        {
            SetDapperMock<Guid>();
            Assert.IsType<Guid>(await _creditCardRepository.InsertFile(_autoFaker.Generate<CreditCardInsertByFileModel>()));
        }

        [Fact]
        public async Task InsertCreditCardByFile_Test()
        {
            SetDapperMock<ResultModel>();
            Assert.IsType<List<ResultModel>>(await _creditCardRepository.InsertCreditCardByFile(_autoFaker.Generate<CreditCardInsertByFileModel>(), _autoFaker.Generate<Guid>()));
        }

        [Fact]
        public async Task VerifyCreditCard_Test()
        {
            SetDapperMock<ResultModel>();
            Assert.IsType<string>(await _creditCardRepository.VerifyCreditCard(_autoFaker.Generate<string>()));
        }

        private void SetDapperMock<T>()
        {
            var dbConnection = new Mock<DbConnection>();
            dbConnection.SetupDapperAsync(x => x.QueryFirstOrDefaultAsync<T>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(_autoFaker.Generate<T>());
            _dapperMock.Setup(x => x.DapperConnection).Returns(dbConnection.Object);
        }
    }
}
