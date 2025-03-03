using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HyperAtivaTeste.API.Services.Interfaces;

namespace HyperAtivaTeste.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/linkedin")]
    public class LinkedInController : Controller
    {
        private readonly ILinkedInCSharpApiService _cSharpService;

        public LinkedInController(ILinkedInCSharpApiService cSharpService)
            => _cSharpService = cSharpService;

        /// <summary>
        /// C# Teste - Questao 16
        /// In what order would the employee names in this example be printed to the console?
        /// </summary>
        /// <returns>Lista de string ordenada</returns>
        [HttpGet]
        [Route("csharp/question16")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<List<string>> GetQuestion16()
            => Ok(_cSharpService.Question16());
    }
}
