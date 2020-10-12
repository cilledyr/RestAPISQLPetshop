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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pet>()
                .HasOne(p => p.PetOwner)
                .WithMany(o => o.OwnerPets)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Pet>()
                .HasOne(p => p.PetType)
                .WithMany(t => t.PetTypePets)
                .OnDelete(DeleteBehavior.SetNull);
        }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetType> PetTypes { get; set; }
        public DbSet<Owner> Owners { get; set; }
    }
}
