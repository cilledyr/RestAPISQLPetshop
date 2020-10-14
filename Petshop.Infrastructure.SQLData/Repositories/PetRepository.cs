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
            return _ctx.Pets.Include(p => p.PetType).Include(p => p.PetOwner).Where(p => p.PetId == theId).Include(p => p.PetColor).ToList();
        }

        public IEnumerable<Pet> FindPetsByColor(FilterModel filter)
        {
            IEnumerable<PetColorPet> theColors = _ctx.PetColorPets.Where(cp => cp.petColor.PetColorName.ToLower().Contains(filter.SearchValue.ToLower()));

            IEnumerable<Pet> thePets = new List<Pet>();
            foreach(var color in theColors)
            {
                
                    thePets = thePets.Concat(new List<Pet> { color.Pet });
                            }
            if(filter.CurrentPage == 0  || filter.ItemsPrPage == 0)
            {
                return thePets;
            }
            else
            {
                thePets.Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                   .Take(filter.ItemsPrPage);

                if (string.IsNullOrEmpty(filter.SortOrder))
                {
                    return thePets;
                }
                else if (filter.SortOrder.ToLower().Equals("desc"))
                {
                    return thePets.OrderByDescending(p => p.PetColor.Count()) ;
                }
                else
                {
                    return thePets.OrderBy(p => p.PetColor.Count());
                }
            }
            
        }

        public IEnumerable<Pet> FindPetsByName(string name, FilterModel filter)
        {
            if(filter == null)
            {
                return _ctx.Pets.Where(p => p.PetName.ToLower().Contains(name.ToLower()));
            }

            IEnumerable<Pet> thePets = _ctx.Pets.Where(p => p.PetName.ToLower().Contains(name.ToLower()))
                                                .Skip((filter.CurrentPage -1) * filter.ItemsPrPage)
                                                .Take(filter.ItemsPrPage);
            if (string.IsNullOrEmpty(filter.SortOrder))
            {
                return thePets;
            }
            else if (filter.SortOrder.ToLower().Equals("desc"))
            {
                return thePets.OrderByDescending(p => p.PetName);
            }
            else
            {
                return thePets.OrderBy(p => p.PetName);
            }
        }

        public IEnumerable<Pet> FindPetsByPreviousOwner(string searchValue, FilterModel filter)
        {
            if(filter.CurrentPage == 0|| filter.ItemsPrPage == 0)
            {
                return _ctx.Pets.Where(p => p.PetPreviousOwner.ToLower().Contains(searchValue.ToLower()));
            }
            IEnumerable<Pet> thePets = _ctx.Pets.Where(p => p.PetPreviousOwner.ToLower().Contains(searchValue.ToLower()))
                                                .Skip((filter.CurrentPage -1) * filter.ItemsPrPage)
                                                .Take(filter.ItemsPrPage);
            if (string.IsNullOrEmpty(filter.SortOrder))
            {
                return thePets;
            }
            else if (filter.SortOrder.ToLower().Equals("desc"))
            {
                return thePets.OrderByDescending(p => p.PetPreviousOwner);
            }
            else
            {
                return thePets.OrderBy(p => p.PetPreviousOwner);
            }
        }

        public IEnumerable<Pet> FindPetsByPrice(long thePriceValue, FilterModel filter)
        {
            if(filter.CurrentPage == 0 || filter.ItemsPrPage == 0)
            {
                return _ctx.Pets.Where(pet => pet.PetPrice <= thePriceValue - 10 && pet.PetPrice <= thePriceValue + 10);
            }
            IEnumerable<Pet> thePets = _ctx.Pets.Where(pet => pet.PetPrice <= thePriceValue - 10 && pet.PetPrice <= thePriceValue + 10)
                                                .Skip((filter.CurrentPage -1) * filter.ItemsPrPage)
                                                .Take(filter.ItemsPrPage);

            if (string.IsNullOrEmpty(filter.SortOrder))
            {
                return thePets;
            }
            else if (filter.SortOrder.ToLower().Equals("desc"))
            {
                return thePets.OrderByDescending(p => p.PetPrice);
            }
            else
            {
                return thePets.OrderBy(p => p.PetPrice);
            }
        }

        public IEnumerable<Pet> FindPetsBySoldDate(DateTime theSoldValue, FilterModel filter)
        {
            if(filter.CurrentPage == 0 || filter.ItemsPrPage == 0)
            {
                return _ctx.Pets.Where(pet => pet.PetSoldDate.Year == theSoldValue.Year);
            }
            IEnumerable<Pet> thePets = _ctx.Pets.Where(pet => pet.PetSoldDate.Year == theSoldValue.Year)
                                                .Skip((filter.CurrentPage -1) * filter.ItemsPrPage)
                                                .Take(filter.ItemsPrPage);
            if (string.IsNullOrEmpty(filter.SortOrder))
            {
                return thePets;
            }
            else if (filter.SortOrder.ToLower().Equals("desc"))
            {
                return thePets.OrderByDescending(p => p.PetSoldDate);
            }
            else
            {
                return thePets.OrderBy(p => p.PetSoldDate);
            }
        }

        public IEnumerable<Pet> GetAllPets(FilterModel filter)
        {
            if(filter == null)
            {
                return _ctx.Pets;
            }
            IEnumerable<Pet> thePets = _ctx.Pets
                                            .Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                                            .Take(filter.ItemsPrPage);
            if(string.IsNullOrEmpty(filter.SortOrder))
            {
                return thePets;
            }
            else if(filter.SortOrder.ToLower().Equals("desc"))
            {
                return thePets.OrderByDescending(p => p.PetId);
            }
            else
            {
                return thePets.OrderBy(p => p.PetId);
            }
        }

        public IEnumerable<Pet> GetSortedPets()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pet> SearchPetsByBirthYear(DateTime theDateValue, FilterModel filter)
        {
            if(filter.CurrentPage == 0 || filter.ItemsPrPage == 0 )
            {
                return _ctx.Pets.Where(pet => pet.PetBirthday.Year == theDateValue.Year);
            }
            IEnumerable<Pet> thePets = _ctx.Pets.Where(pet => pet.PetBirthday.Year == theDateValue.Year)
                                                .Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                                                .Take(filter.ItemsPrPage);
            if (string.IsNullOrEmpty(filter.SortOrder))
            {
                return thePets;
            }
            else if (filter.SortOrder.ToLower().Equals("desc"))
            {
                return thePets.OrderByDescending(p => p.PetBirthday);
            }
            else
            {
                return thePets.OrderBy(p => p.PetBirthday);
            }
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
