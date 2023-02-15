namespace HyperAtivaTeste.API.Services.Interfaces
{
    public interface IUserApiService
    {
        Task<string> GetTokenByEmail(string email);
    }
}
