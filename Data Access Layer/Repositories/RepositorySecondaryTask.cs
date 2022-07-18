using Data_Access_Layer.Contacts;
using Data_Access_Layer.Data;
using Data_Access_Layer.Models;

namespace Data_Access_Layer.Repositories
{
    public class RepositorySecondaryTask : IRepository<SecondaryTask>
    {
        /// <summary>
        ///  we initialize an object to make the connection between the repository and the data base
        /// </summary>
        private readonly masterContext _context;

        public RepositorySecondaryTask(masterContext context)
        {
            _context = context;
        }

        /// <summary>
        /// We add a new task in database
        /// </summary>
        /// <param name="secondaryTask"></param>the new object
        /// <returns></returns>
        public async Task<string> Create(SecondaryTask secondaryTask)
        {
            try
            {
                if (secondaryTask != null)
                {
                    var primary_task = _context.PrincipalTasks.Where(x => x.Id == secondaryTask.PrincipalTaskId).FirstOrDefault();
                    if (primary_task != null)
                    {
                        if (DateTime.Compare(secondaryTask.StartDate.Date, primary_task.StartDate.Date) >= 0)
                        {
                            if (DateTime.Compare(secondaryTask.EndDate.Date, primary_task.EndDate.Date) <= 0)
                            {
                             var obj = _context.Add(secondaryTask);
                             await _context.SaveChangesAsync();
                             return "The task was created";
                            }
                            else
                            {
                                return "The secondary task can t end after the principal one";
                            }
                        }
                        else
                        {
                            return "The secondary task can t start before the principal one";
                        }
                    }
                    else
                    {
                        return "The parent task is invalid";
                    }
                }
                
                else
                {
                    return "The task is empty";
                }
            
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// We remove a task from database
        /// </summary>
        /// <param name="secondaryTask"></param>the object that we want to delete
        public async Task<string> Delete(SecondaryTask secondaryTask)
        {
            try
            {
                if (secondaryTask != null)
                {
                    _context.Remove(secondaryTask);
                    await _context.SaveChangesAsync();
                    return "The task was deleted";
                }
                else
                {
                    return "The task doesn t exist";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// Return all the secondary tasks from the database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SecondaryTask> GetAll()
        {
            try
            {
                var obj = _context.SecondaryTasks.OrderByDescending(x => x.Priority).ToList();
                if (obj != null) return obj;
                else return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Return one task from the databse
        /// </summary>
        /// <param name="Id"></param>Id of the task that we want to get
        /// <returns></returns>
        public SecondaryTask GetById(int Id)
        {
            try
            {
                var Obj = _context.SecondaryTasks.FirstOrDefault(x => x.Id == Id);
                if (Obj != null)
                {
                    return Obj;
                }
                else {
                    return null; 
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The GetChildsTask will return all the child tasks of the Principal task
        /// </summary>
        /// <param name="id"></param>The id of the principal task
        /// <returns></returns>
        public IEnumerable<SecondaryTask> GetChildTask(int? id)
        {
            var masterContext = _context.SecondaryTasks.Where(s => s.PrincipalTaskId == id);

            if (masterContext == null)
            {
                return null;
            }
            else
            {
                return masterContext.ToList();
            }
        }

        /// <summary>
        /// This function will update the secondaryTask task 
        /// </summary>
        /// <param name="principalTask"></param>the modified object
        /// <returns></returns>
        public async Task<string> Update(SecondaryTask secondaryTask)
        {
            try
            {
                var primary_task = _context.PrincipalTasks.Where(x => x.Id == secondaryTask.PrincipalTaskId).FirstOrDefault();
                if (primary_task != null)
                {
                    if (DateTime.Compare(secondaryTask.StartDate.Date, primary_task.StartDate.Date) >= 0)
                    {
                        if (DateTime.Compare(secondaryTask.EndDate.Date, primary_task.EndDate.Date) <= 0)
                        {
                            _context.Update(secondaryTask);
                            await _context.SaveChangesAsync();
                            return "The task was modified";
                        }
                        else
                        {
                            return "The secondary task can t end after the principal one";
                        }
                    }
                    else
                    {
                        return "The secondary task can t start before the principal one";
                    }
                }
                else
                {
                    return "The parent task is invalid";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
