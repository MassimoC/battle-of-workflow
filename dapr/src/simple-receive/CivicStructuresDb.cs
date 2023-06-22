using Microsoft.EntityFrameworkCore;
using simple_model;


class CivicStructuresDb : DbContext
{
    public CivicStructuresDb(DbContextOptions<CivicStructuresDb> options)
        : base(options) { }

    public DbSet<ParkingFacility> ParkingFacilities => Set<ParkingFacility>();
}