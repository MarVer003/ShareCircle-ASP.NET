using Microsoft.EntityFrameworkCore;
using ShareCircle.Models;
namespace ShareCircle.Data;
using ShareCircle.Models;
using Microsoft.AspNetCore.Authorization;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

public class ShareCircleDbContext : IdentityDbContext<ApplicationUser>
{

    public ShareCircleDbContext(DbContextOptions<ShareCircleDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    }

    internal static void Initialize(ShareCircleDbContext context)
    {
        throw new NotImplementedException();
    }

    public DbSet<Skupina> Skupina { get; set; } = default!;

    public DbSet<Strosek> Strosek { get; set; } = default!;

    public DbSet<Vracilo> Vracilo { get; set; } = default!;

    public DbSet<Uporabnik> Uporabnik { get; set; } = default!;

    public DbSet<ClanSkupine> ClanSkupine { get; set; } = default!;

    public DbSet<RazdelitevStroska> RazdelitevStroska { get; set; } = default!;

}