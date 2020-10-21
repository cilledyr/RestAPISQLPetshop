using Petshop.Core.DomainService;
using Petshop.Core.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace Petshop.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        PetshopAppContext _ctx;

        public UserRepository(PetshopAppContext ctx)
        {
            _ctx = ctx;
        }
        public User AddUser(User newUser)
        {
            var theUser = _ctx.Add(newUser).Entity;
            _ctx.SaveChanges();
            return theUser;
        }

        public User DeleteUser(int id)
        {
            var userToDelete = _ctx.Users.FirstOrDefault(u => u.UserId == id);
            _ctx.Remove(userToDelete);
            _ctx.SaveChanges();
            return userToDelete;
        }

        public User EditUser(User alteredUser)
        {
            var finishedUser = _ctx.Update(alteredUser).Entity;
            _ctx.SaveChanges();
            return finishedUser;
        }

        public User FindUserById(int id)
        {
            return _ctx.Users.FirstOrDefault(u => u.UserId == id);
        }

        public List<User> FindUserByName(string name)
        {
            return _ctx.Users.Where(u => u.UserName.ToLower().Contains(name.ToLower())).ToList();
        }

        public List<User> GetUsers(FilterModel filter)
        {
            if (filter == null)
            {
                return _ctx.Users.ToList();
            }
            else
            {
                IEnumerable<User> theUsers = _ctx.Users
                                            .Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                                            .Take(filter.ItemsPrPage);
                if (string.IsNullOrEmpty(filter.SortOrder))
                {
                    return theUsers.ToList();
                }
                else if (filter.SortOrder.ToLower().Equals("desc"))
                {
                    return theUsers.OrderByDescending(u => u.UserId).ToList();
                }
                else
                {
                    return theUsers.OrderBy(o => o.UserId).ToList();
                }
            }
        }
    }
}
