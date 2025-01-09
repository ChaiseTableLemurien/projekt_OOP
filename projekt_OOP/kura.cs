using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_OOP
{
    internal class Kura
    {
        int ID;
        public string gender;
        int age;
        double weight;
        int? eggs_laid;

        public Kura(int iD, string gender, int age, double weight, int? eggs_laid)
        {
            ID = iD;
            this.gender = gender;
            this.age = age;
            this.weight = weight;
            this.eggs_laid = eggs_laid;
        }

        public void showChicken()
        {
            Console.WriteLine($"Płeć: {gender}, Wiek: {age}, Waga: {weight}, Zniesione jajka: {eggs_laid}");
        }

    }
}
