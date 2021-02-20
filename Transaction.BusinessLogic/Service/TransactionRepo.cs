using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NLog;
using Transaction.DataAccess;
using Transaction.Domain.Models;
using Transaction.Domain.Models.Dtos;

namespace Transaction.BusinessLogic.Service
{
    
    public class TransactionRepo : ITransactionRepo
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly TContext _context;
        private ResponseMessage<Domain.Models.Transaction> _response;
        public TransactionRepo(TContext context)
        {
            _context = context;
            _response = new ResponseMessage<Domain.Models.Transaction>();
        }
        public async Task<ResponseMessage<Domain.Models.Transaction>> CreateTransaction(TransactionDto transactionDto)
        {
            
            try
            {
                Domain.Models.Transaction transaction = new Domain.Models.Transaction();
                transaction.ID = Guid.NewGuid();
                transaction.Amount = transactionDto.Amount;
                transaction.CrAccountID = transactionDto.CrAccountID;
                transaction.DrAccountID = transactionDto.DrAccountID;
                transaction.TransactionType = transactionDto.TransactionType;
                transaction.DateCreated = DateTime.Now;
                await _context.Transactions.AddAsync(transaction);
                await _context.SaveChangesAsync();
                _response.Data = transaction;
                _response.IsSuccessful = true;
                _response.Statuscode = "00";
                _response.Message = "Record inserted Successfully";
                return _response;
            }
            catch (Exception ex)
            {
                _response.Message = "An error occurred";
                _response.IsSuccessful = false;
                _response.Statuscode = "99";
                logger.Info(ex);
                return _response;
            }
        }

        public async Task<ResponseMessage<Domain.Models.Transaction>> GetTransactionById(Guid id)
        {
            var result = new Domain.Models.Transaction();
            try
            {
                result = await _context.Transactions.FindAsync(id);
                if (result != null)
                {
                    _response.Data = result;
                    _response.IsSuccessful = true;
                    _response.Statuscode = "00";
                    _response.Message = "Record inserted Successfully";
                    return _response;
                }
                _response.Statuscode = "01";
                _response.Message = "No Record";
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.Statuscode = "99";
                _response.Message = "An Error Occurred";
                logger.Error(ex);
                return _response;
            }

            
        }

        public async Task<List<Domain.Models.Transaction>> GetAllTransactions()
        {
          

            var result = new List<Domain.Models.Transaction>();
            try
            {
                result = await _context.Transactions.ToListAsync();
                if (result != null)
                    return result;    
                return null;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return result;
            }
        }

        public async Task<List<TransactionViewModelDto>> GetTransactionByPersonId(Guid id)
        {
            List<TransactionViewModelDto> list = new List<TransactionViewModelDto>();
            try
            {
                var account = await _context.Accounts.Where(x => x.PersonID == id).Include(x => x.Person).FirstOrDefaultAsync();
                if (account != null)
                {
                    var getById = _context.Transactions.Where(x => x.CrAccountID == account.ID || x.DrAccountID == account.ID).ToList();


                    if (getById.Count > 0)
                    {
                        foreach (var item in getById)
                        {
                            var accountDetail = _context.Accounts.Where(x => x.ID == item.DrAccountID || x.ID == item.CrAccountID).Include(x => x.Person).FirstOrDefault();
                            if (accountDetail != null)
                            {
                                TransactionViewModelDto transaction = new TransactionViewModelDto();
                                transaction.AccountName = accountDetail.Name;
                                transaction.Surname = accountDetail.Person.Surname;
                                transaction.Firstname = accountDetail.Person.Firstname;
                                transaction.Balance = accountDetail.Balance;
                                transaction.AccountNumber = accountDetail.Number;
                                transaction.TransactionType = item.TransactionType.ToString();
                                transaction.OffsetAccount = accountDetail.Name + "|" + accountDetail.Number;
                                transaction.TransactionDateTime = item.DateCreated.AddHours(12).ToString("tt", CultureInfo.InvariantCulture);

                                list.Add(transaction);
                            }

                        }
                        return list;
                    }

                }
                return null;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return list;
            }


        }
    }
}
