using Petshop.Core.DomainService;
using Petshop.Core.Enteties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Petshop.Core.ApplicationService.Impl
{
    public class PetColorService : IPetColorService
    {
        private IPetColorRepository _petColorRepo;

        public PetColorService(IPetColorRepository petColorRepository)
        {
            _petColorRepo = petColorRepository;
        }

        public PetColor AddNewPetColor(PetColor theNewColor)
        {
            return _petColorRepo.AddNewPetColor(theNewColor);
        }

        public PetColor DeletePetColor(int Id)
        {
            var toBeDeletedColor = FindPetColorById(Id);
            if(toBeDeletedColor == null)
            {
                throw new Exception(message: "Id not found");
            }
            return _petColorRepo.DeletePetColor(toBeDeletedColor);
        }

        public List<Pet> FindAllPetsByColor(PetColor theColor)
        {
            return _petColorRepo.FindAllPetsByColor(theColor);
        }

        public PetColor FindPetColorById(int id)
        {
            List<PetColor> petColors = _petColorRepo.FindPetColorById(id);
            if(petColors.Count != 1)
            {
                return null;
            }
            else
            {
                return petColors[0];
            }
        }

        public PetColor FindPetColorByIdWithPets(int id)
        {
            List<PetColor> theColors =  _petColorRepo.FindPetColorByIdWithPets(id);
            if(theColors.Count != 1)
            {
                return null;
            }
            else
            {
                return theColors[0];
            }
        }

        public List<PetColor> FindPetColorByName(string name)
        {
            return _petColorRepo.FindPetColorByName(name);
        }

        public List<PetColor> GetAllFilteredPetColors(FilterModel filter)
        {
            return _petColorRepo.GetAllPetColors(filter);
        }

        public List<PetColor> GetAllPetColors()
        {
            return _petColorRepo.GetAllPetColors();
        }

        public List<PetColor> SearchPetColor(FilterModel filter)
        {
            string searchTerm = filter.SearchTerm.ToLower();
            switch (searchTerm)
            {
                case "id":
                    int theId;
                    if(int.TryParse(filter.SearchValue, out theId))
                    {
                        return new List<PetColor> { FindPetColorById(theId) };
                    }
                    else
                    {
                        throw new Exception(message: "You have not entered a valid id.");
                    }
                case "name":
                    return _petColorRepo.FindPetColorByName(filter.SearchValue, filter);
                default:
                    throw new Exception(message: "Search Term out of bounds.");
            }
        }

        public PetColor UpdatePetColor(PetColor theUpdatedColor)
        {
            return _petColorRepo.UpdatePetColor(theUpdatedColor);
        }
    }
}
