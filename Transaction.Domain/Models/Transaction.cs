using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Transaction.Domain.Models
{
    public class Transaction
    {
        public Guid ID { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        [ForeignKey("AccountID")]
        public Guid DrAccountID { get; set; }
        [ForeignKey("AccountID")]
        public Guid CrAccountID { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public enum TransactionType
    {
        CR = 1,
        DR = 2
    }
}
