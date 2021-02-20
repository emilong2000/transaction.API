using System;
using System.Collections.Generic;
using System.Text;

namespace Transaction.Domain.Models
{
    public class ErrorResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
