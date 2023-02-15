using AutoBogus;
using HyperAtivaTeste.Domains.Models;
using Xunit;

namespace HyperAtivaTeste.Tests.UnitTests.HyperAtivaTeste.Domains.Models
{
    public class ModelsTest
    {
        private readonly IAutoFaker _autoFaker;

        public ModelsTest()
            => _autoFaker = AutoFaker.Create();

        [Fact]
        public void CreditCardFileModel_Test()
        {
            var model = _autoFaker.Generate<CreditCardFileModel>();
            Assert.IsType<CreditCardFileModel>(model);
            Assert.IsType<string>(model.Identify);
            Assert.IsType<string>(model.Number);
            Assert.IsType<string>(model.CreditCard);
        }

        [Fact]
        public void CreditCardInsertByFileModel_Test()
        {
            var model = _autoFaker.Generate<CreditCardInsertByFileModel>();
            model.CreditCards = _autoFaker.Generate<CreditCardFileModel>(1);

            Assert.IsType<CreditCardInsertByFileModel>(model);
            Assert.IsType<Guid>(model.CreatedUserId);
            Assert.IsType<string>(model.Name);
            Assert.IsType<DateTime>(model.DateFile);
            Assert.IsType<string>(model.Lote);
            Assert.IsType<int>(model.TotalRegisters);
            Assert.IsType<List<CreditCardFileModel>>(model.CreditCards);
        }

        [Fact]
        public void CreditCardInsertByUserModel_Test()
        {
            var model = _autoFaker.Generate<CreditCardInsertByUserModel>();
            Assert.IsType<CreditCardInsertByUserModel>(model);
            Assert.IsType<Guid>(model.CreatedUserId);
            Assert.IsType<string>(model.CreditCard);
        }

        [Fact]
        public void DefaultModel_Test()
        {
            var model = _autoFaker.Generate<DefaultModel>();
            Assert.IsType<DefaultModel>(model);
            Assert.IsType<Guid>(model.Id);
        }

        [Fact]
        public void LogModel_Test()
        {
            var model = _autoFaker.Generate<LogModel>();
            Assert.IsType<LogModel>(model);
            Assert.IsType<Guid>(model.UserId);
            Assert.IsType<string>(model.Action);
            Assert.IsType<string>(model.Result);
        }

        [Fact]
        public void ResultModel_Test()
        {
            var model = _autoFaker.Generate<ResultModel>();
            Assert.IsType<ResultModel>(model);
            Assert.IsType<Guid>(model.Id);
            Assert.IsType<string>(model.Result);
        }

        [Fact]
        public void UserModel_Test()
        {
            var model = _autoFaker.Generate<UserModel>();
            Assert.IsType<UserModel>(model);
            Assert.IsType<Guid>(model.Id);
            Assert.IsType<string>(model.Email);
        }
    }
}
