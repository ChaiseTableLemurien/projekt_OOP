using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_OOP.Interfaces
{
    public interface IKura
    {
        string gender { get; set; }
        int age { get; set; }
        double weight { get; set; }
        int? eggs_laid { get; set; }
    }

}
