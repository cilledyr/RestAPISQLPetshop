using Petshop.Core.DomainService;
using Petshop.Core.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Petshop.Infrastructure.Data.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        readonly PetshopAppContext _ctx;

        public OwnerRepository(PetshopAppContext ctx)
        {
            _ctx = ctx;
        }
        public Owner AddNewOwner(Owner theNewOwner)
        {
            var theOwner = _ctx.Owners.Add(theNewOwner).Entity;
            _ctx.SaveChanges();
            return theOwner;
        }

        public Owner DeleteOwner(Owner toBeDeletedOwner)
        {
            var petsToRemove = _ctx.Pets.Where(p => p.PetOwner == toBeDeletedOwner);
            _ctx.RemoveRange(petsToRemove);
            var deletedOwner = _ctx.Owners.Remove(toBeDeletedOwner).Entity;
            _ctx.SaveChanges();
            return deletedOwner;
        }

        public List<Pet> FindAllPetsByOwner(Owner theOwner, FilterModel filter)
        {
            if(filter == null || filter.ItemsPrPage == 0 || filter.CurrentPage == 0)
            {
                return _ctx.Pets.Where(p => p.PetOwner == theOwner).ToList();
            }

            return _ctx.Pets.Where(p => p.PetOwner == theOwner)
                            .Skip((filter.CurrentPage -1) * filter.ItemsPrPage)
                            .Take(filter.ItemsPrPage)
                            .ToList();
        }

        public List<Owner> FindOwner(int theOwnerId)
        {
            return _ctx.Owners.Where(o => o.OwnerId == theOwnerId).ToList();
        }

        public IEnumerable<Owner> FindOwnerByAddress(string searchValue)
        {
            return _ctx.Owners.Where(o => o.OwnerAddress.ToLower().Contains(searchValue.ToLower()));
        }

        public IEnumerable<Owner> FindOwnerByEmail(string searchValue)
        {
            return _ctx.Owners.Where(o => o.OwnerEmail.ToLower().Equals(searchValue.ToLower()));
        }

        public List<Owner> FindOwnerByID(int searchId)
        {
            return _ctx.Owners.Where(o => o.OwnerId == searchId).ToList();
        }

        public IEnumerable<Owner> FindOwnerByName(string searchValue)
        {
            return _ctx.Owners.Where(o => o.OwnerFirstName.ToLower().Contains(searchValue.ToLower()) || o.OwnerLastName.ToLower().Contains(searchValue.ToLower()));
        }

        public IEnumerable<Owner> FindOwnerByPhonenr(string searchValue)
        {
            return _ctx.Owners.Where(o => o.OwnerPhoneNr.ToLower().Contains(searchValue.ToLower()));
        }

        public IEnumerable<Owner> GetAllOwners()
        {
            return _ctx.Owners;
        }

        public Owner UpdateAddressOfOwner(Owner updatedOwner, string updateValue)
        {
            throw new NotImplementedException();
        }

        public Owner UpdateEmailOfOwner(Owner updatedOwner, string updateValue)
        {
            throw new NotImplementedException();
        }

        public Owner UpdateFirstNameOfOwner(Owner updatedOwner, string updateValue)
        {
            throw new NotImplementedException();
        }

        public Owner UpdateFullOwner(Owner theNewOwner, Owner theOldOwner)
        {
            var updatedOwner = _ctx.Update(theNewOwner).Entity;
            _ctx.SaveChanges();
            return updatedOwner;
        }

        public Owner UpdateLastNameOfOwner(Owner updatedOwner, string updateValue)
        {
            throw new NotImplementedException();
        }

        public Owner UpdatePhoneNrOfOwner(Owner updatedOwner, string updateValue)
        {
            throw new NotImplementedException();
        }
    }
}
