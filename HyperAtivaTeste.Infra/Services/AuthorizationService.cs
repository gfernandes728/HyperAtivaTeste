using HyperAtivaTeste.Domains.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;

namespace HyperAtivaTeste.Infra.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        public AuthorizationService() { }

        public Guid? GetUserIdLogged(string obj)
        {
            if (string.IsNullOrEmpty(obj))
                return null;

            var token = ((new JwtSecurityTokenHandler()).ReadToken(obj)) as JwtSecurityToken;
            foreach (var item in token.Payload)
            {
                if (item.Key.Contains("userId"))
                    return Guid.Parse(item.Value.ToString());
            }

            return null;
        }
    }
}
