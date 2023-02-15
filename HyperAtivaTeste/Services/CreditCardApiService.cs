using AutoMapper;
using HyperAtivaTeste.API.Services.Interfaces;
using HyperAtivaTeste.API.ViewModels;
using HyperAtivaTeste.Domains.Interfaces.Services;
using HyperAtivaTeste.Domains.Models;

namespace HyperAtivaTeste.API.Services
{
    public class CreditCardApiService : ICreditCardApiService
    {
        private readonly IMapper _mapper;
        private readonly ICreditCardService _creditCardService;

        public CreditCardApiService(IMapper mapper, ICreditCardService creditCardService)
        {
            _mapper = mapper;
            _creditCardService = creditCardService;
        }

        public async Task<ResultViewModel> InsertCreditCardByUser(CreditCardInsertByUserViewModel insertView)
            => _mapper.Map<ResultViewModel>(await _creditCardService.InsertCreditCardByUser(_mapper.Map<CreditCardInsertByUserModel>(insertView)));

        public CreditCardInsertByFileViewModel ReadCreditCardFile(IFormFile file, Guid createdUserId)
            => _mapper.Map<CreditCardInsertByFileViewModel>(_creditCardService.ReadCreditCardFile(file, createdUserId));

        public async Task<List<ResultViewModel>> InsertCreditCardByFile(CreditCardInsertByFileViewModel insertView)
            => _mapper.Map<List<ResultViewModel>>(await _creditCardService.InsertCreditCardByFile(_mapper.Map<CreditCardInsertByFileModel>(insertView)));

        public async Task<string> VerifyCreditCard(string creditCard)
            => await _creditCardService.VerifyCreditCard(creditCard);
    }
}
