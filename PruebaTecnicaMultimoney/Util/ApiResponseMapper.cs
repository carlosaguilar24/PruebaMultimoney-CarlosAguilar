using Microsoft.AspNetCore.Mvc;

namespace PruebaTecnicaMultimoney.Util
{
    public class ApiResponseMapper
    {
        public static IActionResult MapResult<T>(ControllerBase controller, T result, int returnValue)
        {
            var operationResult = (OperationResult)returnValue;

            return operationResult switch
            {
                OperationResult.Success => controller.Ok(result),

                OperationResult.InsufficientBalance =>
                    controller.BadRequest(new { message = "Insufficient balance" }),

                OperationResult.InvalidAmount =>
                    controller.BadRequest(new { message = "Amount must be greater than 0" }),

                OperationResult.AccountNotFound =>
                    controller.NotFound(new { message = "Account not found" }),

                OperationResult.UnexpectedError =>
                    controller.StatusCode(500, new { message = "An unexpected error occurred in database" }),

                OperationResult.DestinationAccountNotFound =>
                controller.NotFound(new { message = "Destination Account not found" }),

                _ => controller.StatusCode(500)
            };
        }
    }
}
