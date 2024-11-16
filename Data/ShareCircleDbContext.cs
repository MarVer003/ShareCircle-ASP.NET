using Microsoft.EntityFrameworkCore;
using ShareCircle.Models;
namespace ShareCircle.Data;
using ShareCircle.Models;

using Microsoft.EntityFrameworkCore;

public class ShareCircleDbContext : DbContext
{

public ShareCircleDbContext(DbContextOptions<ShareCircleDbContext> options) : base(options)
{}

protected override void OnModelCreating(ModelBuilder modelBuilder)
{}

public DbSet<Skupina> Skupina { get; set; } = default!;

public DbSet<Strosek> Strosek { get; set; } = default!;

public DbSet<Vracilo> Vracilo { get; set; } = default!;

public DbSet<Uporabnik>? Uporabnik { get; set; } = default!;

}