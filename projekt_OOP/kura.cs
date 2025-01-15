using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projekt_OOP.Interfaces;

namespace projekt_OOP
{
    public class Kura : IKura
    {
        public string gender { get; set; }
        public int age { get; set; }
        public double weight { get; set; }
        public int? eggs_laid { get; set; }

        public Kura(string gender, int age, double weight, int? eggs_laid)
        {

            this.gender = gender;
            this.age = age;
            this.weight = weight;
            this.eggs_laid = eggs_laid;
        }

    }
}
