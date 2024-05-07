using BlazorApp2.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlazorApp2.Server.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        protected bool IsValidParams()
        {
            return ModelState.IsValid;
        }

        protected IActionResult OnMissingParameters()
        {
            var errorMessage = ModelState.SelectMany(state => state.Value.Errors).Aggregate("", (current, error) => current + (error.ErrorMessage + ". "));
            return HandleResponse(errorMessage: errorMessage, statusCode: HttpStatusCode.BadRequest);
        }

        protected IActionResult HandleResponse<T>(T result = default, string errorMessage = "", bool success = true, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            ApiResponse<T> responseObject = new(success);
            try
            {
                if (success)
                    responseObject.data = result;
                else
                    responseObject.errorMessage = errorMessage;
            }
            catch (Exception ex)
            {
                responseObject.success = false;
                responseObject.errorMessage = ex.Message;
                responseObject.data = default;
            }

            return CreateApiResponse(responseObject, statusCode);
        }

        protected IActionResult HandleResponse(string errorMessage, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return HandleResponse<object>(success: false, errorMessage: errorMessage, statusCode: statusCode);
        }

        protected IActionResult HandleException(Exception exception)
        {
            return HandleResponse(errorMessage: exception.Message.ToString());
        }

        protected IActionResult CreateApiResponse<T>(ApiResponse<T> generalResponse, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return StatusCode((int)statusCode, generalResponse);
            /*
            if (statusCode == HttpStatusCode.BadRequest)
                return BadRequest(generalResponse);

            if (statusCode == HttpStatusCode.NotFound)
                return NotFound(generalResponse);

            if (statusCode == HttpStatusCode.InternalServerError)
                return StatusCode(((int)statusCode), generalResponse);

            return Ok(generalResponse);*/
        }
    }
}
