using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transaction.BusinessLogic.Service;
using Transaction.DataAccess;
using Transaction.Domain.Models;
using Transaction.Domain.Models.Dtos;

namespace Transaction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepo _repo;

        public AccountController(IAccountRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _repo.GetAllAccount();
            if(result != null)
                return Ok(result);
            return StatusCode(500, new ErrorResponse() { Code = "96", Message = "System Malfunction" });

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var result = await _repo.GetAccountById(id);
            if (result.Statuscode.Equals("00"))
                return Ok(result);
            if (result.Statuscode.Equals("01"))
                return NotFound(result);

            return StatusCode(500, new ErrorResponse() { Code = "96", Message = "System Malfunction" });
        }

        [HttpGet("[action]/{personId}")]
        public async Task<ActionResult> GetAccountByPersonID(Guid personId)
        {
            var result = await _repo.GetAccountByPersonId(personId);
            if(result.Statuscode.Equals("00"))
                return Ok(result);
            if (result.Statuscode.Equals("01"))
                return NotFound(result);

            return StatusCode(500, new ErrorResponse() { Code = "96", Message = "System Malfunction" });
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AccountDto account)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid entries!");

            var response = await _repo.CreateAccount(account);
            if (response.Statuscode.Equals("00"))
                return Ok(response);
            return StatusCode(500, new ErrorResponse() { Code = "96", Message = "System Malfunction" });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] AccountDto account, Guid id)
        {

            if (!ModelState.IsValid)
                return BadRequest("Invalid entries!");

           var response = await _repo.UpdateAccount(account, id);
            if(response.Statuscode.Equals("00"))
                return Ok(response);
            if (response.Statuscode.Equals("01"))
                return NotFound(response);
            return StatusCode(500, new ErrorResponse() { Code = "96", Message = "System Malfunction" });


        }
    }
}
