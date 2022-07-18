using Business_access_layer.Services;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Mvc;
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
            if (_service.GetAllTasks() == null)
            {
                return null;
            }
            else
            {
                return _service.GetAllTasks();
            }
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
            if (_service.GetTask(id) == null)
            {
                return null;
            }
            else { 
                return _service.GetTask(id);
                  }
        }

        /// <summary>
        /// The post function create a new task
        /// </summary>
        /// <param name="principalTask"></param>
        /// <returns></returns>
        // POST api/<PrincipalTaskController>
        [HttpPost]
        public async Task<ActionResult> PostAsync(PrincipalTask principalTask)
        {
            if (principalTask != null)
            {
                return StatusCode(StatusCodes.Status200OK, await _service.AddTask(principalTask));
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "The task is empty");
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
        public async Task<ActionResult> PutAsync(int id,PrincipalTask principalTask)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    principalTask.Id = id;
                    if (!PrincipalTaskExists(principalTask.Id))
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "The task doesn t exist");
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status200OK, await _service.UpdateTask(principalTask));
                    }
                }
                catch (Exception e)
                {
                    return StatusCode(StatusCodes.Status400BadRequest,e.Message);
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "The task is not valid");
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
        public async Task<ActionResult> DeleteAsync(int id)
        {
            if (PrincipalTaskExists(id) == false)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "The id is null or the object doesn t exist");
            } 
            return StatusCode(StatusCodes.Status200OK, await _service.DeleteTask(id));
        }
    }
}
