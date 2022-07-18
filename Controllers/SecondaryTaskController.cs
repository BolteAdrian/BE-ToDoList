using Business_access_layer.Services;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;


namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecondaryTaskController : ControllerBase
    {
        /// <summary>
        /// we initialize a service object to make the connection between the controller and the service
        /// </summary>
        public readonly ServiceSecondaryTask _service;
        public SecondaryTaskController(ServiceSecondaryTask service)
        {
            _service = service;
        }

        /// <summary>
        /// The get function return all the tasks
        /// </summary>
        /// <returns></returns>
        // GET: api/<SecondaryTaskController>
        [HttpGet]
        public  IEnumerable GetAsync()
        {
               return _service.GetAllTasks();
        }


        /// <summary>
        /// The GetChildsTask will return all the child tasks of the Principal task
        /// </summary>
        /// <param name="id"></param>The id of the principal task
        /// <returns></returns>
        // GET api/<SecondaryTaskController>/5
        [HttpGet("/childs/{id}")]
        public IEnumerable GetChildsTask(int id)
        {
                return _service.GetChilds(id);  
        }

        /// <summary>
        /// The getTask function return a task
        /// </summary>
        /// <param name="id"></param>The id of the task that we want to return
        /// <returns></returns>
        // GET api/<SecondaryTaskController>/5
        [HttpGet("{id}")]
        public SecondaryTask GetAsync(int id)
        {
                return _service.GetTask(id);
        }
        

        /// <summary>
        /// The post function create a new task
        /// </summary>
        /// <param name="secondaryTask"></param>
        /// <returns></returns>
        // POST api/<SecondaryTaskController>
        [HttpPost]
        public async Task<SecondaryTask> PostAsync(SecondaryTask secondaryTask)
        {
            if (secondaryTask != null)
            {
                return await _service.AddTask(secondaryTask);
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
        /// <param name="secondaryTask"></param>The task that we want to update
        /// <returns></returns>
        // PUT api/<SecondaryTaskController>/5
        [HttpPut("{id}")]
        public  Task<bool> PutAsync(int id, SecondaryTask secondaryTask)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    secondaryTask.Id = id;
                    if (!SecondaryTaskExists(secondaryTask.Id))
                    {
                        return Task.FromResult(false);
                    }
                    else
                    {
                        _service.UpdateTask(secondaryTask);
                        return Task.FromResult(true);
                    }
                }
                catch (Exception)
                {
                    throw;
 ;                }
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        /// <summary>
        /// This function will check if the object secondary task exist
        /// </summary>
        /// <param name="id"></param>The id of the task that we want to verify
        /// <returns></returns>
        private bool SecondaryTaskExists(int id) {
            if (id == 0)
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
        // DELETE api/<SecondaryTaskController>/5
        [HttpDelete("{id}")]
        public Task<bool> DeleteAsync(int id)
        {
            if (SecondaryTaskExists(id) == false)
            {
                return Task.FromResult(false);
            }
                 _service.DeleteTask(id);
                return Task.FromResult(true);

        }
    }
}
