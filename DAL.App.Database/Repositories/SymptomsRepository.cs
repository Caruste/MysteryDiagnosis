﻿using DAL.App.Interfaces.Interfaces;
using DAL.Repository;
using Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.App.Database.Repositories
{
    public class SymptomsRepository : EFRepository<Symptom>, ISymptomsRepository
    {
        public SymptomsRepository(DbContext dbContext) : base(dbContext)
        {
        }
        public bool Exists(Symptom symptom)
        {
            return repoDbSet.Where(x => x.Name == symptom.Name).Any();
        }
        public Symptom FindByName(Symptom symptom)
        {
            return repoDbSet.Where(x => x.Name == symptom.Name).FirstOrDefault();
        }
    }
}
