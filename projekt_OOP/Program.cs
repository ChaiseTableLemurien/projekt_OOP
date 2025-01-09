// See https://aka.ms/new-console-template for more information
using projekt_OOP;

Console.WriteLine("Hello, World!");

Kura cziken1 = new Kura(1, "Kogut", 2, 2.5, null);

Kura cziken2 = new Kura(1, "pisklę", 1, 0.2, null);

cziken1.showChicken();


kurnik kurnik = new kurnik("Kurnik 1");

kurnik.DodajKure(cziken1);
kurnik.DodajKure(cziken2);

kurnik.WypiszKury();

Console.WriteLine("");



dbConnect con = new dbConnect("localhost", "kurnik_test", "root", "");
con.GetConnection();