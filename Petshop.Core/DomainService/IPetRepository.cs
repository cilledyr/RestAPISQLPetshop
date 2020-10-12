using Petshop.Core.Enteties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop.Core.DomainService
{
    public interface IPetRepository
    {
        public IEnumerable<Pet> GetAllPets(FilterModel filter = null);
        public Pet AddNewPet(Pet theNewPet);
        public Pet DeletePet(Pet toBeDeletedPet);
        public IEnumerable<Pet> FindPetsByName(string name, FilterModel filter = null);
        public List<Pet> FindPetByID(int theId);
        public Pet UpdateNameOfPet(Pet updatedPet, string updateValue);
        public Pet UpdateColorOfPet(Pet updatedPet, string updateValue);
        public Pet UpdateTypeOfPet(Pet updatedPet, PetType updateValue);
        public Pet UpdateBirthdayOfPet(Pet updatedPet, DateTime updateValue);
        public Pet UpdateSoldDateOfPet(Pet updatedPet, DateTime updateValue);
        public Pet UpdatePreviousOwnerOfPet(Pet updatedPet, string updateValue);
 
        public Pet UpdatePriceOfPet(Pet updatedPet, long updateValue);
        public Pet UpdateFullPet(Pet theNewPet);
        public IEnumerable<Pet> GetSortedPets();
        public IEnumerable<Pet> FindPetsByColor(FilterModel filter);
        public IEnumerable<Pet> SearchPetsByBirthYear(DateTime theDateValue, FilterModel filter);
        public IEnumerable<Pet> FindPetsBySoldDate(DateTime theSoldValue, FilterModel filter);
        public IEnumerable<Pet> FindPetsByPreviousOwner(string searchValue, FilterModel filter);
        public IEnumerable<Pet> FindPetsByPrice(long thePriceValue, FilterModel filter);

        public Pet UpdateOwnerOfPet(Pet updatedPet, Owner newOwner);
    }
}
