using Petshop.Core.DomainService;
using Petshop.Core.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Petshop.Infrastructure.Data.Repositories
{
    public class PetTypeRepository : IPetTypeRepository
    {
        readonly PetshopAppContext _ctx;

        public PetTypeRepository(PetshopAppContext ctx)
        {
            _ctx = ctx;
        }
        public PetType AddNewPetType(PetType theNewType)
        {
            var thePetType = _ctx.PetTypes.Add(theNewType).Entity;
            _ctx.SaveChanges();
            return thePetType;
        }

        public PetType DeletePetType(PetType toBeDeletedPetType)
        {
            throw new NotImplementedException();
        }

        public List<Pet> FindAllPetsByType(PetType theType)
        {
            throw new NotImplementedException();
        }

        public List<PetType> FindPetTypeById(int id)
        {
            return (List<PetType>)_ctx.PetTypes.Where(p => p.PetTypeId == id);
        }

        public List<PetType> FindPetTypeByName(string name)
        {
            return (List<PetType>)_ctx.PetTypes.Where(p => p.PetTypeName.ToLower().Contains(name.ToLower()));
        }

        public List<PetType> GetAllPetTypes()
        {
            return _ctx.PetTypes.ToList();
        }

        public PetType UpdatePetType(PetType theNewPetType, PetType theOldPetType)
        {
            throw new NotImplementedException();
        }
    }
}
