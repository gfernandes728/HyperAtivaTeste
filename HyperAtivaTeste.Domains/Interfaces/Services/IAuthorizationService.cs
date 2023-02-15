namespace HyperAtivaTeste.Domains.Interfaces.Services
{
    public interface IAuthorizationService
    {
        Guid? GetUserIdLogged(string obj);
    }
}
