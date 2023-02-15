using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using HyperAtivaTeste.Domains.Interfaces.Repository;
using HyperAtivaTeste.Domains.Interfaces.Services;
using HyperAtivaTeste.Domains.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HyperAtivaTeste.Infra.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<string> GetTokenByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            return user == null ? "" : GenerateToken(user);
        }

        private string GenerateToken(UserModel user)
        {
            var logged = new List<Claim>();
            logged.Add(new Claim("userId", user.Id.ToString()));
            logged.Add(new Claim("email", user.Email));

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: "HyperAtivaTeste",
                audience: "http://localhost",
                claims: logged,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthJwt:Key"])), SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
