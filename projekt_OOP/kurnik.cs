using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_OOP
{
    internal class kurnik
    {
        public int ID;
        public string Name;
        public string localization;
        public List<Kura> Kury { get; private set; } = new List<Kura>();


        public kurnik(string name)
        {
            Name = name;
        }
        public void DodajKure(Kura kura)
        {
            Kury.Add(kura);
        }

        public void UsunKure(Kura kura)
        {
            Kury.Remove(kura);
        }

        public void WypiszKury()
        {
            foreach (var kura in Kury)
            {
                Console.WriteLine(kura.gender);
            }
        }
    }

}
