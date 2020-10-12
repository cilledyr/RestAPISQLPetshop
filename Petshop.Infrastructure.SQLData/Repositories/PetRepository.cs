using Microsoft.EntityFrameworkCore;
using Petshop.Core.DomainService;
using Petshop.Core.Enteties;
using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
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
            _ctx.Attach(theNewPet.PetType);
            _ctx.Attach(theNewPet.PetOwner);
            var thePet = _ctx.Pets.Add(theNewPet).Entity;
            _ctx.SaveChanges();
            return thePet;
        }

        public Pet DeletePet(Pet toBeDeletedPet)
        {
            Pet deletedPet = _ctx.Pets.Remove(toBeDeletedPet).Entity;
            _ctx.SaveChanges();
            return deletedPet;
        }

        public List<Pet> FindPetByID(int theId)
        {
            return _ctx.Pets.Include(p => p.PetType).Include(p => p.PetOwner).Where(p => p.PetId == theId).ToList();
        }

        public IEnumerable<Pet> FindPetsByColor(FilterModel filter)
        {
            return _ctx.Pets.Where(p => p.PetColor.ToLower().Contains(filter.SearchValue.ToLower()))
                            .Skip((filter.CurrentPage -1) * filter.ItemsPrPage)
                            .Take(filter.ItemsPrPage);
        }

        public IEnumerable<Pet> FindPetsByName(string name, FilterModel filter)
        {
            if(filter == null)
            {
                return _ctx.Pets.Where(p => p.PetName.ToLower().Contains(name.ToLower()));
            }

            return _ctx.Pets.Where(p => p.PetName.ToLower().Contains(name.ToLower()))
                            .Skip((filter.CurrentPage -1) * filter.ItemsPrPage)
                            .Take(filter.ItemsPrPage);
        }

        public IEnumerable<Pet> FindPetsByPreviousOwner(string searchValue, FilterModel filter)
        {
            if(filter.CurrentPage == 0|| filter.ItemsPrPage == 0)
            {
                return _ctx.Pets.Where(p => p.PetPreviousOwner.ToLower().Contains(searchValue.ToLower()));
            }
            return _ctx.Pets.Where(p => p.PetPreviousOwner.ToLower().Contains(searchValue.ToLower()))
                            .Skip((filter.CurrentPage -1) * filter.ItemsPrPage)
                            .Take(filter.ItemsPrPage);
        }

        public IEnumerable<Pet> FindPetsByPrice(long thePriceValue, FilterModel filter)
        {
            if(filter.CurrentPage == 0 || filter.ItemsPrPage == 0)
            {
                return _ctx.Pets.Where(pet => pet.PetPrice <= thePriceValue - 10 && pet.PetPrice <= thePriceValue + 10);
            }
            return _ctx.Pets.Where(pet => pet.PetPrice <= thePriceValue - 10 && pet.PetPrice <= thePriceValue + 10)
                            .Skip((filter.CurrentPage -1) * filter.ItemsPrPage)
                            .Take(filter.ItemsPrPage);
        }

        public IEnumerable<Pet> FindPetsBySoldDate(DateTime theSoldValue, FilterModel filter)
        {
            if(filter.CurrentPage == 0 || filter.ItemsPrPage == 0)
            {
                return _ctx.Pets.Where(pet => pet.PetSoldDate.Year == theSoldValue.Year);
            }
            return _ctx.Pets.Where(pet => pet.PetSoldDate.Year == theSoldValue.Year)
                            .Skip((filter.CurrentPage -1) * filter.ItemsPrPage)
                            .Take(filter.ItemsPrPage);
        }

        public IEnumerable<Pet> GetAllPets(FilterModel filter)
        {
            if(filter == null)
            {
                return _ctx.Pets;
            }
            return _ctx.Pets
                .Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                .Take(filter.ItemsPrPage);
        }

        public IEnumerable<Pet> GetSortedPets()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pet> SearchPetsByBirthYear(DateTime theDateValue, FilterModel filter)
        {
            if(filter.CurrentPage == 0 || filter.ItemsPrPage == 0)
            {
                return _ctx.Pets.Where(pet => pet.PetBirthday.Year == theDateValue.Year);
            }
            return _ctx.Pets.Where(pet => pet.PetBirthday.Year == theDateValue.Year)
                            .Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                            .Take(filter.ItemsPrPage);
        }

        public Pet UpdateBirthdayOfPet(Pet updatedPet, DateTime updateValue)
        {
            throw new NotImplementedException();
        }

        public Pet UpdateColorOfPet(Pet updatedPet, string updateValue)
        {
            throw new NotImplementedException();
        }

        public Pet UpdateFullPet(Pet theNewPet)
        {
            _ctx.Attach(theNewPet.PetOwner);
            _ctx.Attach(theNewPet.PetType);
            var updatedPet = _ctx.Update(theNewPet).Entity;
            _ctx.SaveChanges();
            return updatedPet;
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
