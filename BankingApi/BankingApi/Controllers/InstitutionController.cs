using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using BankingApi.Models;
using System.Net;
using System;

namespace BankingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutionController : ControllerBase
    {

        private readonly InstitutionContext _context;

        public InstitutionController(InstitutionContext context)
        {
            _context = context;
        }

        // GET: api/<InstitutionController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Institution>>> GetInstitutions()
        {
            return await _context.Institutions.ToListAsync();
        }

        // GET api/<InstitutionController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Institution
        [HttpPost]
        public async Task<ActionResult<Institution>> PostMember(Institution institution)
        {
            if (ModelState.IsValid)
            {
                _context.Institutions.Add(institution);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetInstitutions", new { id = institution.InstitutionId }, institution);
            }
            else
            {
                return Json(new { status = "error", message = "error creating customer" });
            }

        }

        private ActionResult<Institution> Json(object p)
        {
            throw new NotImplementedException();
        }

        // PUT api/<InstitutionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<InstitutionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
