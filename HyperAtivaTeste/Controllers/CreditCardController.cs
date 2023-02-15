using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HyperAtivaTeste.API.Services.Interfaces;
using HyperAtivaTeste.API.ViewModels;

namespace HyperAtivaTeste.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/creditCard")]
    public class CreditCardController : Controller
    {
        private readonly ICreditCardApiService _creditCardApiService;
        private readonly IAuthorizationApiService _authorizationApiService;
        private readonly ILogApiService _logApiService;

        public CreditCardController(ICreditCardApiService creditCardApiService, IAuthorizationApiService authorizationApiService, ILogApiService logApiService)
        {
            _creditCardApiService = creditCardApiService;
            _authorizationApiService = authorizationApiService;
            _logApiService = logApiService;
        }

        /// <summary>
        /// Insercao de Cartao de Credito pelo usuario
        /// </summary>
        /// <param name="creditCard">Cartao de Credito</param>
        /// <returns>Resultado se o Cartao de Credito foi criado ou ja existe no banco</returns>
        [HttpPost, Authorize]
        [Route("insertByUser/{creditCard}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResultViewModel>> InsertCreditCardByUser([FromRoute] string creditCard)
        {
            var userId = _authorizationApiService.GetUserIdLogged(Request != null ? Request.Headers["authorization"].ToString().Replace("Bearer ", "") : "");
            if (!userId.HasValue)
                return BadRequest("User logged not found");

            if (string.IsNullOrEmpty(creditCard))
                return await ReturnBadRequest<ResultViewModel>(userId.Value, "InsertCreditCardByUser", "Error", $"Credit Card {creditCard} not exists.");

            var result = await _creditCardApiService.InsertCreditCardByUser(new CreditCardInsertByUserViewModel() { CreatedUserId = userId.Value, CreditCard = creditCard });
            if (result == null)
                return await ReturnBadRequest<ResultViewModel>(userId.Value, "InsertCreditCardByUser", "Error", $"Credit Card {creditCard} not inserted.");

            return await ReturnOk<ResultViewModel>(userId.Value, "InsertCreditCardByUser", "Success", $"Credit Card {creditCard} {result.Result}.");
        }

        /// <summary>
        /// Insercao de Cartao de Credito por arquivo
        /// </summary>
        /// <param name="file">Arquivo de Insercao</param>
        /// <returns>Lista de Cartoes de Credito se foi criado ou ja existe no banco</returns>
        [HttpPost, Authorize]
        [Route("insertByFile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<List<ResultViewModel>>> InsertCreditCardByFile([FromForm] FileViewModel file)
        {
            var userId = _authorizationApiService.GetUserIdLogged(Request != null ? Request.Headers["authorization"].ToString().Replace("Bearer ", "") : "");
            if (!userId.HasValue)
                return BadRequest("User logged not found");

            if (file == null)
                return await ReturnBadRequest<List<ResultViewModel>>(userId.Value, "InsertCreditCardByFile", "Error", "DataFile not exists.");

            if (file.File == null)
                return await ReturnBadRequest<List<ResultViewModel>>(userId.Value, "InsertCreditCardByFile", "Error", "File not exists.");

            var result = await _creditCardApiService.InsertCreditCardByFile(_creditCardApiService.ReadCreditCardFile(file.File, userId.Value));
            if (result == null)
                return await ReturnBadRequest<List<ResultViewModel>>(userId.Value, "InsertCreditCardByFile", "Error", "File not inserted.");

            return await ReturnOk<List<ResultViewModel>>(userId.Value, "InsertCreditCardByFile", "Success", "File inserted");
        }

        /// <summary>
        /// Verificacao se Cartao de Credito ja existe no banco
        /// </summary>
        /// <param name="creditCard">Cartao de Credito</param>
        /// <returns>Retorna se Cartao de Credito jah existe ou nao no banco</returns>
        [HttpGet, Authorize]
        [Route("verify/{creditCard}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> VerifyCreditCard([FromRoute] string creditCard)
        {
            var userId = _authorizationApiService.GetUserIdLogged(Request != null ? Request.Headers["authorization"].ToString().Replace("Bearer ", "") : "");
            if (!userId.HasValue)
                return BadRequest("User logged not found");

            if (string.IsNullOrEmpty(creditCard))
                return await ReturnBadRequest<string>(userId.Value, "VerifyCreditCard", "Error", $"Credit Card {creditCard} not exists.");

            var result = await _creditCardApiService.VerifyCreditCard(creditCard);
            if (string.IsNullOrEmpty(result))
                return await ReturnNotFound<string>(userId.Value, "VerifyCreditCard", "Error", $"Credit Card {creditCard} not found.");

            if (result == "not exists")
                return await ReturnNotFound<string>(userId.Value, "VerifyCreditCard", "Error", $"Credit Card {creditCard} not exists.");

            return await ReturnOk<string>(userId.Value, "VerifyCreditCard", "Success", $"Credit Card {creditCard} {result}");
        }

        private async Task<ActionResult<T>> ReturnBadRequest<T>(Guid userId, string action, string result, string message)
        {
            await _logApiService.InsertLog(new LogViewModel() { UserId = userId, Action = action, Result = $"{result} - {message}" });
            return BadRequest(message);
        }

        private async Task<ActionResult<T>> ReturnNotFound<T>(Guid userId, string action, string result, string message)
        {
            await _logApiService.InsertLog(new LogViewModel() { UserId = userId, Action = action, Result = $"{result} - {message}" });
            return NotFound(message);
        }

        private async Task<ActionResult<T>> ReturnOk<T>(Guid userId, string action, string result, string message)
        {
            await _logApiService.InsertLog(new LogViewModel() { UserId = userId, Action = action, Result = $"{result} - {message}" });
            return Ok(message);
        }
    }
}