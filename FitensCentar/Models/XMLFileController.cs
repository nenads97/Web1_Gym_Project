using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace FitensCentar.Models
{
    public class XMLFileController
    {
        #region Serijalizuj

        static XmlSerializer vlasnikSrlz = new XmlSerializer(typeof(Vlasnici), new XmlRootAttribute("NizVlasnika"));
        static XmlSerializer trenerSrlz = new XmlSerializer(typeof(Treneri), new XmlRootAttribute("NizTrenera"));
        static XmlSerializer posetilacSrlz = new XmlSerializer(typeof(Posetioci), new XmlRootAttribute("NizPosetioca"));

        public static void SerializujVlasnike(Vlasnici vlasnici)
        {
            TextWriter writer = new StreamWriter(ApsolutnaPutanjaKreiranje.NapraviApsolutnuPutanjuDoXmlPodataka("Vlasnici.xml"));
            posetilacSrlz.Serialize(writer, vlasnici);
            writer.Close();
        }

        public static void SerializujTrenere(Treneri treneri)
        {
            TextWriter writer = new StreamWriter(ApsolutnaPutanjaKreiranje.NapraviApsolutnuPutanjuDoXmlPodataka("Treneri.xml"));
            posetilacSrlz.Serialize(writer, treneri);
            writer.Close();
        }

        public static void SerializujPosetioce(Posetioci posetioci)
        {
            TextWriter writer = new StreamWriter(ApsolutnaPutanjaKreiranje.NapraviApsolutnuPutanjuDoXmlPodataka("Posetioci.xml"));
            posetilacSrlz.Serialize(writer, posetioci);
            writer.Close();
        }
        #endregion

        #region Deserijalizuj
        public static Vlasnici DeserijalizujVlasnike()
        {
            Vlasnici vlasnici = null;

            if(File.Exists(ApsolutnaPutanjaKreiranje.NapraviApsolutnuPutanjuDoXmlPodataka("Vlasnici.xml")))
            {
                FileStream fileStream = new FileStream(ApsolutnaPutanjaKreiranje.NapraviApsolutnuPutanjuDoXmlPodataka("Vlasnici.xml"), FileMode.Open);
                vlasnici = (Vlasnici)vlasnikSrlz.Deserialize(fileStream);

            }
            return vlasnici;
        }

        public static Posetioci DeserijalizujPosetioce()
        {
            Posetioci posetioci = null;

            if (File.Exists(ApsolutnaPutanjaKreiranje.NapraviApsolutnuPutanjuDoXmlPodataka("Posetioci.xml")))
            {
                FileStream fileStream = new FileStream(ApsolutnaPutanjaKreiranje.NapraviApsolutnuPutanjuDoXmlPodataka("Posetioci.xml"), FileMode.Open);
                posetioci = (Posetioci)posetilacSrlz.Deserialize(fileStream);

            }
            return posetioci;
        }

        public static Treneri DeserijalizujTrenere()
        {
            Treneri treneri = null;

            if (File.Exists(ApsolutnaPutanjaKreiranje.NapraviApsolutnuPutanjuDoXmlPodataka("Treneri.xml")))
            {
                FileStream fileStream = new FileStream(ApsolutnaPutanjaKreiranje.NapraviApsolutnuPutanjuDoXmlPodataka("Treneri.xml"), FileMode.Open);
                treneri = (Treneri)trenerSrlz.Deserialize(fileStream);

            }
            return treneri;
        }

        #endregion
    }
}