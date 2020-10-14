using Petshop.Core.Enteties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop.Core.ApplicationService
{
    public interface IPetColorService
    {
        public List<PetColor> GetAllPetColors();
        public PetColor FindPetColorByIdWithPets(int id);
        public PetColor FindPetColorById(int id);
        public List<PetColor> FindPetColorByName(string name);
        public PetColor AddNewPetColor(PetColor theNewColor);
        public PetColor UpdatePetColor(PetColor theUpdatedColor);
        public PetColor DeletePetColor(int Id);
        public List<Pet> FindAllPetsByColor(PetColor theColor);
        public List<PetColor> SearchPetColor(FilterModel filter);
        public List<PetColor> GetAllFilteredPetColors(FilterModel filter); 
    }
}
