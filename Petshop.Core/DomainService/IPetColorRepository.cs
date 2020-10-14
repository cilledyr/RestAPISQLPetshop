using Petshop.Core.Enteties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop.Core.DomainService
{
    public interface IPetColorRepository
    {
        public List<PetColor> GetAllPetColors(FilterModel filter = null);
        public List<PetColor> FindPetColorById(int id);
        public List<PetColor> FindPetColorByName(string name, FilterModel filter = null);
        public PetColor AddNewPetColor(PetColor theNewColor);
        public PetColor UpdatePetColor(PetColor theNewPetColor);
        public PetColor DeletePetColor(PetColor toBeDeletedPetColor);
        public List<Pet> FindAllPetsByColor(PetColor theColor, FilterModel filter = null);

        public List<PetColor> FindPetColorByIdWithPets(int id);

    }
}
