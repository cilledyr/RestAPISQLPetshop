using Microsoft.EntityFrameworkCore;
using Petshop.Core.Enteties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop.Infrastructure.Data
{
    public class PetshopAppContext: DbContext
    {
        public PetshopAppContext(DbContextOptions<PetshopAppContext> opt):base(opt)
        {
            
        }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetType> PetTypes { get; set; }
        public DbSet<Owner> Owners { get; set; }
    }
}
