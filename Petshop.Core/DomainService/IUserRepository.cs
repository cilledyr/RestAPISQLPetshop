using Petshop.Core.Enteties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop.Core.DomainService
{
    public interface IUserRepository
    {
        public List<User> GetUsers(FilterModel filter = null);
        public User AddUser(User newUser);
        public User EditUser(User alteredUser);
        public User DeleteUser(int id);

        public User FindUserById(int id);
        public List<User> FindUserByName(string name);
    }
}
