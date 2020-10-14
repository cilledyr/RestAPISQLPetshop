using Microsoft.EntityFrameworkCore;
using Petshop.Core.DomainService;
using Petshop.Core.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Petshop.Infrastructure.Data.Repositories
{
    public class PetColorRepository : IPetColorRepository
    {
        readonly PetshopAppContext _ctx;

        public PetColorRepository(PetshopAppContext ctx)
        {
            _ctx = ctx;
        }
        public PetColor AddNewPetColor(PetColor theNewColor)
        {
            var theColor = _ctx.PetColors.Add(theNewColor).Entity;
            _ctx.SaveChanges();
            return theColor;
        }

        public PetColor DeletePetColor(PetColor toBeDeletedPetColor)
        {
            var deletedColor = _ctx.Remove(toBeDeletedPetColor).Entity;
            _ctx.SaveChanges();
            return deletedColor;
        }

        public List<Pet> FindAllPetsByColor(PetColor theColor, FilterModel filter = null)
        {
            List<PetColorPet> petColorPets = _ctx.PetColorPets.Where(cp => cp.petColor == theColor).ToList();
            IEnumerable < Pet > thePets = new List<Pet>();
            foreach(var colorPet in petColorPets)
            {
                thePets = thePets.Concat(new List<Pet> { colorPet.Pet });
            }
            if(filter == null)
            {
                return thePets.ToList();
            }
            else
            {
                thePets.Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                        .Take(filter.ItemsPrPage);

                if(string.IsNullOrEmpty(filter.SortOrder))
                {
                    return thePets.ToList();
                }
                else if(filter.SortOrder.ToLower().Equals("desc"))
                {
                    return thePets.OrderByDescending(p => p.PetName).ToList();
                }
                else
                {
                    return thePets.OrderBy(p => p.PetName).ToList();
                }
            }

        }

        public List<PetColor> FindPetColorById(int id)
        {
            return _ctx.PetColors.Where(c => c.PetColorId == id).ToList();
        }

        public List<PetColor> FindPetColorByIdWithPets(int id)
        {
            return _ctx.PetColors.Include(c => c.ColoredPets).Where(p => p.PetColorId == id).ToList();
        }

        public List<PetColor> FindPetColorByName(string name, FilterModel filter)
        {
            if(filter == null)
            {
                return _ctx.PetColors.Where(c => c.PetColorName.ToLower().Contains(name.ToLower())).ToList();
            }
            else
            {
                IEnumerable<PetColor> theColors = _ctx.PetColors.Where(c => c.PetColorName.ToLower().Contains(name.ToLower()))
                                                                .Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                                                                .Take(filter.ItemsPrPage);
                if (string.IsNullOrEmpty(filter.SortOrder))
                {
                    return theColors.ToList();
                }
                else if (filter.SortOrder.ToLower().Equals("desc"))
                {
                    return theColors.OrderByDescending(c => c.PetColorName).ToList();
                }
                else
                {
                    return theColors.OrderBy(c => c.PetColorName).ToList();
                }
            }
            
        }

        public List<PetColor> GetAllPetColors(FilterModel filter)
        {
            if(filter == null)
            {
                return _ctx.PetColors.ToList();
            }

            else
            {
                IEnumerable<PetColor> theColors = _ctx.PetColors
                                                .Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                                                .Take(filter.ItemsPrPage);
                if (string.IsNullOrEmpty(filter.SortOrder))
                {
                    return theColors.ToList();
                }
                else if (filter.SortOrder.ToLower().Equals("desc"))
                {
                    return theColors.OrderByDescending(c => c.PetColorName).ToList();
                }
                else
                {
                    return theColors.OrderBy(c => c.PetColorName).ToList();
                }
            }
        }

        public PetColor UpdatePetColor(PetColor theNewPetColor)
        {
            var updatedColor = _ctx.Update(theNewPetColor).Entity;
            _ctx.SaveChanges();
            return updatedColor;
        }
    }
}
