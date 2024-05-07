using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp2.Shared.Models
{
    public class ApiResponse<T>
    {
        public bool success;
        public string errorMessage;
        public T data;
        public ApiResponse()
        {
        }

        public ApiResponse(bool success)
        {
            this.success = success;
        }
    }

}
