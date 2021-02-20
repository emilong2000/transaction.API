using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.Domain.Models;
using Transaction.Domain.Models.Dtos;

namespace Transaction.BusinessLogic.Service
{
    public interface ITransactionRepo
    {
        Task<List<Domain.Models.Transaction>> GetAllTransactions();
        Task<ResponseMessage<Domain.Models.Transaction>> GetTransactionById(Guid id);
        Task<ResponseMessage<Domain.Models.Transaction>> CreateTransaction(TransactionDto transaction);
        Task<List<TransactionViewModelDto>> GetTransactionByPersonId(Guid id);
        
    }
}
