using ShareCircle.Models;
using System;
using System.Linq;
using ShareCircle.Data;
using Microsoft.EntityFrameworkCore;



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
            new() {Ime="Carson",Priimek="Alexander",DatumPrijave=DateTime.Parse("2005-09-01")},
            new() {Ime="Meredith",Priimek="Alonso",DatumPrijave=DateTime.Parse("2002-09-01")},
            new() {Ime="Arturo",Priimek="Anand",DatumPrijave=DateTime.Parse("2003-09-01")},
            new() {Ime="Gytis",Priimek="Barzdukas",DatumPrijave=DateTime.Parse("2002-09-01")},
            new() {Ime="Yan",Priimek="Li",DatumPrijave=DateTime.Parse("2002-09-01")},
            new() {Ime="Peggy",Priimek="Justice",DatumPrijave=DateTime.Parse("2001-09-01")},
            new() {Ime="Laura",Priimek="Norman",DatumPrijave=DateTime.Parse("2003-09-01")},
            new() {Ime="Nino",Priimek="Olivetto",DatumPrijave=DateTime.Parse("2005-09-01")}
            };
            foreach (Uporabnik s in Uporabniki)
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
                new() {UporabnikID=Uporabniki[0].ID,SkupinaID=skupine[0].ID,DatumPridruzitve=DateTime.Parse("2011-01-01"),Stanje=-2.67m},
                new() {UporabnikID=Uporabniki[1].ID,SkupinaID=skupine[0].ID,DatumPridruzitve=DateTime.Parse("2011-01-02"),Stanje=7.67m},
                new() {UporabnikID=Uporabniki[2].ID,SkupinaID=skupine[0].ID,DatumPridruzitve=DateTime.Parse("2011-01-02"),Stanje=-1m},
                new() {UporabnikID=Uporabniki[3].ID,SkupinaID=skupine[0].ID,DatumPridruzitve=DateTime.Parse("2011-01-02"),Stanje=-4m},
                new() {UporabnikID=Uporabniki[4].ID,SkupinaID=skupine[1].ID,DatumPridruzitve=DateTime.Parse("2011-02-01"),Stanje=16m},
                new() {UporabnikID=Uporabniki[5].ID,SkupinaID=skupine[1].ID,DatumPridruzitve=DateTime.Parse("2011-02-02"),Stanje=-5.33m},
                new() {UporabnikID=Uporabniki[6].ID,SkupinaID=skupine[1].ID,DatumPridruzitve=DateTime.Parse("2011-02-03"),Stanje=-5.33m},
                new() {UporabnikID=Uporabniki[7].ID,SkupinaID=skupine[1].ID,DatumPridruzitve=DateTime.Parse("2011-02-04"),Stanje=-5.34m}
            };
            foreach (ClanSkupine cs in claniSkupin)
            {
                context.ClanSkupine.Add(cs);
            }
            context.SaveChanges();

            //dodajanje stroskov
            var stroseki = new Strosek[]
            {
            new() {ID_placnika=Uporabniki[0].ID,ID_skupine=skupine[0].ID,Naslov="pivo",StevilkaStroska=1,CelotniZnesek=4,DatumPlacila=DateTime.Parse("2023-09-01"),Placnik=Uporabniki[0],Skupina=skupine[0]},
            new() {ID_placnika=Uporabniki[1].ID,ID_skupine=skupine[0].ID,Naslov="vino",StevilkaStroska=2,CelotniZnesek=8,DatumPlacila=DateTime.Parse("2023-01-08"),Placnik=Uporabniki[1],Skupina=skupine[0]},
            new() {ID_placnika=Uporabniki[4].ID,ID_skupine=skupine[1].ID,Naslov="hotel",StevilkaStroska=1,CelotniZnesek=16,DatumPlacila=DateTime.Parse("2023-06-04"),Placnik=Uporabniki[4],Skupina=skupine[1]}
            };

            foreach (Strosek c in stroseki)
            {
                context.Strosek.Add(c);
            }
            context.SaveChanges();

            //dodajanje razdelitve stroska
            var razdelitveStroskov = new RazdelitevStroska[]
            {
            new() {ID_stroska=stroseki[0].ID,ID_dolznika=Uporabniki[1].ID,Znesek=1.33m,Strosek=stroseki[0],Dolznik=Uporabniki[1]},
            new() {ID_stroska=stroseki[0].ID,ID_dolznika=Uporabniki[2].ID,Znesek=1.33m,Strosek=stroseki[0],Dolznik=Uporabniki[2]},
            new() {ID_stroska=stroseki[0].ID,ID_dolznika=Uporabniki[3].ID,Znesek=1.34m,Strosek=stroseki[0],Dolznik=Uporabniki[3]},
            new() {ID_stroska=stroseki[1].ID,ID_dolznika=Uporabniki[0].ID,Znesek=2.67m,Strosek=stroseki[1],Dolznik=Uporabniki[0]},
            new() {ID_stroska=stroseki[1].ID,ID_dolznika=Uporabniki[2].ID,Znesek=2.67m,Strosek=stroseki[1],Dolznik=Uporabniki[2]},
            new() {ID_stroska=stroseki[1].ID,ID_dolznika=Uporabniki[3].ID,Znesek=2.66m,Strosek=stroseki[1],Dolznik=Uporabniki[3]},
            new() {ID_stroska=stroseki[2].ID,ID_dolznika=Uporabniki[5].ID,Znesek=5.33m,Strosek=stroseki[2],Dolznik=Uporabniki[5]},
            new() {ID_stroska=stroseki[2].ID,ID_dolznika=Uporabniki[6].ID,Znesek=5.33m,Strosek=stroseki[2],Dolznik=Uporabniki[6]},
            new() {ID_stroska=stroseki[2].ID,ID_dolznika=Uporabniki[7].ID,Znesek=5.34m,Strosek=stroseki[2],Dolznik=Uporabniki[7]},

            };
            foreach (RazdelitevStroska rs in razdelitveStroskov)
            {
                context.RazdelitevStroska.Add(rs);
            }
            context.SaveChanges();

            //dodajanje vracil
            var vracila = new Vracilo[]
            {
                new() {ID_dolznika=Uporabniki[1].ID,ID_upnika=Uporabniki[0].ID,ID_skupine=skupine[0].ID,StevilkaVracila=1,ZnesekVracila=1,DatumVracila=DateTime.Parse("2024-09-01"),Dolžnik=Uporabniki[1],Upnik=Uporabniki[0],Skupina=skupine[0]},
                new() {ID_dolznika=Uporabniki[2].ID,ID_upnika=Uporabniki[0].ID,ID_skupine=skupine[0].ID,StevilkaVracila=2,ZnesekVracila=1,DatumVracila=DateTime.Parse("2024-09-02"),Dolžnik=Uporabniki[2],Upnik=Uporabniki[0],Skupina=skupine[0]},
                new() {ID_dolznika=Uporabniki[2].ID,ID_upnika=Uporabniki[0].ID,ID_skupine=skupine[0].ID,StevilkaVracila=3,ZnesekVracila=2,DatumVracila=DateTime.Parse("2024-09-03"),Dolžnik=Uporabniki[2],Upnik=Uporabniki[0],Skupina=skupine[0]}
            };
            foreach (Vracilo vr in vracila)
            {
                context.Vracilo.Add(vr);
            }
            context.SaveChanges();
        }
    }
}