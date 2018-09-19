using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RECOApp.Models
{
    public class RECOAppContext : DbContext
    {
        public RECOAppContext (DbContextOptions<RECOAppContext> options)
            : base(options)
        {
        }

        public DbSet<Document> Document { get; set; }

        public DbSet<Page> Page { get; set; }
    }
}
