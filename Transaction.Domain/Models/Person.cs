using System;
using System.Collections.Generic;
using System.Text;

namespace Transaction.Domain.Models
{
    public class Person
    {
        public Guid ID { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
