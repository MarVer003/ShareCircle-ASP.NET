using ShareCircle.Models;
using System;
using System.Linq;
using ShareCircle.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;



namespace ShareCircle.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ShareCircleDbContext context)
        {
            context.Database.EnsureCreated();
            // Look for any students.
            if (context.Uporabnik.Any())
            {
                return;   // DB has been seeded
            }

            //dodajanje uporabnikov



            var Uporabniki = new ApplicationUser[]
            {
            new() {Ime="Carson",Priimek="Alexander",DatumPrijave=DateTime.Parse("2005-09-01"),UserName="CarsonAl90",NormalizedUserName="CARSONAL90"},
            new() {Ime="Meredith",Priimek="Alonso",DatumPrijave=DateTime.Parse("2002-09-01"),UserName="Meredith__",NormalizedUserName="MEREDITH__"},
            new() {Ime="Arturo",Priimek="Anand",DatumPrijave=DateTime.Parse("2003-09-01"),UserName="Anand556",NormalizedUserName="ANAND556"},
            new() {Ime="Gytis",Priimek="Barzdukas",DatumPrijave=DateTime.Parse("2002-09-01"),UserName="GytisBarzdukas",NormalizedUserName="GYTISBARZDUKAS"},
            new() {Ime="Yan",Priimek="Li",DatumPrijave=DateTime.Parse("2002-09-01"),UserName="YannaY",NormalizedUserName="YANNAY"},
            new() {Ime="Peggy",Priimek="Justice",DatumPrijave=DateTime.Parse("2001-09-01"),UserName="Peggyone.",NormalizedUserName="PEGGYONE"},
            new() {Ime="Laura",Priimek="Norman",DatumPrijave=DateTime.Parse("2003-09-01"),UserName="LauraNorman",NormalizedUserName="LAURANORMAN"},
            new() {Ime="Nino",Priimek="Olivetto",DatumPrijave=DateTime.Parse("2005-09-01"),UserName="Ninolol._",NormalizedUserName="NINOLOL._._"},
            new(), new()
            };
            var mainUser1 = new ApplicationUser
            {
                Ime = "Martin",
                Priimek = "Veršnjak",
                Email = "mv81348@student.uni-lj.si",
                NormalizedEmail = "MV81348@STUDENT.UNI-LJ.SI",
                UserName = "MarVer",
                NormalizedUserName = "MARVER",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            var mainUser2 = new ApplicationUser
            {
                Ime = "Andraž",
                Priimek = "Arhač",
                Email = "aa12345@student.uni-lj.si",
                NormalizedEmail = "AA12345@STUDENT.UNI-LJ.SI",
                UserName = "AndArh",
                NormalizedUserName = "ANDARH",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            if (!context.Users.Any(u => u.UserName == mainUser1.UserName || u.UserName == mainUser2.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed1 = password.HashPassword(mainUser1, "Test123!");
                var hashed2 = password.HashPassword(mainUser2, "Test123!");
                mainUser1.PasswordHash = hashed1;
                mainUser2.PasswordHash = hashed2;
            }
            Uporabniki[8] = mainUser1;
            Uporabniki[9] = mainUser2;
            foreach (ApplicationUser s in Uporabniki)
            {
                context.Uporabnik.Add(s);
            }
            context.SaveChanges();

            //dodajanje skupin
            var skupine = new Skupina[]
            {
                new() {ImeSkupine="Skupina1",DatumNastanka=DateTime.Parse("2010-01-01")},
                new() {ImeSkupine="Skupina2",DatumNastanka=DateTime.Parse("2010-01-02")}
            };
            foreach (Skupina sk in skupine)
            {
                context.Skupina.Add(sk);
            }
            context.SaveChanges();

            //dodajanje clanov skupin
            var claniSkupin = new ClanSkupine[]
            {
                new() {UporabnikID=Uporabniki[0].Id,SkupinaID=skupine[0].ID,DatumPridruzitve=DateTime.Parse("2011-01-01")},
                new() {UporabnikID=Uporabniki[1].Id,SkupinaID=skupine[0].ID,DatumPridruzitve=DateTime.Parse("2011-01-02")},
                new() {UporabnikID=Uporabniki[2].Id,SkupinaID=skupine[0].ID,DatumPridruzitve=DateTime.Parse("2011-01-02")},
                new() {UporabnikID=Uporabniki[3].Id,SkupinaID=skupine[0].ID,DatumPridruzitve=DateTime.Parse("2011-01-02")},
                new() {UporabnikID=Uporabniki[4].Id,SkupinaID=skupine[1].ID,DatumPridruzitve=DateTime.Parse("2011-02-01")},
                new() {UporabnikID=Uporabniki[5].Id,SkupinaID=skupine[1].ID,DatumPridruzitve=DateTime.Parse("2011-02-02")},
                new() {UporabnikID=Uporabniki[6].Id,SkupinaID=skupine[1].ID,DatumPridruzitve=DateTime.Parse("2011-02-03")},
                new() {UporabnikID=Uporabniki[7].Id,SkupinaID=skupine[1].ID,DatumPridruzitve=DateTime.Parse("2011-02-04")},
                new() {UporabnikID=Uporabniki[8].Id,SkupinaID=skupine[0].ID,DatumPridruzitve=DateTime.Parse("2025-01-01")},
                new() {UporabnikID=Uporabniki[9].Id,SkupinaID=skupine[0].ID,DatumPridruzitve=DateTime.Parse("2025-01-01")},
                new() {UporabnikID=Uporabniki[9].Id,SkupinaID=skupine[1].ID,DatumPridruzitve=DateTime.Parse("2025-01-01")}
            };
            foreach (ClanSkupine cs in claniSkupin)
            {
                context.ClanSkupine.Add(cs);
            }
            context.SaveChanges();

            //dodajanje stroskov
            var stroseki = new Strosek[]
            {
            new() {ID_placnika=Uporabniki[0].Id,ID_skupine=skupine[0].ID,Naslov="pivo",StevilkaStroska=1,CelotniZnesek=4,DatumPlacila=DateTime.Parse("2023-09-01"),Placnik=Uporabniki[0],Skupina=skupine[0]},
            new() {ID_placnika=Uporabniki[1].Id,ID_skupine=skupine[0].ID,Naslov="vino",StevilkaStroska=2,CelotniZnesek=8,DatumPlacila=DateTime.Parse("2023-01-08"),Placnik=Uporabniki[1],Skupina=skupine[0]},
            new() {ID_placnika=Uporabniki[4].Id,ID_skupine=skupine[1].ID,Naslov="hotel",StevilkaStroska=1,CelotniZnesek=16,DatumPlacila=DateTime.Parse("2023-06-04"),Placnik=Uporabniki[4],Skupina=skupine[1]}
            };

            foreach (Strosek c in stroseki)
            {
                context.Strosek.Add(c);
            }
            context.SaveChanges();

            //dodajanje razdelitve stroska
            var razdelitveStroskov = new RazdelitevStroska[]
            {
            new() {ID_stroska=stroseki[0].ID,ID_dolznika=Uporabniki[1].Id,Znesek=0.80m,Strosek=stroseki[0],Dolznik=Uporabniki[1]},
            new() {ID_stroska=stroseki[0].ID,ID_dolznika=Uporabniki[2].Id,Znesek=0.80m,Strosek=stroseki[0],Dolznik=Uporabniki[2]},
            new() {ID_stroska=stroseki[0].ID,ID_dolznika=Uporabniki[3].Id,Znesek=0.80m,Strosek=stroseki[0],Dolznik=Uporabniki[3]},
            new() {ID_stroska=stroseki[0].ID,ID_dolznika=Uporabniki[8].Id,Znesek=0.80m,Strosek=stroseki[0],Dolznik=Uporabniki[8]},
            new() {ID_stroska=stroseki[0].ID,ID_dolznika=Uporabniki[9].Id,Znesek=0.80m,Strosek=stroseki[0],Dolznik=Uporabniki[9]},
            new() {ID_stroska=stroseki[1].ID,ID_dolznika=Uporabniki[0].Id,Znesek=1.60m,Strosek=stroseki[1],Dolznik=Uporabniki[0]},
            new() {ID_stroska=stroseki[1].ID,ID_dolznika=Uporabniki[2].Id,Znesek=1.60m,Strosek=stroseki[1],Dolznik=Uporabniki[2]},
            new() {ID_stroska=stroseki[1].ID,ID_dolznika=Uporabniki[8].Id,Znesek=1.60m,Strosek=stroseki[1],Dolznik=Uporabniki[8]},
            new() {ID_stroska=stroseki[1].ID,ID_dolznika=Uporabniki[9].Id,Znesek=1.60m,Strosek=stroseki[1],Dolznik=Uporabniki[9]},
            new() {ID_stroska=stroseki[1].ID,ID_dolznika=Uporabniki[3].Id,Znesek=1.60m,Strosek=stroseki[1],Dolznik=Uporabniki[3]},
            new() {ID_stroska=stroseki[2].ID,ID_dolznika=Uporabniki[4].Id,Znesek=3.20m,Strosek=stroseki[2],Dolznik=Uporabniki[4]},
            new() {ID_stroska=stroseki[2].ID,ID_dolznika=Uporabniki[5].Id,Znesek=3.20m,Strosek=stroseki[2],Dolznik=Uporabniki[5]},
            new() {ID_stroska=stroseki[2].ID,ID_dolznika=Uporabniki[6].Id,Znesek=3.20m,Strosek=stroseki[2],Dolznik=Uporabniki[6]},
            new() {ID_stroska=stroseki[2].ID,ID_dolznika=Uporabniki[7].Id,Znesek=3.20m,Strosek=stroseki[2],Dolznik=Uporabniki[7]},
            new() {ID_stroska=stroseki[2].ID,ID_dolznika=Uporabniki[9].Id,Znesek=3.20m,Strosek=stroseki[2],Dolznik=Uporabniki[9]},

            };
            foreach (RazdelitevStroska rs in razdelitveStroskov)
            {
                context.RazdelitevStroska.Add(rs);
            }
            context.SaveChanges();

            //dodajanje vracil
            var vracila = new Vracilo[]
            {
                new() {ID_dolznika=Uporabniki[1].Id,ID_upnika=Uporabniki[0].Id,ID_skupine=skupine[0].ID,ZnesekVracila=1,DatumVracila=DateTime.Parse("2024-09-01"),Dolžnik=Uporabniki[1],Upnik=Uporabniki[0],Skupina=skupine[0]},
                new() {ID_dolznika=Uporabniki[2].Id,ID_upnika=Uporabniki[0].Id,ID_skupine=skupine[0].ID,ZnesekVracila=1,DatumVracila=DateTime.Parse("2024-09-02"),Dolžnik=Uporabniki[2],Upnik=Uporabniki[0],Skupina=skupine[0]},
                new() {ID_dolznika=Uporabniki[2].Id,ID_upnika=Uporabniki[0].Id,ID_skupine=skupine[0].ID,ZnesekVracila=2,DatumVracila=DateTime.Parse("2024-09-03"),Dolžnik=Uporabniki[2],Upnik=Uporabniki[0],Skupina=skupine[0]}
            };
            foreach (Vracilo vr in vracila)
            {
                context.Vracilo.Add(vr);
            }
            context.SaveChanges();
        }
    }
}