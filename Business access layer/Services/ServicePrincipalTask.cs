using Data_Access_Layer.Models;
using Data_Access_Layer.Repositories;

namespace Business_access_layer.Services
{
    public class ServicePrincipalTask
    {
        /// <summary>
        /// we initialize a repository object to make the connection between the repository and the service
        /// </summary>
        public readonly RepositoryPrincipalTask _repository;
        public ServicePrincipalTask(RepositoryPrincipalTask repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// This function will create a new task
        /// </summary>
        /// <param name="principalTask"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<string> AddTask(PrincipalTask principalTask)
        {
            try
            {
                if (principalTask == null)
                {
                    return "This task is empty";
                }
                else
                {
                    var DateNow = DateTime.Now;
                    if (DateTime.Compare(principalTask.StartDate.Date, DateNow.Date) >= 0) {
                        if (DateTime.Compare(principalTask.StartDate.Date, principalTask.EndDate.Date) <= 0)
                        {
                            await _repository.Create(principalTask);
                            return "The task was created";
                        }
                        return "EndData can t be before the StartDate";
                    }
                    return "StartDate can t be before the the today s date";
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
        /// <param name="principalTask"></param>The modified object
        /// <returns></returns>
        public async Task<string> UpdateTask(PrincipalTask principalTask)
        {
            try
            {
                if (principalTask != null)
                {
                    if (principalTask.Id != 0)
                    {
                        if (DateTime.Compare(principalTask.StartDate.Date, principalTask.EndDate.Date) <= 0)
                        {
                            await _repository.Update(principalTask);
                            return "The task was modified";
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
        /// Returneaza toate task-urile principale
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PrincipalTask> GetAllTasks()
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
        /// Returneaza un task in functie de id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PrincipalTask GetTask(int id)
        {
            try
            {
                if (id != 0 || _repository.GetById(id)!=null)
                {
                    return _repository.GetById(id);
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}