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
        /// <summary>
        /// we initialize a service object to make the connection between the controller and the service
        /// </summary>
        public readonly ServicePrincipalTask _service;
        public PrincipalTaskController(ServicePrincipalTask service)
        {
            _service = service;
        }

        /// <summary>
        /// The get function return all the tasks
        /// </summary>
        /// <returns></returns>
        // GET: api/<PrincipalTaskController>
        [HttpGet]
        public IEnumerable GetAsync()
        {
                return _service.GetAllTasks();
        }

        /// <summary>
        /// The getTask function return a task
        /// </summary>
        /// <param name="id"></param>The id of the task that we want to return
        /// <returns></returns>
        // GET api/<PrincipalTaskController>/5
        [HttpGet("{id}")]
        public PrincipalTask GetAsync(int id)
        {
                return _service.GetTask(id);
        }

        /// <summary>
        /// The post function create a new task
        /// </summary>
        /// <param name="principalTask"></param>
        /// <returns></returns>
        // POST api/<PrincipalTaskController>
        [HttpPost]
        public async Task<PrincipalTask> PostAsync(PrincipalTask principalTask)
        {
            if (principalTask != null)
            {
                return await  _service.AddTask(principalTask);
            }
            else
            {
                return null;
            }
        }
      

        /// <summary>
        /// The PUT function update the task
        /// </summary>
        /// <param name="id"></param>The id of the task that we want to update
        /// <param name="principalTask"></param>The task that we want to update
        /// <returns></returns>
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

        /// <summary>
        /// This function will check if the object principal task exist
        /// </summary>
        /// <param name="id"></param>The id of the task that we want to verify
        /// <returns></returns>
        private bool PrincipalTaskExists(int id)
        {
            if(id==0)
            {
                return false; 
            }
            else if (_service.GetTask(id) == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// The Delete function will remove the task 
        /// </summary>
        /// <param name="id"></param>The id of the task that we want to remove
        /// <returns></returns>
        // DELETE api/<PrincipalTaskController>/5
        [HttpDelete("{id}")]
        public  Task<bool> DeleteAsync(int id)
        {
            if (PrincipalTaskExists(id) == false)
            {
                return Task.FromResult(false);
            } 
             _service.DeleteTask(id);
            return Task.FromResult(true);
        }
    }
}
