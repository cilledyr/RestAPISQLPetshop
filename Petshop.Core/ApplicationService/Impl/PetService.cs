﻿using Petshop.Core.DomainService;
using Petshop.Core.Enteties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Petshop.Core.ApplicationService.Impl
{
    public class PetService : IPetService
    {

        private IPetRepository _petRepo;
        private IOwnerRepository _ownerRepo;
        private IPetTypeRepository _petTypeRepo;
        private IPetColorRepository _petColorRepo;
        public PetService(IPetRepository petRepository, IOwnerRepository ownerRepository, IPetTypeRepository petTypeRepository, IPetColorRepository petColorRepository)
        {
            _petRepo = petRepository;
            _ownerRepo = ownerRepository;
            _petTypeRepo = petTypeRepository;
            _petColorRepo = petColorRepository;
        }


        // Adds a new pet, Owner, Color and Type are added unless their ID is given, in which case they are found by Id.
        public Pet AddNewPet(Pet theNewPet)
        {
            
            List<PetType> theType = null;
            if(theNewPet.PetType.PetTypeId != 0)
            {
                theType = _petTypeRepo.FindPetTypeById(theNewPet.PetType.PetTypeId);
                if(theType.Count != 1)
                {
                    throw new Exception("Sorry can't find that type.");
                }
            }
            else
            {
                theType = new List<PetType> { _petTypeRepo.AddNewPetType(theNewPet.PetType) };
            }
            
            if(theType.Count != 1)
            {
                throw new Exception(message: "Could not find the type.");
            }
            else
            {
                theNewPet.PetType = theType[0];
            }

            List<Owner> theOwners = null;
            if (theNewPet.PetOwner.OwnerId == 0)
            {
                theOwners =  new List<Owner> { _ownerRepo.AddNewOwner(theNewPet.PetOwner) };
            }
            else
            {
                theOwners = _ownerRepo.FindOwnerByID(theNewPet.PetOwner.OwnerId);
            }

            if(theOwners.Count != 1)
            {
                throw new Exception(message: "Could not find the right owner");
            }
            else
            {
                theNewPet.PetOwner = theOwners[0];
            }

            List<PetColor> theColors = null;
            foreach (var color in theNewPet.PetColor)
            {
                PetColor theColor;
                if (color.petColorId != 0)
                {
                    var allColors = _petColorRepo.FindPetColorById(color.petColorId);
                    if (allColors.Count() != 1)
                    {
                        throw new Exception(message: "Wrong nr of id's found");
                    }
                    else
                    {
                        theColor = allColors[0];
                    }
                }
                else
                {
                    theColor = _petColorRepo.AddNewPetColor(color.petColor);
                }
                if (theColors == null)
                {
                    theColors = new List<PetColor> { theColor };
                }
                else
                {
                    theColors.Add(theColor);
                }
            }
            List<PetColorPet> thePetColorPets = new List<PetColorPet>();
            foreach(var color in theColors)
            {
                thePetColorPets.Add(new PetColorPet { petColor = color });
            }
            theNewPet.PetColor = thePetColorPets;

            return  _petRepo.AddNewPet(theNewPet);

            
        }



        public Pet DeletePetByID(int theId)
        {
            List<Pet> toBeDeletedPets = _petRepo.FindPetByID(theId);
            if(toBeDeletedPets.Count != 1 || toBeDeletedPets[0] == null)
            {
                throw new InvalidDataException(message: "Could not find that pet Id to delete.");
            }
            return _petRepo.DeletePet(toBeDeletedPets[0]);
        }



        public Pet FindPetByID(int theNewId)
        {
            List<Pet> thePets = _petRepo.FindPetByID(theNewId);
            if (thePets.Count != 1)
            {
                return null;
            }
            else
            {
                return thePets[0];

            }
        }

        public List<Pet> FindPetsByName(string theName)
        {
            return _petRepo.FindPetsByName(theName).ToList();
        }

        public List<Pet> GetAllFilteredPets(FilterModel filter)
        {
            return _petRepo.GetAllPets(filter).ToList();
        }

        public List<Pet> GetAllPets()
        {
            return _petRepo.GetAllPets().ToList();
            
        }

        public List<Pet> GetSortedPets()
        {
            return _petRepo.GetSortedPets().ToList();
        }


        //Searches for pets, for owner and type both id, and name are valid searches.
        public List<Pet> SearchForPet(FilterModel filter)
        {
            string searchValue = filter.SearchValue.ToLower();
            string toSearchString = filter.SearchTerm.ToLower();
            switch (toSearchString)
            {
                case "name":
                    return _petRepo.FindPetsByName(searchValue, filter).ToList();
                case "color":
                    return _petRepo.FindPetsByColor(filter).ToList();
                case "type":
                    int theSearch;
                    List<PetType> thePetType = null;
                    if (int.TryParse(searchValue, out theSearch) && theSearch != 0)
                    {
                        thePetType = _petTypeRepo.FindPetTypeById(theSearch);
                        if(thePetType.Count < 1 )
                        {
                            throw new Exception(message: "Sorry could not find id of that PetType.");
                        }
                        else
                        {
                            return _petTypeRepo.FindAllPetsByType(thePetType[0], filter);
                        }

                    }
                    else
                    {
                        thePetType = _petTypeRepo.FindPetTypeByName(searchValue);
                        if (thePetType.Count <  1)
                        {
                            throw new Exception(message: "Sorry could not find name of that PetType.");
                        }
                        else
                        {
                            IEnumerable<Pet> allPetsByType = null;
                            foreach (var petType in thePetType)
                            {
                                if(allPetsByType == null)
                                {
                                    allPetsByType = _petTypeRepo.FindAllPetsByType(petType);
                                }
                                else
                                {
                                    allPetsByType = allPetsByType.Concat(_petTypeRepo.FindAllPetsByType(petType));
                                }
                            }
                            if(filter.CurrentPage != 0 && filter.ItemsPrPage != 0)
                            {
                                allPetsByType = allPetsByType.Skip((filter.CurrentPage - 1) * filter.ItemsPrPage).Take(filter.ItemsPrPage);
                            }
                            return allPetsByType.ToList();
                        }
                    }

                case "birthday":
                    DateTime theDateValue = DateTime.Now;
                    if (DateTime.TryParse(searchValue, out theDateValue))
                    {
                        return _petRepo.SearchPetsByBirthYear(theDateValue, filter).ToList() ;
                    }
                    else
                    {
                        throw new InvalidDataException(message: "You have not entered a valid date.");
                    }

                case "solddate":
                    DateTime theSoldValue = DateTime.Now;
                    if (DateTime.TryParse(searchValue, out theSoldValue))
                    {
                        return _petRepo.FindPetsBySoldDate(theSoldValue, filter).ToList() ;
                    }
                    else
                    {
                        throw new InvalidDataException(message: "You have not entered a valid date.");
                    }
                case "previousowner":
                    return _petRepo.FindPetsByPreviousOwner(searchValue, filter).ToList();

                case "owner":
                    int theSearchForOwner;
                    List<Owner> thePetOwner = null;
                    if (int.TryParse(searchValue, out theSearchForOwner) && theSearchForOwner != 0)
                    {
                        thePetOwner = _ownerRepo.FindOwnerByID(theSearchForOwner);
                        if (thePetOwner.Count < 1 || thePetOwner == null)
                        {
                            throw new Exception(message: "Sorry could not find id of that PetType.");
                        }
                        else
                        {
                            return _ownerRepo.FindAllPetsByOwner(thePetOwner[0], filter);
                        }

                    }
                    else
                    {
                        thePetOwner = _ownerRepo.FindOwnerByName(searchValue).ToList();
                        if (thePetOwner.Count < 1 || thePetOwner == null)
                        {
                            throw new Exception(message: "Sorry could not find name of that Owner.");
                        }
                        else
                        {
                            IEnumerable<Pet> allPetsByTheOwners = null;
                            foreach (var owner in thePetOwner)
                            {
                                if(allPetsByTheOwners == null)
                                {
                                    allPetsByTheOwners = _ownerRepo.FindAllPetsByOwner(owner);
                                }
                                else
                                {
                                    allPetsByTheOwners = allPetsByTheOwners.Concat(_ownerRepo.FindAllPetsByOwner(owner));
                                }
                            }
                            if (filter.CurrentPage != 0 && filter.ItemsPrPage != 0)
                            {
                                allPetsByTheOwners = allPetsByTheOwners.Skip((filter.CurrentPage - 1) * filter.ItemsPrPage).Take(filter.ItemsPrPage);
                            }
                            return allPetsByTheOwners.ToList();
                        }
                    }
                case "price":
                    long thePriceValue = 0;
                    if (long.TryParse(searchValue, out thePriceValue))
                    {
                        return _petRepo.FindPetsByPrice(thePriceValue, filter).ToList();
                    }
                    else
                    {
                        throw new InvalidDataException(message: "You have not entered a valid price.");
                    }
                case "id":
                    int searchId;
                    if(int.TryParse(searchValue, out searchId) && searchId!=0)
                    {
                        return _petRepo.FindPetByID(searchId);
                    }
                    else
                    {
                        throw new InvalidDataException(message: "Id out of bounds.");
                    }
                    

                default:
                    throw new InvalidDataException(message: "I don't have that property to seach for.");
            }
        }


        //Updates pet, for type and owner, if Id is set to 0, a new one wil be created from the entered values. From values entered, instead of a whole pet.
        public Pet UpdatePet(int updatePetId, UpdateModel update)
        {
            int updateParam = update.UpdateParam.Value;
            string updateValue = update.UpdateValue;
            Pet updatedPet = FindPetByID(updatePetId);
            if(updatedPet == null)
            {
                throw new Exception(message: "I am sorry could not find that pet to Update.");
            }
            else
            {
                switch (updateParam)
                {
                    case 1:
                        return _petRepo.UpdateNameOfPet(updatedPet, updateValue);
                    case 2:
                        return _petRepo.UpdateColorOfPet(updatedPet, updateValue);
                    case 3:
                        int petTypeId;
                        List<PetType> updatedType = null;
                        if (int.TryParse(updateValue, out petTypeId))
                        {
                            updatedType = _petTypeRepo.FindPetTypeById(petTypeId);
                        }
                        else
                        {
                            updatedType = _petTypeRepo.FindPetTypeByName(updateValue);
                        }
                        if (updatedType.Count != 1)
                        {
                            throw new Exception("Can't find a PetType of that variety");
                        }
                        else
                        {
                            return _petRepo.UpdateTypeOfPet(updatedPet, updatedType[0]);
                        }

                    case 4:
                        DateTime theUpdateValue = DateTime.Now;
                        if (DateTime.TryParse(updateValue, out theUpdateValue))
                        {
                            return _petRepo.UpdateBirthdayOfPet(updatedPet, theUpdateValue);
                        }
                        else
                        {
                            throw new InvalidDataException(message: "You have not entered a valid date.");
                        }

                    case 5:
                        DateTime theSoldUpdateValue = DateTime.Now;
                        if (DateTime.TryParse(updateValue, out theSoldUpdateValue))
                        {
                            return _petRepo.UpdateSoldDateOfPet(updatedPet, theSoldUpdateValue);
                        }
                        else
                        {
                            throw new InvalidDataException(message: "You have not entered a valid date.");
                        }
                    case 6:
                        return _petRepo.UpdatePreviousOwnerOfPet(updatedPet, updateValue);
                    case 7:
                        long thePriceValue = 0;
                        if (long.TryParse(updateValue, out thePriceValue))
                        {
                            return _petRepo.UpdatePriceOfPet(updatedPet, thePriceValue);
                        }
                        else
                        {
                            throw new InvalidDataException(message: "You have not entered a valid price.");
                        }
                    case 8:
                        int id;
                        if (int.TryParse(updateValue, out id))
                        {
                            List<Owner> allTheOwners = _ownerRepo.GetAllOwners().ToList();
                            if (id != 0)
                            {
                                List<Owner> newOwners = _ownerRepo.FindOwner(id);
                                if (newOwners.Count != 1 || newOwners[0] == null)
                                {
                                    throw new Exception(message: "Could not find an owner with that Id.");
                                }
                                else
                                {
                                    return _petRepo.UpdateOwnerOfPet(updatedPet, newOwners[0]);
                                }

                            }
                            else
                            {
                                throw new InvalidDataException(message: "Id of owner out of Bounds.");
                            }
                        }
                        else
                        {
                            throw new InvalidDataException(message: "You have not entered a valid id.");
                        }
                    default:
                        throw new InvalidDataException(message: "I am sorry i do not have that parameter to update.");
                }
            }

            
        }
        //Updates pet, for type and owner, if Id is set to 0, a new one wil be created from the entered values. Takes a whole pet, returns the upated pet.
        public Pet UpdatePet(Pet thePet)
        {
            /*List<Pet> thePets = _petRepo.FindPetByID(thePet.PetId);
            if (thePets.Count !=1 || thePets[0] == null)
            {
                throw new Exception(message: "I am sorry could not find pet to update.");
            }
            else
            {*/
                if(thePet.PetOwner.OwnerId == 0)
                {
                    thePet.PetOwner = _ownerRepo.AddNewOwner(thePet.PetOwner);
                }
                else
                {
                    List<Owner> theOwners = _ownerRepo.FindOwner(thePet.PetOwner.OwnerId);
                    if(theOwners.Count != 1 || theOwners == null)
                    {
                        throw new Exception(message: "Sorry wrong number of owners found with that ID.");
                    }
                    else
                    {
                        thePet.PetOwner = theOwners[0];
                    }
                }
                List<PetType> theType = null;
                if (thePet.PetType.PetTypeId == 0)
                {
                    theType = _petTypeRepo.FindPetTypeByName(thePet.PetType.PetTypeName);
                }
                else
                {
                    theType = _petTypeRepo.FindPetTypeById(thePet.PetType.PetTypeId);
                }
                if(theType == null && !string.IsNullOrEmpty(thePet.PetType.PetTypeName))
                {
                    var newType = new PetType() { PetTypeName = thePet.PetType.PetTypeName };
                    thePet.PetType = _petTypeRepo.AddNewPetType(newType);
                }
                else if(theType.Count() == 1)
                {
                    thePet.PetType = theType[0];
                }
                else
                {
                    throw new Exception(message: "Too many petTypes with those parameters found.");
                }

                List<PetColor> theColors = null;
                foreach(var color in thePet.PetColor)
                {
                    PetColor theColor;
                    if(color.petColorId != 0)
                    {
                        var allColors = _petColorRepo.FindPetColorById(color.petColorId);
                        if(allColors.Count() != 1)
                        {
                            throw new Exception(message: "Wrong nr of id's found");
                        }
                        else
                        {
                            theColor = allColors[0];
                        }
                    }
                    else
                    {
                        theColor = _petColorRepo.AddNewPetColor(color.petColor);
                    }
                    if(theColors == null)
                    {
                        theColors = new List<PetColor> { theColor };
                    }
                    else
                    {
                        theColors.Add(theColor);
                    }
                }
                List<PetColorPet> thePetColorPets = new List<PetColorPet>();
                foreach (var color in theColors)
                {
                    thePetColorPets.Add(new PetColorPet { petColor = color });
                }
                thePet.PetColor = thePetColorPets;
                
                return _petRepo.UpdateFullPet(thePet);
        //    }
        }
    }
}
