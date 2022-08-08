using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitensCentar.Models
{
    public class Vlasnici : ICollection
    {
        public Vlasnici()
        {

        }

        public Vlasnici(List<Vlasnik> _vlasnici)
        {
            DodajVlasnike(_vlasnici);
        }

        public string CollectionName { get; set; } = "Vlasnici";

        private ArrayList vlasnici = new ArrayList();

        public int Count { get { return vlasnici.Count; } }

        public object SyncRoot { get { return this; } }

        public bool IsSynchronized { get { return false; } }

        public void CopyTo(Array array, int index)
        {
            vlasnici.CopyTo(array, index);
        }

        public Vlasnik this[int index]
        {
            get { return (Vlasnik)vlasnici[index]; }
        }

        public IEnumerator GetEnumerator()
        {
            return vlasnici.GetEnumerator();
        }

        private void DodajVlasnike(List<Vlasnik> _vlasnici)
        {
            foreach (Vlasnik a in _vlasnici)
            {
                vlasnici.Add(a);
            }
        }

        public void Add(Vlasnik a)
        {
            vlasnici.Add(a);
        }

        public List<Vlasnik> DobaviVlasnike()
        {
            List<Vlasnik> _vlasnici = new List<Vlasnik>();

            foreach (var a in vlasnici)
            {
                _vlasnici.Add(a as Vlasnik);
            }

            return _vlasnici;
        }
    }
}