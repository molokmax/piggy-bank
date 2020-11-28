using System;
using Microsoft.EntityFrameworkCore;
using Persist.Model;

namespace Persist
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<StoragePersistModel> Storages { get; set; }
        public DbSet<PurposePersistModel> Purposes { get; set; }
        public DbSet<StoreItemPersistModel> StoreItems { get; set; }
        public DbSet<MovementPersistModel> Movements { get; set; }
    }
}
