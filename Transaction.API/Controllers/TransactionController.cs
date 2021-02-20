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
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepo _repo;

        public TransactionController(ITransactionRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _repo.GetAllTransactions();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var result = await _repo.GetTransactionById(id);
            if (result == null)
                return BadRequest("Record not found!");
            
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TransactionDto transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid entries!");

            var response = await _repo.CreateTransaction(transaction);
            if(response.Statuscode == "00")
                return Ok(transaction);
            return StatusCode(500, new ErrorResponse() {Code = "96", Message = "System Malfunction"});
        }
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult> GetTransactionsByPersonId(Guid id)
        {
            var person = await _repo.GetTransactionByPersonId(id);
            if (person != null)
                return Ok(person);
            return NotFound("No record found");
        }
    }
}
