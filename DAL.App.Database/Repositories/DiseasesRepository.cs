using DAL.App.Interfaces.Interfaces;
using Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.App.Database.Repositories
{
    public class DiseasesRepository : IDiseasesRepository
    {
        public IEnumerable<Disease> All()
        {
            throw new NotImplementedException();
        }
    }
}
