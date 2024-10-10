using Microsoft.EntityFrameworkCore;
using SpinsNew.Data;
using SpinsNew.Interfaces;
using SpinsNew.Models;
using SpinsNew.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinsNew.Services
{
    public class TableRegisterUserService : ITableRegisterUser
    {
        public async Task<List<RegisterUsersViewModel>> DisplayRegisterModelAsync()
        {
            using (var context = new ApplicationDbContext())
            {
                var registeredUsers = await context.tbl_registered_users
                    .Include(x => x.LibraryRole)
                     .Select(x => new RegisterUsersViewModel
                     {
                         Id = x.Id,
                         Lastname = x.Lastname,
                         Firstname = x.Firstname,
                         Middlename = x.Middlename,
                         Birthdate = x.Birthdate,
                         Username = x.Username,
                         Password = x.Password,
                         Role = x.LibraryRole.Role,
                         DateRegistered = x.DateRegistered,
                         IsActive = x.IsActive

                     })
                    .OrderByDescending(x => x.Id)
                    .ToListAsync();

                return registeredUsers;
            }
        }
    }
}
