using System;
using System.Collections.Generic;
using System.Text;

namespace Transaction.Domain.Models.Dtos
{
    public class TransactionViewModelDto
    {
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string OffsetAccount { get; set; }
        public string TransactionType { get; set; }
        public decimal Balance { get; set; }
        public string TransactionDateTime { get; set; }
    }
}
