using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projekt_OOP.Interfaces;

namespace projekt_OOP
{
    public class Kurnik : IKurnik
    {
        public string Name { get; set; }
        public string Localization { get; set; }

        public Kurnik(string name, string localization)
        {
            Name = name;
            Localization = localization;
        }

    }

}
