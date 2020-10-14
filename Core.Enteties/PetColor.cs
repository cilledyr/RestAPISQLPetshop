using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop.Core.Enteties
{
    public class PetColor
    {
        public int PetColorId { get; set; }
        public string PetColorName { get; set; }

        public List<PetColorPet> ColoredPets { get; set; }
    }
}
