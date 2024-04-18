using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;

namespace API.Errors
{
    public class ApiException : ApiResponse
    {
        public ApiException(int statusCode, string errorMessage = null, string details = null) 
        : base(statusCode, errorMessage)
        {

        }

        public string details { get; set; }
    }
}