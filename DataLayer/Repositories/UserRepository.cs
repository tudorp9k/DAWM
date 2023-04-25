using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class UserRepository : RepositoryBase<User>
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public User GetByUsername(string username)
        {
            return GetRecords().FirstOrDefault(u => u.Username == username);
        }
    }
}
