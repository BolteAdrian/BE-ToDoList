using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using System.Collections;


namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrincipalTaskController : ControllerBase
    {
        private readonly masterContext _context;

        public PrincipalTaskController(masterContext context)
        {
            _context = context;
        }

        // GET: api/<PrincipalTaskController>
        [HttpGet]
        public async Task<IEnumerable> GetAsync()
        {
            return await _context.PrincipalTasks.ToListAsync();
        }

        // GET api/<PrincipalTaskController>/5
        [HttpGet("{id}")]
        public async Task<IEnumerable> GetAsync(int id)
        {
            return await _context.PrincipalTasks.Where(x => x.Id == id).ToListAsync();
        }

        // POST api/<PrincipalTaskController>
        [HttpPost]
        public async Task<bool> PostAsync(PrincipalTask principalTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(principalTask);
                await _context.SaveChangesAsync();
                return true;
            }
            else {
                return false; 
            }
        }

        // PUT api/<PrincipalTaskController>/5
        [HttpPut("{id}")]
        public async Task<bool> PutAsync(int id, PrincipalTask principalTask)
        {
            if (id != principalTask.Id)
            {
                return false;
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(principalTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrincipalTaskExists(principalTask.Id))
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

        private bool PrincipalTaskExists(int id)
        {
            return (_context.PrincipalTasks?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // DELETE api/<PrincipalTaskController>/5
        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(int id)
        {
            if (id == 0 || _context.PrincipalTasks == null)
            {
                return false;
            }

            var principalTask = await _context.PrincipalTasks
                .FirstOrDefaultAsync(m => m.Id == id);

            if (principalTask == null)
            {
                return false;
            }
            else
            {
                _context.PrincipalTasks.Remove(principalTask);
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
