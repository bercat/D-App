using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
     
    //Alt i ValuesController må være en [Authorize] forespørsel 
    [Authorize]
    [Route("api/[controller]")] 
    [ApiController]// tvinger fram bruken av [Route("api/[controller]")] 
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;
        public ValuesController(DataContext context)
        {
            _context = context;

        }
        // GET api/values
        //ActionResult<IEnumerable<string>> returnerer string verdier
        //ønsker ikke bare å kunne returnere strings
        //Så derfor bruker vi IActionResult 
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            //gir oss tilgang til entities framework metoder og iDb
            //som her er Values 
            //som ToList metoden går ut å henter fra databsen vår, som en liste
            var values = await _context.Values.ToListAsync();//Async med tanke på trafikken
            
            //var values returnerer til klient med http 200 ok response
            return Ok(values);
        }

        // GET api/values/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            //FirstOrDefault returnerer standar verdien, for hver enkelte gang den returnerer verdien.
            // returnerer også NULL i stedet for Exceptions
            var value = await _context.Values.FirstOrDefaultAsync(x => x.id == id);
            //x er verdien vi returnerer. Id brukes for å matche id som er lik Id'n vi sender inn.

            return Ok(value); 
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
