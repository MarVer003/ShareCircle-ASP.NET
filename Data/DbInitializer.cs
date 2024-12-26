using ShareCircle.Models;
using System;
using System.Linq;
using ShareCircle.Data;

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

            var Uporabniki = new Uporabnik[]
            {
            new Uporabnik{Ime="Carson",Priimek="Alexander",DatumPrijave=DateTime.Parse("2005-09-01")},
            new Uporabnik{Ime="Meredith",Priimek="Alonso",DatumPrijave=DateTime.Parse("2002-09-01")},
            new Uporabnik{Ime="Arturo",Priimek="Anand",DatumPrijave=DateTime.Parse("2003-09-01")},
            new Uporabnik{Ime="Gytis",Priimek="Barzdukas",DatumPrijave=DateTime.Parse("2002-09-01")},
            new Uporabnik{Ime="Yan",Priimek="Li",DatumPrijave=DateTime.Parse("2002-09-01")},
            new Uporabnik{Ime="Peggy",Priimek="Justice",DatumPrijave=DateTime.Parse("2001-09-01")},
            new Uporabnik{Ime="Laura",Priimek="Norman",DatumPrijave=DateTime.Parse("2003-09-01")},
            new Uporabnik{Ime="Nino",Priimek="Olivetto",DatumPrijave=DateTime.Parse("2005-09-01")}
            };
            foreach (Uporabnik s in Uporabniki)
            {
                context.Uporabnik.Add(s);
            }
            context.SaveChanges();

//dodajanje skupin
            var skupine = new Skupina[]
            {
                new Skupina{ImeSkupine="Skupina1",DatumNastanka=DateTime.Parse("2010-01-01")},
                new Skupina{ImeSkupine="Skupina2",DatumNastanka=DateTime.Parse("2010-01-02")}
            };
            foreach (Skupina sk in skupine)
            {
                context.Skupina.Add(sk);
            }
            context.SaveChanges();

//dodajanje clanov skupin
            var claniSkupin = new ClanSkupine[]
            {
                new ClanSkupine{UporabnikID=Uporabniki[0].ID,SkupinaID=skupine[0].ID,DatumPridruzitve=DateTime.Parse("2011-01-01")},
                new ClanSkupine{UporabnikID=Uporabniki[1].ID,SkupinaID=skupine[0].ID,DatumPridruzitve=DateTime.Parse("2011-01-02")},
                new ClanSkupine{UporabnikID=Uporabniki[2].ID,SkupinaID=skupine[0].ID,DatumPridruzitve=DateTime.Parse("2011-01-02")},
                new ClanSkupine{UporabnikID=Uporabniki[3].ID,SkupinaID=skupine[0].ID,DatumPridruzitve=DateTime.Parse("2011-01-02")},
                new ClanSkupine{UporabnikID=Uporabniki[4].ID,SkupinaID=skupine[1].ID,DatumPridruzitve=DateTime.Parse("2011-02-01")},
                new ClanSkupine{UporabnikID=Uporabniki[5].ID,SkupinaID=skupine[1].ID,DatumPridruzitve=DateTime.Parse("2011-02-02")},
                new ClanSkupine{UporabnikID=Uporabniki[6].ID,SkupinaID=skupine[1].ID,DatumPridruzitve=DateTime.Parse("2011-02-03")},
                new ClanSkupine{UporabnikID=Uporabniki[7].ID,SkupinaID=skupine[1].ID,DatumPridruzitve=DateTime.Parse("2011-02-04")}
            };
            foreach (ClanSkupine cs in claniSkupin)
            {
                context.ClanSkupine.Add(cs);
            }
            context.SaveChanges();

//dodajanje stroskov
            var stroseki = new Strosek[]
            {
            new Strosek{ID_placnika=Uporabniki[0].ID,ID_skupine=skupine[0].ID,Naslov="pivo",StevilkaStroska=1,CelotniZnesek=4,DatumPlacila=DateTime.Parse("2023-09-01"),Placnik=Uporabniki[0],Skupina=skupine[0]},
            new Strosek{ID_placnika=Uporabniki[1].ID,ID_skupine=skupine[0].ID,Naslov="vino",StevilkaStroska=2,CelotniZnesek=8,DatumPlacila=DateTime.Parse("2023-01-08"),Placnik=Uporabniki[1],Skupina=skupine[0]},
            new Strosek{ID_placnika=Uporabniki[4].ID,ID_skupine=skupine[1].ID,Naslov="hotel",StevilkaStroska=1,CelotniZnesek=16,DatumPlacila=DateTime.Parse("2023-06-04"),Placnik=Uporabniki[4],Skupina=skupine[1]}
            };

            foreach (Strosek c in stroseki)
            {
                context.Strosek.Add(c);
            }
            context.SaveChanges();

//dodajanje razdelitve stroska
            var razdelitveStroskov = new RazdelitevStroska[]
            {
            new RazdelitevStroska{ID_stroska=stroseki[0].ID,ID_dolznika=Uporabniki[1].ID,Znesek=1,Strosek=stroseki[0],Dolznik=Uporabniki[1]},
            new RazdelitevStroska{ID_stroska=stroseki[0].ID,ID_dolznika=Uporabniki[2].ID,Znesek=1,Strosek=stroseki[0],Dolznik=Uporabniki[2]},
            new RazdelitevStroska{ID_stroska=stroseki[0].ID,ID_dolznika=Uporabniki[3].ID,Znesek=1,Strosek=stroseki[0],Dolznik=Uporabniki[3]},
            new RazdelitevStroska{ID_stroska=stroseki[1].ID,ID_dolznika=Uporabniki[0].ID,Znesek=2,Strosek=stroseki[1],Dolznik=Uporabniki[0]},
            new RazdelitevStroska{ID_stroska=stroseki[1].ID,ID_dolznika=Uporabniki[2].ID,Znesek=2,Strosek=stroseki[1],Dolznik=Uporabniki[2]},
            new RazdelitevStroska{ID_stroska=stroseki[1].ID,ID_dolznika=Uporabniki[3].ID,Znesek=2,Strosek=stroseki[1],Dolznik=Uporabniki[3]},
            new RazdelitevStroska{ID_stroska=stroseki[2].ID,ID_dolznika=Uporabniki[5].ID,Znesek=4,Strosek=stroseki[2],Dolznik=Uporabniki[5]},
            new RazdelitevStroska{ID_stroska=stroseki[2].ID,ID_dolznika=Uporabniki[6].ID,Znesek=4,Strosek=stroseki[2],Dolznik=Uporabniki[6]},
            new RazdelitevStroska{ID_stroska=stroseki[2].ID,ID_dolznika=Uporabniki[7].ID,Znesek=4,Strosek=stroseki[2],Dolznik=Uporabniki[7]},

            };
            foreach (RazdelitevStroska rs in razdelitveStroskov)
            {
                context.RazdelitevStroskas.Add(rs);
            }
            context.SaveChanges();

//dodajanje vracil
            var vracila = new Vracilo[]
            {
                new Vracilo{ID_dolznika=Uporabniki[1].ID,ID_skupine=skupine[0].ID,StevilkaVracila=1,ZnesekVracila=1,DatumVracila=DateTime.Parse("2024-09-01"),Dolžnik=Uporabniki[1],Skupina=skupine[0]},
                new Vracilo{ID_dolznika=Uporabniki[2].ID,ID_skupine=skupine[0].ID,StevilkaVracila=2,ZnesekVracila=1,DatumVracila=DateTime.Parse("2024-09-02"),Dolžnik=Uporabniki[2],Skupina=skupine[0]},
                new Vracilo{ID_dolznika=Uporabniki[2].ID,ID_skupine=skupine[0].ID,StevilkaVracila=3,ZnesekVracila=2,DatumVracila=DateTime.Parse("2024-09-03"),Dolžnik=Uporabniki[2],Skupina=skupine[0]}
            };
            foreach (Vracilo vr in vracila)
            {
                context.Vracilo.Add(vr);
            }
            context.SaveChanges();

        }
    }
}