using Business_access_layer.Services;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;


namespace Project.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SecondaryTaskController : ControllerBase
    {
        public readonly ServiceSecondaryTask _service;
        public SecondaryTaskController(ServiceSecondaryTask service)
        {
            _service = service;
        }

        // GET: api/<SecondaryTaskController>
        [HttpGet]
        public  IEnumerable GetAsync()
        {
            return _service.GetAllTasks();
        }

    

        // GET api/<SecondaryTaskController>/5
        [HttpGet("/childs/{id}")]
        public IEnumerable GetChildsTask(int id)
        {
            return _service.GetChilds(id);
        }

        // GET api/<SecondaryTaskController>/5
        [HttpGet("{id}")]
        public SecondaryTask GetAsync(int id)
        {
            return _service.GetTask(id);
        }

        // POST api/<SecondaryTaskController>
        [HttpPost]
        public Task<SecondaryTask> PostAsync(SecondaryTask secondaryTask)
        {
            return _service.AddTask(secondaryTask);
        }

        // PUT api/<SecondaryTaskController>/5
        [HttpPut("{id}")]
        public Task<bool> PutAsync(int id, SecondaryTask secondaryTask)
        {
            if (id != secondaryTask.Id)
            {
                return Task.FromResult(false);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _service.UpdateTask(id);
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SecondaryTaskExists(secondaryTask.Id))
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

        private bool SecondaryTaskExists(int id) {
            if(_service.GetTask(id) == null)
            {
                return false;
            }

            return true;
        }

        // DELETE api/<SecondaryTaskController>/5
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
