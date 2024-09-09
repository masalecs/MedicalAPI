using Microsoft.EntityFrameworkCore;
using System.Numerics;

public class ApplicationDbContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Specialization> Specializations { get; set; }
    public DbSet<Office> Offices { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
}