using Microsoft.EntityFrameworkCore;
using SpinsNew.Data;
using SpinsNew.Interfaces;
using SpinsNew.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinsNew.Services
{
    public class TableRegisterUserService : ITableRegisterUser
    {
        public async Task<List<RegisterModel>> DisplayRegisterModelAsync()
        {
            using(var context = new ApplicationDbContext())
            {
                var registeredUsers = await context.tbl_registered_users
                    .OrderByDescending(x => x.Id)
                    .ToListAsync();

                return registeredUsers;
            }
        }
    }
}
