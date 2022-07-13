using Business_access_layer.Services;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;


namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrincipalTaskController : ControllerBase
    {
        public readonly ServicePrincipalTask _service;
        public PrincipalTaskController(ServicePrincipalTask service)
        {
            _service = service;
        }

        // GET: api/<PrincipalTaskController>
        [HttpGet]
        public IEnumerable GetAsync()
        {
            return _service.GetAllTasks();
        }

        // GET api/<PrincipalTaskController>/5
        [HttpGet("{id}")]
        public PrincipalTask GetAsync(int id)
        {
            return _service.GetTask(id);
        }

        // POST api/<PrincipalTaskController>
        [HttpPost]
        public Task<PrincipalTask> PostAsync(PrincipalTask principalTask)
        {
            return _service.AddTask(principalTask);
        }

        // PUT api/<PrincipalTaskController>/5
        [HttpPut("{id}")]
        public Task<bool> PutAsync(int id,PrincipalTask principalTask)
        {
           
            if (ModelState.IsValid)
            {
                try
                {
                    principalTask.Id = id;
                    _service.UpdateTask(principalTask);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrincipalTaskExists(principalTask.Id))
                    {
                        return Task.FromResult(false);
                    }
                    else
                    {
                        throw;
                    }
                }
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        private bool PrincipalTaskExists(int id)
        {
            if (_service.GetTask(id) == null)
            {
                return false;
            }

            return true;
        }

        // DELETE api/<PrincipalTaskController>/5
        [HttpDelete("{id}")]
        public Task<bool> DeleteAsync(int id)
        {
            if (id == 0 || _service.GetTask(id) == null)
            {
                return Task.FromResult(false);
            }
            _service.DeleteTask(id);

            return Task.FromResult(true);
        }
    }
}
