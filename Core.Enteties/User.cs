using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop.Core.Enteties
{
    public class User
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public bool UserIsAdmin { get; set; }
    }
}
