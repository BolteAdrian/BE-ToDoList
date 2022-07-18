using Data_Access_Layer.Models;
using Data_Access_Layer.Repositories;

namespace Business_access_layer.Services
{
    public class ServicePrincipalTask
    {
        public readonly RepositoryPrincipalTask _repository;
        public ServicePrincipalTask(RepositoryPrincipalTask repository)
        {
            _repository = repository;
        }
        //Create Method
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

                    //return _repository
                    return _repository.Create(principalTask);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

       
        public void DeleteTask(int Id)
        {
            try
            {
                if (Id != 0)
                {
                    var obj = _repository.GetAll().Where(x => x.Id == Id).FirstOrDefault();
                    _repository.Delete(obj);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void UpdateTask(PrincipalTask principalTask)
        {
            try
            {
                if (principalTask.Id != 0)
                {
                    _repository.Update(principalTask);
                }
            }
            catch (Exception)
            {
                
            }
        }
        public IEnumerable<PrincipalTask> GetAllTasks()
        {
            try
            {
                return _repository.GetAll().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PrincipalTask GetTask(int id)
        {
            try
            {
                return _repository.GetById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}