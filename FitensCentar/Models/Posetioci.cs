using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitensCentar.Models
{
    public class Posetioci : ICollection
    {
        public Posetioci()
        {

        }

        public Posetioci(List<Posetilac> _posetioci)
        {
            DodajPosetioce(_posetioci);
        }

        public string CollectionName { get; set; } = "Posetioci";

        private ArrayList posetioci = new ArrayList();

        public int Count { get { return posetioci.Count; } }

        public object SyncRoot { get { return this; } }

        public bool IsSynchronized { get { return false; } }

        public void CopyTo(Array array, int index)
        {
            posetioci.CopyTo(array, index);
        }

        public Posetilac this[int index]
        {
            get { return (Posetilac)posetioci[index]; }
        }

        public IEnumerator GetEnumerator()
        {
            return posetioci.GetEnumerator();
        }

        private void DodajPosetioce(List<Posetilac> _posetioci)
        {
            foreach (Posetilac a in _posetioci)
            {
                posetioci.Add(a);
            }
        }

        public void Add(Posetilac a)
        {
            posetioci.Add(a);
        }

        public List<Posetilac> DobaviPosetioce()
        {
            List<Posetilac> _posetioci = new List<Posetilac>();

            foreach (var a in posetioci)
            {
                _posetioci.Add(a as Posetilac);
            }

            return _posetioci;
        }
    }
}