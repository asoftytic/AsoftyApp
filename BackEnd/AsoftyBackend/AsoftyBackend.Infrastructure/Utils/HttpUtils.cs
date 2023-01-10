using Microsoft.AspNetCore.Mvc;

namespace AsoftyBackend.Infrastructure.Utils
{
    public static class HttpUtils
    {
        public static OkObjectResult ReturnData<T>(T data)
        {
            return new OkObjectResult(BuildDto.ReturnData(data));
        }

        public static ConflictObjectResult ReturnValidationError<T>(T data)
        {
            return new ConflictObjectResult(BuildDto.ReturnData(data));
        }

        public static IActionResult ReturnInternalServerError(string message)
        {
            return new ObjectResult(message) { StatusCode = 500 };
        }
    }
}
