using AsoftyBackend.Infrastructure.Data.Model;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsoftyBackend.Infrastructure.Utils
{
    public static class BuildDto
    {
        public static ResponseDto<T> ReturnData<T>(T data)
        {
            return new ResponseDto<T>
            {
                Data = data,
                IsSuccess = true,
                Message = "",
                Errors = null
            };
        }

        public static ResponseDto<object> ReturnValidationError(IEnumerable<ValidationFailure> errors)
        {
            return new ResponseDto<object>
            {
                Data = null,
                IsSuccess = false,
                Message = errors.First()?.ErrorMessage ?? "Validation Error",
                Errors = errors
            };
        }

        public static ResponseDto<object> ReturnInternalError(Exception ex)
        {
            return new ResponseDto<object>
            {
                Data = null,
                IsSuccess = false,
                Message = ex.Message,
                Errors = { }
            };
        }
    }
}
