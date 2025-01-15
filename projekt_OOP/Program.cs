// See https://aka.ms/new-console-template for more information
using Mysqlx.Session;
using Org.BouncyCastle.Crypto.Digests;
using projekt_OOP;
using System;
using System.Threading.Channels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

static void Main()
{

    bool continueRunning = true;

    while (continueRunning)
    {
        try 
        {

       
            Console.Clear(); // Czyszczenie konsoli przed każdym wyświetleniem menu
            Console.WriteLine("╔═══════════════════════════════╗");
            Console.WriteLine("║           Menu Główne         ║");
            Console.WriteLine("╠═══════════════════════════════╣");
            Console.WriteLine("║ 1. Dodaj kurę                 ║");
            Console.WriteLine("║ 2. Usuń kurę                  ║");
            Console.WriteLine("║ 3. Edytuj kurę                ║");
            Console.WriteLine("║ 4. Dodaj kurnik               ║");
            Console.WriteLine("║ 5. Usuń kurnik                ║");
            Console.WriteLine("║ 6. Wyświetl dane              ║");
            Console.WriteLine("║ 7. Zakończ                    ║");
            Console.WriteLine("╚═══════════════════════════════╝");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddHen();
                    break;
                case "2":
                    RemoveHen();
                    break;
                case "3":
                    EditHen();
                    break;
                case "4":
                    AddCoop();
                    break;
                case "5":
                    RemoveCoop();
                    break;
                case "6":
                    Search();
                    break;
                case "7":
                    continueRunning = false;
                    Console.WriteLine("╔═══════════════════════╗");
                    Console.WriteLine("║ Zamykanie programu... ║");
                    Console.WriteLine("╚═══════════════════════╝");
                    break;
                default:
                    Console.WriteLine("╔═══════════════════════════════════╗");
                    Console.WriteLine("║ Nieznana opcja. Wybierz ponownie. ║");
                    Console.WriteLine("╚═══════════════════════════════════╝");
                    break;
            }

            if (continueRunning)
            {
                Console.WriteLine("\nNaciśnij dowolny klawisz, aby kontynuować...");
                Console.ReadKey();
            }
        }
        catch (Exception ex)
        {

            Console.WriteLine("Wystąpił błąd. Szczegóły: " + ex.Message);
            Console.WriteLine("Wracam do menu głównego...");
            System.Threading.Thread.Sleep(7000); 
        }
    }
}

static void AddHen()
{
    string gndr;
    int age;
    double weight;
    int? eggs;
    int id_kurnik;

    Console.WriteLine("\nAby dodać kurę musisz podać jej parametry: ");
    Console.WriteLine("Podaj płeć kury, wpisz kura lub kogut:");

    string verify = Console.ReadLine();
    if(verify ==  "kura" || verify == "kogut")
    {
        gndr = verify;
    }
    else
    {
        Console.WriteLine("Ups, podałeś złą wartość, zacznij od nowa");
        return;
    }

    Console.WriteLine("Podaj wiek kury wyrażony w latach np. 0, 1, 2 ...");
    age =  Int32.Parse(Console.ReadLine());

    Console.WriteLine("Podaj wagę kury w kg, np. 0,4  2,1 ...");
    weight = Double.Parse(Console.ReadLine());

    Console.WriteLine("(Opcjonalne) Podaj ile jaj zniosła kura: ");
    string input = Console.ReadLine();
    if (int.TryParse(input, out int parsedValue))
    {
        eggs = parsedValue;
    }
    else
    {
        eggs = null;
    }
    Console.WriteLine("Na koniec umieśćmy kurę w kurniku, podaj ID kurnika: ");
    id_kurnik = int.Parse(Console.ReadLine());


    Kura temp_chicken = new Kura(gndr,age,weight,eggs);

    dbConnect con = new dbConnect();
    con.CreateConnection();
    con.InsertKura(temp_chicken, id_kurnik);
    con.CloseConnection();

    Console.WriteLine("╔═══════════════════╗");
    Console.WriteLine("║ Dodano kurę...    ║");
    Console.WriteLine("╚═══════════════════╝");
 
}

static void RemoveHen()
{
    string confirm;
    string confirm2;
    int id_kura;
    Console.WriteLine("Aby usunąć kurę, napierw upewnij się że masz jej ID");
    Console.WriteLine("Czy chcesz kontynuować? ");
    Console.WriteLine("Y/N?");
    confirm2 = Console.ReadLine();

    if (confirm2 == "n" || confirm2 == "N")
    {
        return;
    }

    Console.WriteLine("Podaj ID kury którą chcesz usunąć: ");
    id_kura = int.Parse(Console.ReadLine());

    Console.WriteLine($"Czy napewno chcesz usunąć kurę o ID {id_kura} ?");
    Console.WriteLine("Y/N?");
    confirm = Console.ReadLine();
    if (confirm == "y" || confirm == "Y")
    {
        dbConnect con = new dbConnect();
        con.CreateConnection();
        con.DeleteKura(id_kura);
        con.CloseConnection();

        Console.WriteLine("╔═══════════════════╗");
        Console.WriteLine("║ Usuwanięto kurę...║");
        Console.WriteLine("╚═══════════════════╝");
    }
    else
    {
        return ;
    }


 
    // Tutaj możesz dodać logikę usuwania kury z bazy danych
}

static void EditHen()
{
    int id_kury;
    int choice;
    bool continueRunning = true;
    Console.WriteLine("Podaj ID kury której parametry chcesz zmienić: ");
    id_kury = int.Parse(Console.ReadLine());

    dbConnect con = new dbConnect();
    con.CreateConnection();
    con.SelectByID(id_kury);
    con.CloseConnection();



    while (continueRunning)
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════╗");
        Console.WriteLine("║ Edycja kury...               ║");
        Console.WriteLine("║══════════════════════════════║");
        Console.WriteLine("║Który parametr chcesz zmienić?║");
        Console.WriteLine("║1. ID kurnika                 ║");
        Console.WriteLine("║2. Płeć kury (niezalecane)    ║");
        Console.WriteLine("║3. Liczba zniesionych jaj     ║");
        Console.WriteLine("║4. Waga kury                  ║");
        Console.WriteLine("║5. Zakończ                    ║");
        Console.WriteLine("╚══════════════════════════════╝");
        con.CreateConnection();
        con.SelectByID(id_kury);
        con.CloseConnection();
        choice = Int32.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                {
                    int new_id;
                    Console.WriteLine("Podaj nowe ID kurnika: ");
                    new_id = Int32.Parse(Console.ReadLine());
                    con.CreateConnection();
                    con.UpdateKura(id_kury,1,new_id);
                    con.CloseConnection();
                    break;
                }
            case 2:
                {

                    string new_gender;
                    Console.WriteLine("Wpisz nową płeć dla kury: (wpisz kura lub kogut");
                    string verify = Console.ReadLine();
                    if (verify == "kura" || verify == "kogut")
                    {
                        new_gender = verify;
                    }
                    else
                    {
                        Console.WriteLine("Ups, podałeś złą wartość, zacznij od nowa");
                        return;
                    }
                    con.CreateConnection();
                    con.UpdateKura(id_kury,2,new_gender);
                    con.CloseConnection();

                    break;
                }
            case 3:
                {

                    int new_eggs;
                    string new_eggsS;
                    Console.WriteLine("Podaj nową ilość zniesionych jaj: (aby ustawić wartość NULL, wciśnij enter) ");

                    string input = Console.ReadLine();
                    if (int.TryParse(input, out int parsedValue))
                    {
                        new_eggs = parsedValue;

                        con.CreateConnection();
                        con.UpdateKura(id_kury,3,new_eggs);
                        con.CloseConnection();

                    }
                    else
                    {
                        new_eggsS = "NULL";

                        con.CreateConnection();
                        con.UpdateKura(id_kury,3,new_eggsS);
                        con.CloseConnection();
                    }


                    break;
                }
            case 4:
                {
                    double new_weight;

                    Console.WriteLine("Podaj nową wagę kury: ( w formacie 1,5   2,5 itd.");
                    new_weight = double.Parse(Console.ReadLine());
                    con.CreateConnection();
                    con.UpdateKura(id_kury,4,new_weight);
                    con.CloseConnection();
                    break;
                }
            case 5:
                {
                    continueRunning = false;
                    break;
                }



        }
    }
}

static void AddCoop()
{
    string nazwa;
    string lokalizacja;
    Console.WriteLine("Podaj nazwę dla nowego kurnika: ");
    nazwa = Console.ReadLine();
    Console.WriteLine("Podaj miasto w którym znajduje się kurnik: ");
    lokalizacja = Console.ReadLine();

    if (string.IsNullOrEmpty(nazwa) || string.IsNullOrEmpty(lokalizacja))
        {
        Console.WriteLine("Nazwa i lokalizacja nie mogą być puste!");
        return;
        }
    else
    {
        Kurnik temp_kurnik = new Kurnik(nazwa, lokalizacja);

        dbConnect con = new dbConnect();
        con.CreateConnection();
        con.InsertKurnik(temp_kurnik);
        con.CloseConnection();

        Console.WriteLine("╔══════════════════════╗");
        Console.WriteLine("║ Dodano kurnik...     ║");
        Console.WriteLine("╚══════════════════════╝");
        // Tutaj możesz dodać logikę dodawania kurnika do bazy danych
    }


}

static void RemoveCoop()
{
    int id_kurnik;
    string confirm;
    string confirm2;

    Console.WriteLine("Aby usunąć kurnik, napierw upewnij się że masz jego ID");
    Console.WriteLine("Pamiętaj też o tym, że jeśli usuniesz kurnik to wraz z kurami do niego przypisanymi!");
    Console.WriteLine("Czy chcesz kontynuować? ");
    Console.WriteLine("Y/N?");
    confirm2 = Console.ReadLine();

    if (confirm2 == "n" || confirm2 == "N")
    {
        return;
    }

    Console.WriteLine("Podaj ID kurnika do usunięcia: ");
    id_kurnik = Int32.Parse(Console.ReadLine());

    Console.WriteLine($"Czy napewno chcesz usunąć kurnik o ID {id_kurnik} ?");
    Console.WriteLine("Y/N?");
    confirm = Console.ReadLine();
    if (confirm == "y" || confirm == "Y")
    {
        dbConnect con = new dbConnect();
        con.CreateConnection();
        con.DeleteKurnik(id_kurnik);
        con.CloseConnection();

        Console.WriteLine("╔═════════════════════╗");
        Console.WriteLine("║ Usunięto kurnik...  ║");
        Console.WriteLine("╚═════════════════════╝");
    }
    else
    {
        return;
    }



    // Tutaj możesz dodać logikę usuwania kurnika z bazy danych
}

static void Search()
{
    dbConnect con = new dbConnect();
    con.CreateConnection();

   
    Console.WriteLine("╔════════════════════════╗");
    Console.WriteLine("║ Wyszukiwanie danych... ║");
    Console.WriteLine("╚════════════════════════╝");

    con.Select();

    con.CloseConnection();
    // Tutaj możesz dodać logikę wyszukiwania danych w bazie danych
}

Main();