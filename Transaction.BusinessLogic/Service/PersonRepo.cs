using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NLog;
using Transaction.DataAccess;
using Transaction.Domain.Models;
using Transaction.Domain.Models.Dtos;

namespace Transaction.BusinessLogic.Service
{
    public class PersonRepo : IPersonRepo
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly TContext _context;
        private ResponseMessage<Person> _response;
        public PersonRepo(TContext context)
        {
            _context = context;
            _response = new ResponseMessage<Person>();
        }
        public async Task<ResponseMessage<Person>> CreatePerson(PersonDto personDto)
        {
            try
            {
                Person person = new Person();
                person.ID = Guid.NewGuid();
                person.Firstname = personDto.Firstname;
                person.Surname = personDto.Surname;
                person.PhoneNumber = personDto.PhoneNumber;
                person.EmailAddress = personDto.EmailAddress;
                person.DateCreated = DateTime.Now;
                await _context.People.AddAsync(person);
                await _context.SaveChangesAsync();
                _response.Data = person;
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

        public async Task<ResponseMessage<Person>> GetPersonById(Guid id)
        {

            try
            {
                var result = await _context.People.FindAsync(id);
                if (result != null)
                {
                    _response.Data = result;
                    _response.IsSuccessful = true;
                    _response.Statuscode = "00";
                    _response.Message = "Successfully";
                    return _response;

                }
                _response.Statuscode = "01";
                _response.Message = "No Record";
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

        public async Task<List<Person>> GetAllPersons()
        {
            var result = new List<Person>();
            try
            {
                result = await _context.People.ToListAsync();
                if(result.Count > 0)
                    return result;
                return null;

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return result;
            }
        }

        public async Task<ResponseMessage<Person>> UpdatePerson(PersonDto personDto, Guid id)
        {
            try
            {
                var person = await _context.People.FirstOrDefaultAsync(x => x.ID == id);
                if (person != null)
                {
                    person.PhoneNumber = personDto.PhoneNumber;
                    person.Firstname = personDto.Firstname;
                    person.Surname = personDto.Surname;
                    person.EmailAddress = personDto.EmailAddress;
                    _context.People.Update(person);
                    await _context.SaveChangesAsync();
                    _response.Data = person;
                    _response.IsSuccessful = true;
                    _response.Statuscode = "00";
                    _response.Message = "Record updated Successfully";
                    return _response;
                }
                _response.Statuscode = "01";
                _response.Message = "No Record";
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
    }
}
