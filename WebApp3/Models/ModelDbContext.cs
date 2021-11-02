using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp3.Models
{
    public class ModelDbContext : DbContext
    {
        public ModelDbContext(DbContextOptions<ModelDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Personne> Personne { get; set; }

    }
}
