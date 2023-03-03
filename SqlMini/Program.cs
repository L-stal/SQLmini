using SqlMini;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("SQL MINI");
        bool menu = true;
        while (menu)
        {
            Console.WriteLine("Choose option");
            Console.WriteLine(" 1 Get person data");
            Console.WriteLine(" 2 Add new person");
            Console.WriteLine(" 3 option 3");
            string command = Console.ReadLine();
            switch (command)
            {
                case "1":
                    GetPersonInfo();
                    break;
                case "2":
                    CreatePerson();
                    break;
                case "3":
                    //VG uppgift?
                    break;
                default:
                    Console.WriteLine("Choose option above");
                    Console.ReadKey();
                    break;

            }
            Console.ReadKey();
        }
       
    }
    internal static void GetPersonInfo()
    {
        List<PersonModel> persons = DataAccess.GetPersonData();
        foreach (PersonModel person in persons)
        {
            Console.WriteLine(person.person_name);
        }

    }

    internal static void CreatePerson()
    {
        PersonModel newPerson =new PersonModel();
        Console.WriteLine("Please enter the name of the person");
        string personName = Helper.FormatString(Console.ReadLine());
        if(!Regex.IsMatch(personName, @"^[a-öA-Ö]+$"))
        {
            Console.WriteLine("Please only use letters when adding a new person");
            Console.ReadKey();
        }
        else
        {
            newPerson.person_name = personName;
            DataAccess.CreatePerson(newPerson);
            Console.WriteLine(newPerson.person_name);
            Console.ReadKey();
        }

    }
    internal static void CreateProject()
    {


    }
}