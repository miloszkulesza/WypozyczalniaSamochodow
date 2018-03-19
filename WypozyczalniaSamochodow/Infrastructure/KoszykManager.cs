using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WypozyczalniaSamochodow.DAL;
using WypozyczalniaSamochodow.Models;

namespace WypozyczalniaSamochodow.Infrastructure
{
    public class KoszykManager
    {
        private WypozyczeniaContext db;
        private ISessionManager session;

        public KoszykManager(ISessionManager session, WypozyczeniaContext db)
        {
            this.session = session;
            this.db = db;
        }

        public List<PozycjaKoszyka> PobierzKoszyk()
        {
            List<PozycjaKoszyka> koszyk;
            if (session.Get<List<PozycjaKoszyka>>(Consts.KoszykSessionKey) == null)
            {
                koszyk = new List<PozycjaKoszyka>();
            }
            else
            {
                koszyk = session.Get<List<PozycjaKoszyka>>(Consts.KoszykSessionKey) as List<PozycjaKoszyka>;
            }
            return koszyk;
        }

        public void DodajDoKoszyka(int autoId)
        {
            var koszyk = PobierzKoszyk();
            var pozycjaKoszyka = koszyk.Find(a => a.Auto.AutoId == autoId);
            if(pozycjaKoszyka == null)
            {
                var AutoDoDodania = db.Auta.Where(a => a.AutoId == autoId).SingleOrDefault();
                if(AutoDoDodania != null)
                {
                    var nowaPozycja = new PozycjaKoszyka()
                    {
                        Auto = AutoDoDodania,
                        Wartosc = AutoDoDodania.Cena
                    };
                    koszyk.Add(nowaPozycja);
                }
            }
            session.Set(Consts.KoszykSessionKey, koszyk);
        }

        public int UsunZKoszyka(int autoId)
        {
            var koszyk = PobierzKoszyk();
            var pozycja = koszyk.Find(a => a.Auto.AutoId == autoId);
            if(pozycja != null)
            {
                koszyk.Remove(pozycja);
            }
            return 0;
        }

        public decimal WartoscKoszyka()
        {
            var koszyk = PobierzKoszyk();
            return koszyk.Sum(k => k.Wartosc);
        }

        public Wypozyczenie UtworzWypozyczenie(Wypozyczenie noweWypozyczenie, string userEmail)
        {
            var koszyk = PobierzKoszyk();
            noweWypozyczenie.DataZlozenia = DateTime.Now;
            noweWypozyczenie.Email = userEmail;
            db.Wypozyczenia.Add(noweWypozyczenie);
            if (noweWypozyczenie.PozycjeWypozyczenia == null)
                noweWypozyczenie.PozycjeWypozyczenia = new List<PozycjaWypozyczenia>();
            decimal wartoscKoszyka = 0;
            foreach(var pozycja in koszyk)
            {
                var nowaPozycja = new PozycjaWypozyczenia()
                {
                    AutoId = pozycja.Auto.AutoId,
                    WartoscZamowienia = pozycja.Auto.Cena,

                };
                wartoscKoszyka += pozycja.Auto.Cena;
                noweWypozyczenie.PozycjeWypozyczenia.Add(nowaPozycja);
            }
            noweWypozyczenie.Wartosc = wartoscKoszyka;
            db.SaveChanges();
            return noweWypozyczenie;
        }

        public void PustyKoszyk()
        {
            session.Set<List<PozycjaKoszyka>>(Consts.KoszykSessionKey, null);
        }
    }
}