using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalRandkowy.API.Models;
using PortalRandkowy.API.Models.Data;

namespace PortalRandkowy.API.Controllers
{
    //GET http://localhost:5000/api/Values/100
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;

        public ValuesController(DataContext context)
        {
            this._context = context;
        }
        // GET api/values
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            //throw new Exception("Exception has been genereted");
            var values = await _context.Values.ToListAsync();
            return Ok(values);
        }

        // GET api/values/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(value);
        }

        // POST api/values
        //metoda dodawania wartosci
        [HttpPost]
        public async Task<IActionResult> AddValue([FromBody] Value value)
        {
            _context.Values.Add(value);
            await _context.SaveChangesAsync();
            return Ok(value);
        }

        // PUT api/values/5
        //metoda edytowania wartosci
        [HttpPut("{id}")]
        public async Task<IActionResult> EditValue(int id, [FromBody] Value value)
        {
            var data = await _context.Values.FindAsync(id);
            if(data == null)
                return NoContent();

            data.Name = value.Name;
            _context.Values.Update(data);
            await _context.SaveChangesAsync();
            return Ok(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteValue(int id)
        {
            var data = await _context.Values.FindAsync(id);
            _context.Values.Remove(data);
            await _context.SaveChangesAsync();
            return Ok(id);
        }
    }
}
