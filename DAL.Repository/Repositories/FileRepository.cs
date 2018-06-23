using DAL.App.Database;
using DAL.App.Interfaces;
using DAL.Interfaces.Interfaces;
using Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository.Repositories
{
    public class FileRepository : IRepository<Disease>
    {

        private readonly DbContext _context;
        public FileRepository(IDataContext context)
        {
            _context = context as DbContext;
        }
        public IEnumerable<Disease> All()
        {
            return _context.All();
        }
    }
}
