using HyperAtivaTeste.Domains.Models;

namespace HyperAtivaTeste.Domains.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<UserModel> GetUserByEmail(string email);
    }
}
