using ChronoSekai.Shared.Domain.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChronoSekai.Shared.API
{
    public abstract class BaseController : Controller
    {
        public virtual IActionResult SwitchResult(Error error)
        {
            if (error is null)
                return Ok();

            Func<IActionResult> func = error.ErrorCode switch
            {
                ErrorCode.InvalidRequest => () => StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Title = "Ошибка сервера!",
                    error.Message
                }),

                ErrorCode.Validation => () => BadRequest(new
                {
                    Title = "Ошибка валидации!",
                    error.Message
                }),

                ErrorCode.Conflict => () => Conflict(new
                {
                    Title = "Конфликт данных!",
                    error.Message
                }),

                ErrorCode.ServerError => () => StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Title = "Ошибка сервера!",
                    error.Message
                }),

                _ => () => StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Title = "Неизвестная ошибка!",
                    Message = "Произошла непредвиденная ошибка!"
                }),
            };

            return func.Invoke();
        }
    }
}
