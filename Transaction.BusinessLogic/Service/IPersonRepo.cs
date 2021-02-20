using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.Domain.Models;
using Transaction.Domain.Models.Dtos;

namespace Transaction.BusinessLogic.Service
{
    public interface IPersonRepo
    {
        Task<List<Person>> GetAllPersons();
        Task<ResponseMessage<Person>> GetPersonById(Guid id);
        Task<ResponseMessage<Person>> CreatePerson(PersonDto person);
        Task<ResponseMessage<Person>> UpdatePerson(PersonDto person, Guid id);
    }
}
