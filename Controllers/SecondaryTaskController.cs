using Data_Access_Layer.Data;
using Data_Access_Layer.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecondaryTaskController : ControllerBase
    {
        private readonly masterContext _context;

        public SecondaryTaskController(masterContext context)
        {
            _context = context;
        }

        // GET: api/<SecondaryTaskController>
        [HttpGet]
        public async Task<IEnumerable> GetAsync()
        {
            return await _context.SecondaryTasks.ToListAsync();
        }

        // GET api/<SecondaryTaskController>/5
        [HttpGet("{id}")]
        public async Task<IEnumerable> GetAsync(int id)
        {
            return await _context.SecondaryTasks.Where(x => x.Id == id).ToListAsync();
        }

        // POST api/<SecondaryTaskController>
        [HttpPost]
        public async Task<bool> PostAsync(SecondaryTask secondaryTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(secondaryTask);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        // PUT api/<SecondaryTaskController>/5
        [HttpPut("{id}")]
        public async Task<bool> PutAsync(int id, SecondaryTask secondaryTask)
        {
            if (id != secondaryTask.Id)
            {
                return false;
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(secondaryTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SecondaryTaskExists(secondaryTask.Id))
                    {
                        return false;
                    }
                    else
                    {
                        throw;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool SecondaryTaskExists(int id)
        {
            return (_context.SecondaryTasks?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // DELETE api/<SecondaryTaskController>/5
        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(int id)
        {
            if (id == 0 || _context.SecondaryTasks == null)
            {
                return false;
            }

            var secondaryTask = await _context.SecondaryTasks
                .FirstOrDefaultAsync(m => m.Id == id);

            if (secondaryTask == null)
            {
                return false;
            }
            else
            {
                _context.SecondaryTasks.Remove(secondaryTask);
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
