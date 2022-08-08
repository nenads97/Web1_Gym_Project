using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitensCentar.Models
{
    public class Treneri : ICollection
    {
        public Treneri()
        {

        }

        public Treneri(List<Trener> _treneri)
        {
            DodajTrenere(_treneri);
        }

        public string CollectionName { get; set; } = "ListaTrenera";

        private ArrayList treneri = new ArrayList();

        public int Count { get { return treneri.Count; } }

        public object SyncRoot { get { return this; } }

        public bool IsSynchronized { get { return false; } }

        public void CopyTo(Array array, int index)
        {
            treneri.CopyTo(array, index);
        }

        public Trener this[int index]
        {
            get { return (Trener)treneri[index]; }
        }

        public IEnumerator GetEnumerator()
        {
            return treneri.GetEnumerator();
        }

        private void DodajTrenere(List<Trener> _sellers)
        {
            foreach (Trener a in _sellers)
            {
                treneri.Add(a);
            }
        }

        public void Add(Trener a)
        {
            treneri.Add(a);
        }

        public List<Trener> DobaviTrenere()
        {
            List<Trener> _treneri = new List<Trener>();

            foreach (var a in treneri)
            {
                _treneri.Add(a as Trener);
            }

            return _treneri;
        }
    }
}