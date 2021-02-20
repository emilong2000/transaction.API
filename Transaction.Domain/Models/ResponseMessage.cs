using System;
using System.Collections.Generic;
using System.Text;

namespace Transaction.Domain.Models
{
    public class ResponseMessage<T> where T : class
    {
        public string Message { get; set; }
        public string Statuscode { get; set; }
        public bool IsSuccessful { get; set; }
        public T Data {get;set;}
    }
}
