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
    }
}