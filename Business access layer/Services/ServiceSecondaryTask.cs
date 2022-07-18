using Data_Access_Layer.Models;
using Data_Access_Layer.Repositories;

namespace Business_access_layer.Services
{
    public class ServiceSecondaryTask
    {
        /// <summary>
        /// we initialize a repository object to make the connection between the repository and the service
        /// </summary>
        public readonly RepositorySecondaryTask _repository;

        public ServiceSecondaryTask(RepositorySecondaryTask repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// This function will create a new task
        /// </summary>
        /// <param name="secondaryTask"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<string> AddTask(SecondaryTask secondaryTask)
        {
            try
            {
                if (secondaryTask == null)
                {
                    return "This task is empty";
                }
                else
                {                
                        if (DateTime.Compare(secondaryTask.StartDate.Date, secondaryTask.EndDate.Date) <= 0)
                        {
                            
                            return await _repository.Create(secondaryTask);
                        }
                        return "EndData can t be before the StartDate";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        ///This function will remove the task 
        /// </summary>
        /// <param name="Id"></param>Id of the task that we want to delete
        /// <returns></returns>
        public async Task<string> DeleteTask(int Id)
        {
            try
            {
                if (Id != 0)
                {
                    var obj = _repository.GetAll().Where(x => x.Id == Id).FirstOrDefault();
                    if (obj != null)
                    {
                        await _repository.Delete(obj);
                        return "The task was removed";
                    }
                    return "The task doesn t exist";
                }
                return "The id is 0";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// This function will update the task
        /// </summary>
        /// <param name="secondaryTask"></param>The modified object
        /// <returns></returns>
        public async Task<string> UpdateTask(SecondaryTask secondaryTask)
        {
            try
            {
                if (secondaryTask != null)
                {
                    if (secondaryTask.Id != 0)
                    {
                        if (DateTime.Compare(secondaryTask.StartDate.Date, secondaryTask.EndDate.Date) <= 0)
                        {
                            
                            return await _repository.Update(secondaryTask);
                        }
                        else
                        {
                            return "EndData can t be before the StartDate";
                        }
                    }
                    return "The id is null";
                }
                return "The task doesn t exist";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// Returneaza toate task-urile secundare
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SecondaryTask> GetAllTasks()
        {
            try
            {
                if (_repository.GetAll().ToList() != null)
                {
                    return _repository.GetAll().ToList();
                }
                else
                {
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
        public IEnumerable<SecondaryTask> GetChilds(int id)
        {
            try
            {
                if (id != 0)
                {
                    if (_repository.GetChildTask(id).ToList() != null){
                        return null;
                    }
                    else {
                        return _repository.GetChildTask(id);
                    }
                }
                else return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public SecondaryTask GetTask(int id)
        {
            try
            {
                if (_repository.GetById(id) != null)
                {
                    return null;
                }
                else
                {
                    return _repository.GetById(id);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}