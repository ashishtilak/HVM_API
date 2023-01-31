using Microsoft.EntityFrameworkCore;

namespace HVM_API.Models
{
    public static class AppModelBuilderExt
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            // Seed Units
            modelBuilder.Entity<Units>().HasData(
                new Units
                {
                    UnitCode = "01",
                    UnitName = "Samaghogha/Pragpur"
                },
                new Units
                {
                    UnitCode = "02",
                    UnitName = "Nana Kapaya"
                }
            );
        }

    }
}
