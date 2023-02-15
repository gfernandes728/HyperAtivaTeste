using HyperAtivaTeste.API.Services.Interfaces;
using HyperAtivaTeste.Domains.Interfaces.Services;

namespace HyperAtivaTeste.API.Services
{
    public class UserApiService : IUserApiService
    {
        private readonly IUserService _userService;

        public UserApiService(IUserService userService)
            => _userService = userService;

        public async Task<string> GetTokenByEmail(string email)
            => await _userService.GetTokenByEmail(email);
    }
}
