using Data_Access_Layer.Contacts;
using Data_Access_Layer.Data;
using Data_Access_Layer.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class RepositoryPrincipalTask : IRepository<PrincipalTask>
    {
        private readonly masterContext _context;

        public RepositoryPrincipalTask(masterContext context)
        {
            _context = context;
        }

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
                throw;
            }
        }

        public void Delete(PrincipalTask principalTask)
        {
            try
            {
                if (principalTask != null)
                {

                    var secondaryTasks = _context.SecondaryTasks.Where(s => s.PrincipalTaskId == principalTask.Id);
                    foreach (var secondaryTask in secondaryTasks)
                    {
                        var obj2 = _context.Remove(secondaryTask);
                        if (obj2 != null)
                        {
                            _context.SaveChangesAsync();
                        }
                        
                    }
                    var obj = _context.Remove(principalTask);
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
        public PrincipalTask GetById(int Id)
        {
            try
            {
                if (Id != 0)
                {
                    var Obj = _context.PrincipalTasks.FirstOrDefault(x => x.Id == Id);
                    if (Obj != null) return Obj;
                    else return null;
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
                            secondaryTask.Checked = true;
                            var obj2 = _context.Update(secondaryTask);
                            if (obj2 != null) _context.SaveChanges();
                        }
                    }

                    var obj = _context.Update(principalTask);
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
