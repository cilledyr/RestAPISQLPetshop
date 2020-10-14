using Petshop.Core.Enteties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop.Core.DomainService
{
    public interface IOwnerRepository
    {
        public IEnumerable<Owner> FindOwnerByName(string searchValue, FilterModel filter = null);
        public IEnumerable<Owner> FindOwnerByPhonenr(string searchValue, FilterModel filter);
        public IEnumerable<Owner> FindOwnerByAddress(string searchValue, FilterModel filter);
        public IEnumerable<Owner> FindOwnerByEmail(string searchValue, FilterModel filter);
        public List<Owner> FindOwnerByID(int searchId);
        public Owner UpdateFirstNameOfOwner(Owner updatedOwner, string updateValue);
        public Owner UpdateLastNameOfOwner(Owner updatedOwner, string updateValue);
        public Owner UpdateAddressOfOwner(Owner updatedOwner, string updateValue);
        public Owner UpdatePhoneNrOfOwner(Owner updatedOwner, string updateValue);
        public Owner UpdateEmailOfOwner(Owner updatedOwner, string updateValue);
        public Owner DeleteOwner(Owner toBeDeletedOwner);
        public List<Pet> FindAllPetsByOwner(Owner theOwner, FilterModel filter = null);
        public IEnumerable<Owner> GetAllOwners(FilterModel filter = null);
        public Owner AddNewOwner(Owner theNewOwner);

        public List<Owner> FindOwner(int theOwnerId);
        public Owner UpdateFullOwner(Owner theNewOwner, Owner theOldOwner);
    }
}
