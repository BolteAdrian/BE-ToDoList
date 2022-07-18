using Data_Access_Layer.Models;
using Data_Access_Layer.Repositories;

namespace Business_access_layer.Services
{
    public class ServiceSecondaryTask
    {
        public readonly RepositorySecondaryTask _repository;
        public ServiceSecondaryTask(RepositorySecondaryTask repository)
        {
            _repository = repository;
        }
        //Create Method
        public  Task<SecondaryTask> AddTask(SecondaryTask secondaryTask)
        {
            try
            {
                if (secondaryTask == null)
                {
                    throw new ArgumentNullException(nameof(secondaryTask));
                }
                else
                {
                    //return _repository
                    return  _repository.Create(secondaryTask);
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
        public void UpdateTask(SecondaryTask secondaryTask)
        {
            try
            {
                if (secondaryTask.Id != 0)
                {
                    _repository.Update(secondaryTask);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<SecondaryTask> GetAllTasks()
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
        public IEnumerable<SecondaryTask> GetChilds(int id)
        {
            try
            {
                return _repository.GetChildTask(id);
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
                return _repository.GetById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}