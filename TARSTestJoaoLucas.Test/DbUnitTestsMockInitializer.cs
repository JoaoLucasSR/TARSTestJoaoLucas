using System;
using Xunit;
using TARSTestJoaoLucas.Models;
using TARSTestJoaoLucas.Context;

namespace TARSTestJoaoLucas.Test
{
    public class DbUnitTestsMockInitializer
    {
        public DbUnitTestsMockInitializer()
        {
        }
        public void Seed(AppDbContext context)
        {
            context.Workers.Add(new Worker{Id = 1, Name="Pedro"});
            context.Workers.Add(new Worker{Id = 2, Name="Samuel"});
            context.Workers.Add(new Worker{Id = 3, Name="Marcos"});

            context.Projects.Add(new Project{Id = 1, Name="TARS T1", Description="TARS Test", WorkedHours=8, WorkerId=1});
            context.Projects.Add(new Project{Id = 2, Name="TARS T2", Description="TARS Test", WorkedHours=2, WorkerId=1});
            context.Projects.Add(new Project{Id = 3, Name="TARS T3", Description="TARS Test", WorkedHours=4, WorkerId=1});

            context.Projects.Add(new Project{Id = 4, Name="TARS T4", Description="TARS Test", WorkedHours=12, WorkerId=2});
            context.SaveChanges();
        }
    }
}
