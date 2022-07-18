using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Contacts
{
        public interface IRepository<T>
        {
            public Task<string> Create(T _object);
            public Task<string> Delete(T _object);
            public Task<string> Update(T _object);
            public IEnumerable<T> GetAll();
            public T GetById(int Id);
        }
}
