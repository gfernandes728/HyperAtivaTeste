using HyperAtivaTeste.API.Services.Interfaces;
using HyperAtivaTeste.Domains.Interfaces.Services;

namespace HyperAtivaTeste.API.Services
{
    public class AuthorizationApiService : IAuthorizationApiService
    {
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationApiService(IAuthorizationService authorizationService)
            => _authorizationService = authorizationService;

        public Guid? GetUserIdLogged(string obj)
            => _authorizationService.GetUserIdLogged(obj);
    }
}
