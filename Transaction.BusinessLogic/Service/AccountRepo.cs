using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NLog;
using Transaction.DataAccess;
using Transaction.Domain.Models;
using Transaction.Domain.Models.Dtos;

namespace Transaction.BusinessLogic.Service
{
    public class AccountRepo : IAccountRepo
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly TContext _context;
        private ResponseMessage<Account> _response;
        public AccountRepo(TContext context)
        {
            _context = context;
            _response = new ResponseMessage<Account>();
        }
        public async Task<ResponseMessage<Account>> CreateAccount(AccountDto accountDto)
        {


            try
            {
                Account account = new Account();
                account.ID = Guid.NewGuid();
                account.Name = accountDto.Name;
                account.Number = accountDto.Number;
                account.PersonID = accountDto.PersonID;
                account.Balance = accountDto.Balance;
                account.DateCreated = DateTime.Now;
                await _context.Accounts.AddAsync(account);
                await _context.SaveChangesAsync();
                _response.Data = account;
                _response.IsSuccessful = true;
                _response.Statuscode = "00";
                _response.Message = "Record inserted Successfully";
                return _response;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                _response.IsSuccessful = false;
                _response.Statuscode = "99";
                _response.Message = "An error occurred";
                return _response;
            }
        }

        public async Task<ResponseMessage<Account>> GetAccountById(Guid id)
        {
            try
            {
                var result = await _context.Accounts.Where(x=>x.ID == id).Include(x=>x.Person).FirstOrDefaultAsync();
                if (result != null)
                {
                    _response.Data = result;
                    _response.IsSuccessful = true;
                    _response.Statuscode = "00";
                    _response.Message = "Successfully";
                    return _response;
                }
                _response.Message = "No record";
                _response.Statuscode = "01";
                return _response;
            }
            catch (Exception ex)
            {

                logger.Error(ex);
                _response.IsSuccessful = false;
                _response.Statuscode = "99";
                _response.Message = "An error occurred";
                return _response;
            }
        }

        public async Task<ResponseMessage<Account>> GetAccountByPersonId(Guid personId)
        {
            try
            {
                var result = await _context.Accounts.Where(x => x.PersonID == personId).Include(x=>x.Person).FirstOrDefaultAsync();
                if (result != null)
                {
                    _response.Data = result;
                    _response.IsSuccessful = true;
                    _response.Statuscode = "00";
                    _response.Message = "Successfully";
                    return _response;
                }
                _response.Message = "No record";
                _response.Statuscode = "01";
                return _response;
            }
            catch (Exception ex)
            {

                logger.Error(ex);
                _response.IsSuccessful = false;
                _response.Statuscode = "99";
                _response.Message = "An error occurred";
                return _response;
            }
            
        }

        public async Task<List<Account>> GetAllAccount()
        {
            var result = new  List<Account>();
            try
            {
                result = await _context.Accounts.Include(x=>x.Person).ToListAsync();
                if (result.Count > 0)
                    return result;
                return null;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return result;
            }
            
        }

        public async Task<ResponseMessage<Account>> UpdateAccount(AccountDto accountDto, Guid id)
        {
            try
            {
                var account = _context.Accounts.Where(x => x.ID == id).Include(x=>x.Person).FirstOrDefault();
                if (account != null)
                {
                    account.Name = accountDto.Name;
                    account.Number = accountDto.Number;
                    account.PersonID = accountDto.PersonID;
                    account.Balance = accountDto.Balance;
                    _context.Accounts.Update(account);
                    await _context.SaveChangesAsync();
                    _response.Data = account;
                    _response.IsSuccessful = true;
                    _response.Statuscode = "00";
                    _response.Message = "record Successfully updated";
                    return _response;
                }
                _response.Statuscode = "00";
                _response.Message = "No record";
                return _response;
            }
            catch (Exception ex)
            {

                logger.Error(ex);
                _response.IsSuccessful = false;
                _response.Statuscode = "99";
                _response.Message = "An rrror occurred";
                return _response;
            }
            
        }
    }
}
