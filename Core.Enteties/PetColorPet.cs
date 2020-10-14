using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop.Core.Enteties
{
    public class PetColorPet
    {
        public int petColorId { get; set; }
        public PetColor petColor { get; set; }
        public int PetId { get; set; }
        public Pet Pet { get; set; }
    }
}
