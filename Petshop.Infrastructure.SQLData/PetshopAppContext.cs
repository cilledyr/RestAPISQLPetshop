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

            modelBuilder.Entity<PetColorPet>()
                .HasKey(pc => new { pc.petColorId, pc.PetId });
            modelBuilder.Entity<PetColorPet>()
                .HasOne(pc => pc.petColor)
                .WithMany(c => c.ColoredPets)
                .HasForeignKey(pc => pc.petColorId);
            modelBuilder.Entity<PetColorPet>()
                .HasOne(pc => pc.Pet)
                .WithMany(p => p.PetColor)
                .HasForeignKey(pc => pc.PetId);
            
        }
        public DbSet<PetColor> PetColors { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetColorPet> PetColorPets { get; set; }
        public DbSet<PetType> PetTypes { get; set; }
        public DbSet<Owner> Owners { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
