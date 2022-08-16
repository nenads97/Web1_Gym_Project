using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitensCentar.Models
{
    public class SingltonPristupPodacima
    {
        private static SingltonPristupPodacima instanca;

        public List<Vlasnik> _Vlasnici { get; protected set; }
        public List<Posetilac> _Posetioci { get; protected set; }
        public List<Trener> _Treneri { get; protected set; }

        private SingltonPristupPodacima()
        {
            UcitajPodatke();
            DodajVlasnikeNaPocetku();
            InicijalizujKolekcije();
        }

        private void UcitajPodatke()
        {
            CitajVlasnike();
            CitajPosetioce();
            CitajTrenere();
        }

        #region Pisi/Citaj
        private void PisiVlasnike()
        {
            XMLFileController.SerializujVlasnike(new Vlasnici(_Vlasnici));
        }

        private void PisiPosetioce()
        {
            XMLFileController.SerializujPosetioce(new Posetioci(_Posetioci));
        }

        private void PisiTrenere()
        {
            XMLFileController.SerializujTrenere(new Treneri(_Treneri));
        }

        private List<Vlasnik> CitajVlasnike()
        {
            Vlasnici vlasnici = null;
            if ((vlasnici = XMLFileController.DeserijalizujVlasnike()) != null)
            {
                _Vlasnici = vlasnici.DobaviVlasnike();
            }
            return _Vlasnici;
        }

        private List<Posetilac> CitajPosetioce()
        {
            Posetioci posetioci = null;
            if ((posetioci = XMLFileController.DeserijalizujPosetioce()) != null)
            {
                _Posetioci = posetioci.DobaviPosetioce();
            }
            return _Posetioci;
        }

        private List<Trener> CitajTrenere()
        {
            Treneri treneri = null;
            if ((treneri = XMLFileController.DeserijalizujTrenere()) != null)
            {
                _Treneri = treneri.DobaviTrenere();
            }
            return _Treneri;
        }
        #endregion

        #region Dodaj

        #region DodajJednog
        public void DodajPosetioca(Posetilac p)
        {
            _Posetioci.Add(p);
            object posetilac = p;
            PisiPosetioce();
        }

        public void DodajTrenera(Trener t)
        {
            _Treneri.Add(t);
            object trener = t;
            PisiTrenere();
        }

        public void DodajVlasnika(Vlasnik v)
        {
            _Vlasnici.Add(v);
            object vlasnik = v;
            PisiVlasnike();
        }
        //Nisam siguran za ovo
        public void DodajFitnesCentar(FitnesCentar f)
        {
            Vlasnik v = _Vlasnici.Find(x => x.Username == f.Vlasnik.Username);
            if (v != null)
            {
                v.ftCentri.Add(f);
                object fitnesCentar = f;
                PisiVlasnike();
            }
        }
        /*
        public void DodajGrupniTrening(GrupniTrening gt)
        {
           Trener po = _Treneri.Find(x => x.Username == gt.)
        }
        */
        
        #endregion

        #region DodajVise
        public void DodajVlasnike(List<Vlasnik> vlasnici)
        {
            foreach (Vlasnik v in vlasnici)
            {
                if(!_Vlasnici.Contains(v))
                {
                    _Vlasnici.Add(v);
                    object vlasnik = v;
                    PisiVlasnike();

                }
            }
        }

        public void DodajPosetioce(List<Posetilac> posetioci)
        {
            foreach(Posetilac p in posetioci)
            {
                if(!_Posetioci.Contains(p))
                {
                    _Posetioci.Add(p);
                    object posetilac = p;
                    PisiPosetioce();
                }
            }
        }

        public void DodajTrenere(List<Trener> treneri)
        {
            foreach(Trener t in treneri)
            {
                if(!_Treneri.Contains(t))
                {
                    _Treneri.Add(t);
                    object trener = t;
                    PisiTrenere();
                }
            }
        }

        public void DodajFitnesCentre(List<FitnesCentar> fitnesCentri)
        {
            foreach(FitnesCentar fc in fitnesCentri)
            {
                foreach (FitnesCentar f in fitnesCentri)
                    DodajFitnesCentar(fc);
            }
        }
        #endregion

        #endregion

        #region Obrisi
        #region ObrisiJednog
        public void ObrisiVlasnika(Vlasnik v)
        {
            if(_Vlasnici.Contains(v))
            {
                _Vlasnici.Remove(v);
                object vlasnik = v;
                PisiVlasnike();
            }
        }

        public void ObrisiPosetioca(Posetilac p)
        {
            if(_Posetioci.Contains(p))
            {
                _Posetioci.Remove(p);
                object posetilac = p;
                PisiPosetioce();
            }
        }

        public void ObrisiTrenera(Trener t)
        {
            if (_Treneri.Contains(t))
            {
                _Treneri.Remove(t);
                object trener = t;
                PisiTrenere();
            }
        }

        public void ObrisiFitnesCentar(FitnesCentar f)
        {
            foreach (Vlasnik v in _Vlasnici)
            {
                if(v.ftCentri.Contains(f))
                {
                    v.ftCentri.Remove(f);
                    object fitnesCentar = f;
                    PisiVlasnike();
                }
            }
        }
        #endregion

        #region ObrisiVise
        public void ObrisiVlasnike(List<Vlasnik> vlasniciZaBrisanje)
        {
            foreach (Vlasnik v in vlasniciZaBrisanje)
                ObrisiVlasnika(v);
        }

        public void ObrisiPosetioce(List<Posetilac> posetiociZaBrisanje)
        {
            foreach(Posetilac p in posetiociZaBrisanje)
            {
                ObrisiPosetioca(p);
            }
        }

        public void ObrisiTrenera (List<Trener> treneriZaBrisanje)
        {
            foreach(Trener t in treneriZaBrisanje)
            {
                ObrisiTrenera(t);
            }
        }

        public void ObrisiFitnesCentar(List<FitnesCentar> fitnesCentri)
        {
            foreach (Vlasnik v in _Vlasnici)
                foreach (FitnesCentar fc in fitnesCentri)
                    if (v.ftCentri.Contains(fc))
                        ObrisiFitnesCentar(fc);
        }
        #endregion
        #endregion

        #region Modifikuj
        public void ModifikujVlasnika(Vlasnik v)
        {
            Vlasnik vlasniciZaZamenu = null;
            if ((vlasniciZaZamenu = _Vlasnici.Find(x => x.Username == v.Username)) != null)
            {
                _Vlasnici.Remove(vlasniciZaZamenu);
                _Vlasnici.Add(v);
                object vlasnik = v;
                PisiVlasnike();
            }
        }

        public void ModifikujPosetioca(Posetilac p)
        {
            Posetilac posetiociZaZamenu = null;
            if ((posetiociZaZamenu = _Posetioci.Find(x => x.Username == p.Username)) != null)
            {
                _Posetioci.Remove(posetiociZaZamenu);
                _Posetioci.Add(p);
                object posetilac = p;
                PisiPosetioce();
            }
        }

        public void ModifikujTrenera(Trener t)
        {
            Trener treneriZaZamenu = null;
            if ((treneriZaZamenu = _Treneri.Find(x => x.Username == t.Username)) != null)
            {
                _Treneri.Remove(treneriZaZamenu);
                _Treneri.Add(t);
                object trener = t;
                PisiTrenere();
            }
        }

        /*
        public void ModifikujFitnesCentar(FitnesCentar fc)
        {
            foreach (Vlasnik v in _Vlasnici)
            {
                FitnesCentar fitnesCentarZaBrisanje = null;
                if ((fitnesCentarZaBrisanje = v.ftCentri.Find
                    (x => x.GodinaOtvaranja = fc.GodinaOtvaranja &&)))
            }
        }
        */
        #endregion

        private void DodajVlasnikeNaPocetku()
        {
            if(_Vlasnici.Count == 0)
            {
                Vlasnik v1 = new Vlasnik("Nenad97", "suknovic97", "Nenad", "Suknovic", Pol.MUSKI, "nenad.suknovic@gmail.com", "10/01/1997");
                Vlasnik v2 = new Vlasnik("Predrag97", "suknovic97", "Predrag", "Suknovic", Pol.DRUGO, "nenad.suknovic@gmail.com", "10/01/1997");
                _Vlasnici = new List<Vlasnik>();
                _Vlasnici.Add(v1);
                _Vlasnici.Add(v2);
                PisiVlasnike();
            }
        }

        private void InicijalizujKolekcije()
        {
            if(_Vlasnici == null)
            {
                _Vlasnici = new List<Vlasnik>();
            }
            if(_Treneri == null)
            {
                _Treneri = new List<Trener>();
            }
            if(_Posetioci == null)
            {
                _Posetioci = new List<Posetilac>();
            }
        }

        public static SingltonPristupPodacima Instanca()
        {
            if(instanca == null)
            {
                instanca = new SingltonPristupPodacima();
            }
            return instanca;
        }
    }
}