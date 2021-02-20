using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Transaction.Domain.Models
{
    public class Account
    {
        public Guid ID { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        [ForeignKey("PersonID")]
        public Guid PersonID { get; set; }
        public virtual Person Person { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
