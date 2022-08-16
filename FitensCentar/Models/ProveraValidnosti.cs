using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitensCentar.Models
{
    public class Validator
    {
        private static Validator validator;

        private Validator()
        { }

        public static Validator Instanca()
        {
            if (validator == null)
            {
                validator = new Validator();
            }
            return validator;
        }

        public bool PrazanStringValidator (string str, out string poruka)
        {
            poruka = "";
            if (str == "")
            {
                poruka = "*Ovo polje ne sme biti prazno.";
                return true;
            }
            return false;
        }

        public bool SifraStringValidator(string str, out string poruka)
        {
            poruka = "";
            if (str.Length > 0 && str.Length < 8)
            {
                poruka = "*Lozinka mora sadržati minimalno 8 karaktera.";
                return false;
            }

            char[] niz = str.ToCharArray();
            for (int i = 0; i < niz.Length; i++)
            {
                if (!JesteRazmak(niz[i]) || !JesteSlovo(niz[i]) || !JesteBroj(niz[i]))
                {
                    poruka = "*Lozinka moze da sadrzi samo slova, brojeve i razmake.";
                    return false;
                }
            }
            return true;
        }

        public bool JesteRazmak(char c)
        {
            if (c == 32)
            {
                return true;
            }
            return false;
        }

        public bool JesteSlovo(char c)
        {
            if ((c >= 65 && c <= 90) || ( c >= 97 && c <= 122))
            {
                return true;
            }
            return false;
        }

        public bool JesteBroj(char c)
        {
            if (c >= 48 && c <= 57)
            {
                return true;
            }
            return false;
        }

        /*
        private string PronadjiTrenera (string username, string password)
        {
            if ((SingltonPristupPodacima.Instance()))
        }
        */
    }
}