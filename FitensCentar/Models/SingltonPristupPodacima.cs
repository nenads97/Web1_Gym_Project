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

        private SingletonPristupPodacima()
        {
            UcitajPodatke();
            DodajVlasnikaNaPocetku();
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

        public void DodajFitnesCentar(FitnesCentar f)
        {
            Trener t = _Treneri.Find(x => x.Username == f.Vlasnik.Username);
            if (t != null)
            {
                t.ftCentri.Add(f);
                object fitnesCentar = f;
                PisiTrenere();
            }
        }

        /*public void DodajGrupniTrening(GrupniTrening gt)
        {
           
        }
        */
        #endregion

        #region DodajVise
        #endregion

        #endregion
    }
}