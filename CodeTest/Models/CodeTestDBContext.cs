using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CodeTest.Models
{
    public class CodeTestDBContext : DbContext
    {
        public CodeTestDBContext(DbContextOptions<CodeTestDBContext> options)
            : base(options)
        {
        }

        public DbSet<Customers> Customers { get; set; }

        public DbSet<Account> Account { get; set; }
    }
}
