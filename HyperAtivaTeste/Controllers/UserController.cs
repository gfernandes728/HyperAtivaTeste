using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HyperAtivaTeste.API.Services.Interfaces;

namespace HyperAtivaTeste.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/user")]
    public class UserController : Controller
    {
        private readonly IUserApiService _userApiService;

        public UserController(IUserApiService userApiService)
            => _userApiService = userApiService;

        /// <summary>
        /// Geracao de Token pelo e-mail cadastrado no banco
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Token para autenticacao</returns>
        [HttpGet]
        [Route("token/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<string>> GetTokenByEmail([FromRoute] string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest("Email not inserted.");

            var token = await _userApiService.GetTokenByEmail(email);
            if (string.IsNullOrEmpty(token))
                return NotFound("Token can not generated.");

            return Ok(token);
        }

    }
}
