using HyperAtivaTeste.API.ViewModels;

namespace HyperAtivaTeste.API.Services.Interfaces
{
    public interface ICreditCardApiService
    {
        Task<ResultViewModel> InsertCreditCardByUser(CreditCardInsertByUserViewModel insertView);
        CreditCardInsertByFileViewModel ReadCreditCardFile(IFormFile file, Guid createdUserId);
        Task<List<ResultViewModel>> InsertCreditCardByFile(CreditCardInsertByFileViewModel insertView);
        Task<string> VerifyCreditCard(string creditCard);
    }
}
