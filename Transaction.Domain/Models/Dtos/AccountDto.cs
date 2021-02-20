using System;
using System.Collections.Generic;
using System.Text;

namespace Transaction.Domain.Models.Dtos
{
    public class AccountDto
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public Guid PersonID { get; set; }
    }
}
