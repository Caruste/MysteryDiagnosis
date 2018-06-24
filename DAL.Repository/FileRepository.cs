using DAL.Interfaces.Interfaces;
using Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class FileRepository : IRepository<Disease>
    {

        private readonly DbContext _context;
        //public FileRepository(IDataContext context)
        //{
        //    _context = context as DbContext;
        //}

        public Task Add(Disease entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Disease> All()
        {
            return new List<Disease>();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        void IRepository<Disease>.Add(Disease entity)
        {
            throw new NotImplementedException();
        }
    }
}
