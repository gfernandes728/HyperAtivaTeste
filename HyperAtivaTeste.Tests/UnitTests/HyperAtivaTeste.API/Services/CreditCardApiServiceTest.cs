using AutoBogus;
using AutoMapper;
using Moq;
using HyperAtivaTeste.API.AutoMapper;
using HyperAtivaTeste.API.Services;
using HyperAtivaTeste.API.ViewModels;
using HyperAtivaTeste.Domains.Interfaces.Services;
using HyperAtivaTeste.Domains.Models;
using Xunit;

namespace HyperAtivaTeste.Tests.UnitTests.HyperAtivaTeste.API.Services
{
    public class CreditCardApiServiceTest
    {
        private readonly Mapper _mapper;
        private readonly IAutoFaker _autoFaker;
        private readonly Mock<ICreditCardService> _creditCardService;

        public CreditCardApiServiceTest()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfiles())));
            _autoFaker = AutoFaker.Create();
            _creditCardService = new Mock<ICreditCardService>();
        }

        [Fact]
        public async Task InsertCreditCardByUser_Test()
        {
            _creditCardService.Setup(x => x.InsertCreditCardByUser(It.IsAny<CreditCardInsertByUserModel>())).ReturnsAsync(_autoFaker.Generate<ResultModel>());
            Assert.IsType<ResultViewModel>(await new CreditCardApiService(_mapper, _creditCardService.Object).InsertCreditCardByUser(_autoFaker.Generate<CreditCardInsertByUserViewModel>()));
        }

        [Fact]
        public void ReadCreditCardFile_Test()
        {
            _creditCardService.Setup(x => x.ReadCreditCardFile(It.IsAny<IFormFile>(), It.IsAny<Guid>())).Returns(_autoFaker.Generate<CreditCardInsertByFileModel>());
            Assert.IsType<CreditCardInsertByFileViewModel>(new CreditCardApiService(_mapper, _creditCardService.Object).ReadCreditCardFile(_autoFaker.Generate<IFormFile>(), _autoFaker.Generate<Guid>()));
        }

        [Fact]
        public async Task InsertCreditCardByFile_Test()
        {
            _creditCardService.Setup(x => x.InsertCreditCardByFile(It.IsAny<CreditCardInsertByFileModel>())).ReturnsAsync(_autoFaker.Generate<ResultModel>(1));
            Assert.IsType<List<ResultViewModel>>(await new CreditCardApiService(_mapper, _creditCardService.Object).InsertCreditCardByFile(_autoFaker.Generate<CreditCardInsertByFileViewModel>()));
        }

        [Fact]
        public async Task VerifyCreditCard_Test()
        {
            _creditCardService.Setup(x => x.VerifyCreditCard(It.IsAny<string>())).ReturnsAsync(_autoFaker.Generate<string>());
            Assert.IsType<string>(await new CreditCardApiService(null, _creditCardService.Object).VerifyCreditCard(_autoFaker.Generate<string>()));
        }
    }
}
