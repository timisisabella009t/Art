using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Art.Models;

namespace Art.Data
{
    public class ArtContext : DbContext
    {
        public ArtContext (DbContextOptions<ArtContext> options)
            : base(options)
        {
        }

        public DbSet<Art.Models.Picture> Picture { get; set; }

        public DbSet<Art.Models.Publisher> Publisher { get; set; }

        public DbSet<Art.Models.Category> Category { get; set; }
    }
}
