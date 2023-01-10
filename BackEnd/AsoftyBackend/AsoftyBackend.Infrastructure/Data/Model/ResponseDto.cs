using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsoftyBackend.Infrastructure.Data.Model
{
    [Serializable]
    public class ResponseDto<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }

        public string? Message { get; set; }

        public IEnumerable<ValidationFailure>? Errors { get; set; }

    }
}
