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
        public Task<PrincipalTask> AddTask(PrincipalTask principalTask)
        {
            try
            {
                if (principalTask == null)
                {
                    throw new ArgumentNullException(nameof(principalTask));
                }
                else
                {
                    var DateNow = DateTime.Now;
                    if (DateTime.Compare(principalTask.StartDate.Date, DateNow.Date) >= 0) {
                        if (DateTime.Compare(principalTask.StartDate.Date, principalTask.EndDate.Date) <= 0)
                        {
                            return _repository.Create(principalTask);
                            
                        }
                        return null;
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

       /// <summary>
       ///This function will remove the task 
       /// </summary>
       /// <param name="Id"></param>Id of the task that we want to delete
       /// <returns></returns>
        public void DeleteTask(int Id)
        {
            try
            {
                if (Id != 0)
                {
                    var obj = _repository.GetAll().Where(x => x.Id == Id).FirstOrDefault();
                    if (obj != null)
                    {
                         _repository.Delete(obj);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This function will update the task
        /// </summary>
        /// <param name="principalTask"></param>The modified object
        /// <returns></returns>
        public void UpdateTask(PrincipalTask principalTask)
        {
            try
            {
                if (principalTask != null)
                {
                    if (principalTask.Id != 0)
                    {
                        if (DateTime.Compare(principalTask.StartDate.Date, principalTask.EndDate.Date) <= 0)
                        {
                             _repository.Update(principalTask);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
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
                if (id != 0 )
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