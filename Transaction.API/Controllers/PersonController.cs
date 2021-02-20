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
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepo _repo;

        public PersonController(IPersonRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _repo.GetAllPersons();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var result = await _repo.GetPersonById(id);
            if (result.Statuscode.Equals("00"))
                return Ok(result);
            if (result.Statuscode.Equals("01"))
                return NotFound(result);

            return StatusCode(500, new ErrorResponse() { Code = "96", Message = "System Malfunction" });
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PersonDto person)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid entries!");

            var response = await _repo.CreatePerson(person);
            if (response.Statuscode.Equals("00"))
                return Ok(response);
            return StatusCode(500, new ErrorResponse() { Code = "96", Message = "System Malfunction" });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] PersonDto person, Guid id)
        {

            if (!ModelState.IsValid)
                return BadRequest("Invalid entries!");

           var response = await _repo.UpdatePerson(person, id);
            if(response.Statuscode.Equals("00"))
                return Ok(response);
            if (response.Statuscode.Equals("01"))
                return NotFound(response);
            return StatusCode(500, new ErrorResponse() { Code = "96", Message = "System Malfunction" });
        }
    }
}
