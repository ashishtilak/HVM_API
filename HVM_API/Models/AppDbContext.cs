using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HVM_API.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // copied from 
            // https://github.com/dotnet/efcore/issues/11003#issuecomment-492333796
            // for resolving composite primary key issue

            base.OnModelCreating(modelBuilder);

            // find all entities having count of KeyAttributes greater than one.
            foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes()
                         .Where(t =>
                             t.ClrType.GetProperties()
                                 .Count(p => p.CustomAttributes.Any(a => a.AttributeType == typeof(KeyAttribute))) > 1))
            {
                // get the keys in the appropriate order
                var orderedKeys = entity.ClrType
                    .GetProperties()
                    .Where(p => p.CustomAttributes.Any(a => a.AttributeType == typeof(KeyAttribute)))
                    .OrderBy(p =>
                        p.CustomAttributes.Single(x => x.AttributeType == typeof(ColumnAttribute))?
                            .NamedArguments?.Single(y => y.MemberName == nameof(ColumnAttribute.Order))
                            .TypedValue.Value ?? 0)
                    .Select(x => x.Name)
                    .ToArray();

                // apply the keys to the model builder
                modelBuilder.Entity(entity.ClrType).HasKey(orderedKeys);
            }


            // change default delete behaviour
            foreach (IMutableForeignKey key in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                key.DeleteBehavior = DeleteBehavior.Restrict;
            }

            //singular table names
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetTableName(entityType.DisplayName());
            }

            // data seeding extension method:
            modelBuilder.SeedData();
        }


        public DbSet<Units> Units { get; set; }
        public DbSet<AuthObjects> AuthObjects { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<RoleAuths> RoleAuths { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<RoleUsers> RoleUsers { get; set; }
        public DbSet<UserUnits> UserUnits { get; set; }
    }
}