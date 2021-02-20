using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.Domain.Models;
using Transaction.Domain.Models.Dtos;

namespace Transaction.BusinessLogic.Service
{
    public interface IAccountRepo
    {
        Task<List<Account>> GetAllAccount();
        Task<ResponseMessage<Account>> GetAccountById(Guid id);
        Task<ResponseMessage<Account>> GetAccountByPersonId(Guid personId);
        Task<ResponseMessage<Account>> CreateAccount(AccountDto account);
        Task<ResponseMessage<Account>> UpdateAccount(AccountDto account, Guid id);
        
    }
}
