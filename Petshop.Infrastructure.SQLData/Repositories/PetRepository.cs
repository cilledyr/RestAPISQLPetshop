using Petshop.Core.DomainService;
using Petshop.Core.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Petshop.Infrastructure.Data.Repositories
{
    public class PetRepository : IPetRepository
    {
        readonly PetshopAppContext _ctx;

        public PetRepository(PetshopAppContext ctx)
        {
            _ctx = ctx;
        }
        public Pet AddNewPet(Pet theNewPet)
        {
            var thePet = _ctx.Pets.Add(theNewPet).Entity;
            _ctx.SaveChanges();
            return thePet;
        }

        public Pet DeletePet(Pet toBeDeletedPet)
        {
            throw new NotImplementedException();
        }

        public List<Pet> FindPetByID(int theId)
        {
            return (List<Pet>)_ctx.Pets.Where(p => p.PetId == theId);
        }

        public IEnumerable<Pet> FindPetsByColor(string searchValue)
        {
            return _ctx.Pets.Where(p => p.PetColor.ToLower().Contains(searchValue.ToLower()));
        }

        public IEnumerable<Pet> FindPetsByName(string theName)
        {
            return _ctx.Pets.Where(p => p.PetName.ToLower().Contains(theName.ToLower()));
        }

        public IEnumerable<Pet> FindPetsByPreviousOwner(string searchValue)
        {
            return _ctx.Pets.Where(p => p.PetPreviousOwner.ToLower().Contains(searchValue.ToLower()));
        }

        public IEnumerable<Pet> FindPetsByPrice(long thePriceValue)
        {
            return _ctx.Pets.Where(pet => pet.PetPrice <= thePriceValue - 10 && pet.PetPrice <= thePriceValue + 10);
        }

        public IEnumerable<Pet> FindPetsBySoldDate(DateTime theSoldValue)
        {
            return _ctx.Pets.Where(pet => pet.PetSoldDate.Year == theSoldValue.Year);
        }

        public IEnumerable<Pet> GetAllPets()
        {
            return _ctx.Pets;
        }

        public IEnumerable<Pet> GetSortedPets()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pet> SearchPetsByBirthYear(DateTime theDateValue)
        {
            return _ctx.Pets.Where(pet => pet.PetBirthday.Year == theDateValue.Year);
        }

        public Pet UpdateBirthdayOfPet(Pet updatedPet, DateTime updateValue)
        {
            throw new NotImplementedException();
        }

        public Pet UpdateColorOfPet(Pet updatedPet, string updateValue)
        {
            throw new NotImplementedException();
        }

        public Pet UpdateFullPet(Pet theOldPet, Pet theNewPet)
        {
            throw new NotImplementedException();
        }

        public Pet UpdateNameOfPet(Pet updatedPet, string updateValue)
        {
            throw new NotImplementedException();
        }

        public Pet UpdateOwnerOfPet(Pet updatedPet, Owner newOwner)
        {
            throw new NotImplementedException();
        }

        public Pet UpdatePreviousOwnerOfPet(Pet updatedPet, string updateValue)
        {
            throw new NotImplementedException();
        }

        public Pet UpdatePriceOfPet(Pet updatedPet, long updateValue)
        {
            throw new NotImplementedException();
        }

        public Pet UpdateSoldDateOfPet(Pet updatedPet, DateTime updateValue)
        {
            throw new NotImplementedException();
        }

        public Pet UpdateTypeOfPet(Pet updatedPet, PetType updateValue)
        {
            throw new NotImplementedException();
        }
    }
}
