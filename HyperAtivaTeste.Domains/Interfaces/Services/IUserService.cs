namespace HyperAtivaTeste.Domains.Interfaces.Services
{
    public interface IUserService
    {
        Task<string> GetTokenByEmail(string email);
    }
}
