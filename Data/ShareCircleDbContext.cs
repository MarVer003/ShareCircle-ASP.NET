using Microsoft.EntityFrameworkCore;
namespace ShareCircle.Data;
using ShareCircle.Models;
using Microsoft.AspNetCore.Authorization;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class ShareCircleDbContext : IdentityDbContext<ApplicationUser>
{

public ShareCircleDbContext(DbContextOptions<ShareCircleDbContext> options) : base(options)
{}

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
base.OnModelCreating(modelBuilder);

}

public DbSet<Skupina> Skupina { get; set; } = default!;

public DbSet<Strosek> Strosek { get; set; } = default!;

public DbSet<Vracilo> Vracilo { get; set; } = default!;

public DbSet<Uporabnik>? Uporabnik { get; set; } = default!;

}