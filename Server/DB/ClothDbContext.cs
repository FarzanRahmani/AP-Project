using Microsoft.EntityFrameworkCore;
using Mydatas;
using P8.Server.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace P8.Server.DB
{
    public class ClothDbContext : DbContext
    {
        public ClothDbContext(DbContextOptions<ClothDbContext> options)
        : base(options)
        {}
        public DbSet<Cloth> Clothes { get; set; } // DBSet --> table --> attribute(column) - tuple(record-row)
        public DbSet<product> Products { get; set; }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
