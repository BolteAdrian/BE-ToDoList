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
            if(_service.GetAllTasks() == null)
            {
                return null;
            }
            else
            {
               return _service.GetAllTasks();
            }
           
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
            if (_service.GetChilds(id) == null)
            {
                return null;
            }
            else
            {
                return _service.GetChilds(id);
            }
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
        /// <param name="secondaryTask"></param>
        /// <returns></returns>
        // POST api/<SecondaryTaskController>
        [HttpPost]
        public async Task<ActionResult> PostAsync(SecondaryTask secondaryTask)
        {
            if (secondaryTask != null)
            {
                return StatusCode(StatusCodes.Status200OK, await _service.AddTask(secondaryTask));
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
        /// <param name="secondaryTask"></param>The task that we want to update
        /// <returns></returns>
        // PUT api/<SecondaryTaskController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, SecondaryTask secondaryTask)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    secondaryTask.Id = id;
                    if (!SecondaryTaskExists(secondaryTask.Id))
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "The task doesn t exist");
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status200OK, await _service.UpdateTask(secondaryTask));
                    }
                }
                catch (Exception e)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, e.Message);
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "The task is not valid");
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
        public async Task<ActionResult> DeleteAsync(int id)
        {
            if (SecondaryTaskExists(id) == false)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "The id is null or the object doesn t exist");
            }
            return StatusCode(StatusCodes.Status200OK, await _service.DeleteTask(id));
        }
    }
}
