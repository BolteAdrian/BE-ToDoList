using Data_Access_Layer.Contacts;
using Data_Access_Layer.Data;
using Data_Access_Layer.Models;

namespace Data_Access_Layer.Repositories
{
    public class RepositoryPrincipalTask : IRepository<PrincipalTask>
    {
        /// <summary>
        ///  we initialize an object to make the connection between the repository and the data base
        /// </summary>
        private readonly masterContext _context;

        public RepositoryPrincipalTask(masterContext context)
        {
            _context = context;
        }

        /// <summary>
        /// We add a new task in database
        /// </summary>
        /// <param name="principalTask"></param>the new object
        /// <returns></returns>
        public async Task<PrincipalTask> Create(PrincipalTask principalTask)
        {
            try
            {
                if (principalTask != null)
                {
                    var obj = _context.Add(principalTask);
                    await _context.SaveChangesAsync();
                    return obj.Entity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw new ArgumentNullException(nameof(principalTask));
            }
        }

        /// <summary>
        /// We remove a task from database
        /// </summary>
        /// <param name="principalTask"></param>the object that we want to delete
        public  void Delete(PrincipalTask principalTask)
        {
            try
            {
                if (principalTask != null)
                {
                    var secondaryTasks = _context.SecondaryTasks.Where(s => s.PrincipalTaskId == principalTask.Id);

                    foreach (var secondaryTask in secondaryTasks)
                    {
                        if (secondaryTask != null)
                        {   _context.Remove(secondaryTask);
                             _context.SaveChangesAsync();
                        }
                        else
                        {
                            break;
                        }
                    }
                        _context.Remove(principalTask);
                        _context.SaveChangesAsync();    
                }
            
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Return all the principal tasks from the database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PrincipalTask> GetAll()
        {
            try
            {
                var obj = _context.PrincipalTasks.OrderByDescending(x=>x.Priority).ToList();
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
        public PrincipalTask GetById(int Id)
        {
            try
            {
                    var Obj = _context.PrincipalTasks.FirstOrDefault(x => x.Id == Id);
                if (Obj != null) { 
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
        /// This function will update the principal task and if the task property checked is changed to true,
        /// than all the secondary tasks well have the property checked true
        /// </summary>
        /// <param name="principalTask"></param>the modified object
        /// <returns></returns>
        public void Update(PrincipalTask principalTask)
        {
            try
            {
                if (principalTask != null)
                {
                    if(principalTask.Checked==true) {
                        var secondaryTasks = _context.SecondaryTasks.Where(s => s.PrincipalTaskId == principalTask.Id).ToList();
                        foreach(var secondaryTask in secondaryTasks)
                        {
                            if (secondaryTask != null)
                            {
                                secondaryTask.Checked = true;
                                _context.Update(secondaryTask);
                                _context.SaveChanges();
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    _context.Update(principalTask);
                     _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
