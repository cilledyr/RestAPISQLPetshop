using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop.Core.Enteties
{
    public class FilterModel
    {
        public string SearchTerm { get; set; }
        public string SearchValue { get; set; }

        public int CurrentPage { get; set; }
        public int ItemsPrPage { get; set; }
    }
}
