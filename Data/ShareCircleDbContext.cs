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

        modelBuilder.Entity<Vracilo>(entity =>
        {
            // Configure relationship for Dolžnik
            entity.HasOne(v => v.Dolžnik)
                .WithMany()
                .HasForeignKey(v => v.ID_dolznika)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure relationship for Upnik
            entity.HasOne(v => v.Upnik)
                .WithMany()
                .HasForeignKey(v => v.ID_upnika)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure relationship with Skupina
            entity.HasOne(v => v.Skupina)
                .WithMany(s => s.Vracila)
                .HasForeignKey(v => v.ID_skupine)
                .OnDelete(DeleteBehavior.Cascade);
        });
/*
        modelBuilder.Entity<Vracilo>()
            .ToTable(tv => tv.HasTrigger("trg_PosodobiStanjaVracilo"));

        modelBuilder.Entity<Strosek>()
            .ToTable(ts => ts.HasTrigger("trg_PosodobiStanjaStrosek"));*/
    }

    internal static void Initialize(ShareCircleDbContext context)
    {
        throw new NotImplementedException();
    }

    public override int SaveChanges()
    {
        /*ExecuteSqlScript("sql/IzbrisiTriggerje.sql");
        ExecuteSqlScript("sql/triggers/StrosekPosodobiStanja.sql");
        ExecuteSqlScript("sql/triggers/VraciloPosodobiStanja.sql");*/
        return base.SaveChanges();
    }

    private void ExecuteSqlScript(string filePath)
    {
        var script = File.ReadAllText(filePath);
        Database.ExecuteSqlRaw(script);
    }

    public DbSet<Skupina> Skupina { get; set; } = default!;

    public DbSet<Strosek> Strosek { get; set; } = default!;

    public DbSet<Vracilo> Vracilo { get; set; } = default!;

    public DbSet<Uporabnik> Uporabnik { get; set; } = default!;

    public DbSet<ClanSkupine> ClanSkupine { get; set; } = default!;

    public DbSet<RazdelitevStroska> RazdelitevStroska { get; set; } = default!;

}