using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Transaction.Domain.Models.Dtos
{
    public class TransactionDto
    {    
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public Guid DrAccountID { get; set; }
        public Guid CrAccountID { get; set; }
    }
}
