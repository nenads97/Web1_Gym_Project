using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitensCentar.Models
{
    public static class ApsolutnaPutanjaKreiranje
    {
        private static string XmlPodaciPutanja { get; set; } = @"C:\Users\Lenovo\Desktop\Nenad\WEB1\FitensCentar\FitensCentar\Data\";

        public static string NapraviApsolutnuPutanjuDoXmlPodataka(string filename)
        {
            if (!filename.Contains(".xml"))
                filename += ".xml";
            return XmlPodaciPutanja + filename;
        }
    }
}