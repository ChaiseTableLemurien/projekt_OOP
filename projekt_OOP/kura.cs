using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_OOP
{
    public class Kura
    {
        public int ID;
        public string gender;
        public int age;
        public double weight;
        public int? eggs_laid;

        public Kura(string gender, int age, double weight, int? eggs_laid)
        {

            this.gender = gender;
            this.age = age;
            this.weight = weight;
            this.eggs_laid = eggs_laid;
        }

    }
}
