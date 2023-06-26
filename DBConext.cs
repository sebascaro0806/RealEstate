using Microsoft.EntityFrameworkCore;

namespace RealEstate.Infrastructure.Context
{
    public class DBConext : DbContext
    {
        public DBConext(DbContextOptions<DBConext> options) : base(options)
        { }

        public DbSet<Owner> Owners { get; set; }
    }
}
