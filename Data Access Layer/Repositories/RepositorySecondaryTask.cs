using Data_Access_Layer.Contacts;
using Data_Access_Layer.Data;
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Data_Access_Layer.Repositories
{
    public class RepositorySecondaryTask : IRepository<SecondaryTask>
    {
        private readonly masterContext _context;

        public RepositorySecondaryTask(masterContext context)
        {
            _context = context;
        }

        public async Task<SecondaryTask> Create(SecondaryTask secondaryTask)
        {
            try
            {
                if (secondaryTask != null)
                {
                    var obj = _context.Add<SecondaryTask>(secondaryTask);
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
                throw;
            }
        }

        public void Delete(SecondaryTask secondaryTask)
        {
            try
            {
                if (secondaryTask != null)
                {
                    var obj = _context.Remove(secondaryTask);
                    if (obj != null)
                    {
                        _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

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
        public SecondaryTask GetById(int Id)
        {
            try
            {
                if (Id != null)
                {
                    var Obj = _context.SecondaryTasks.FirstOrDefault(x => x.Id == Id);
                    if (Obj != null) return Obj;
                    else { return null; }
                }
                else
                {
                  
                    return null;
                }
            }
            catch (Exception e)
            {



                throw;
               
            }
        }

        public IEnumerable<SecondaryTask> GetChildTask(int? id)
        {

            if (id == null || _context.SecondaryTasks == null)
            {
                return null;
            }
            var masterContext = _context.SecondaryTasks.Where(s => s.PrincipalTaskId == id);
 
            return masterContext.ToList();
        }
        public void Update(SecondaryTask secondaryTask)
        {
            try
            {
                if (secondaryTask != null)
                {
                    var obj = _context.Update(secondaryTask);
                    if (obj != null) _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
