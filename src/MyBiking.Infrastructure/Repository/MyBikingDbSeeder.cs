using Bogus;
using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
using MyBiking.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Infrastructure.Repository
{
    public class MyBikingDbSeeder
    {
        private readonly MyBikingDbContext _myBikingDbContext;

        public MyBikingDbSeeder(MyBikingDbContext myBikingDbContext)
        {
            this._myBikingDbContext = myBikingDbContext;
        }

        public void Seed()
        {
            //if(this._myBikingDbContext.Nationalities.Count()==0)
            //{
            //    SeedNationalities();
            //}
            //SeedRoles();
            //if (_myBikingDbContext.Database.CanConnect())
            //{
            //    _myBikingDbContext.Database.EnsureDeleted();
            //    //_myBikingDbContext.Database.EnsureCreated();
            //    var pendingMigrations = _myBikingDbContext.Database.GetPendingMigrations();
            //    if (pendingMigrations != null && pendingMigrations.Any())
            //    {
            //        _myBikingDbContext.Database.Migrate();
            //    }
            //}
        }

        //private void SeedNationalities()
        //{
        //    var faker = new Faker<Nationality>()
        //        .RuleFor(n => n.NationalityName, f => f.Address.Country())
        //        .Generate(150);

        //    faker = faker.OrderBy(p => p.NationalityName).ToList();
        //    _myBikingDbContext.Nationalities.AddRange(faker);
        //    _myBikingDbContext.SaveChanges();
        //}

        //private void SeedRoles()
        //{
        //    _myBikingDbContext.Roles.AddRange(new List<Role>(){
        //            new Role()
        //            {
        //                Name = "Admin"
        //            },
        //            new Role()
        //            {
        //                Name = "Standard"
        //            },
        //        });
        //    _myBikingDbContext.SaveChanges();
        //}
    }
}
