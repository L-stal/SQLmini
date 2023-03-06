using SqlMini;
using System.Text.RegularExpressions;

internal class Program
{

    //Lägg in personer i projectet
    private static void Main(string[] args)
    {
        Console.WriteLine("SQL MINI");
        bool menu = true;
        while (menu)
        {
            Console.WriteLine("Choose option");
            Console.WriteLine(" 1 Get person data/add hours");
            Console.WriteLine(" 2 Add new person");
            Console.WriteLine(" 3 Create project");
            string command = Console.ReadLine();
            switch (command)
            {
                case "1":
                    AddHours();
                    break;
                case "2":
                    CreatePerson();
                    break;
                case "3":
                    CreateProject();
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
        PersonModel newPerson = new PersonModel();
        Console.Write("Please enter the name of the person: ");
        string personName = Helper.FormatString(Console.ReadLine());
        if (!Regex.IsMatch(personName, @"^[a-öA-Ö]+$"))
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
        ProjectModel newProject = new ProjectModel();
        Console.Write("Enter the name of the project: ");
        string projectName = Helper.FormatString(Console.ReadLine());
        if (!Regex.IsMatch(projectName, @"^[a-öA-Ö]+$"))
        {
            Console.WriteLine("Something went wrong bakka uwu");
            Console.ReadKey();
        }
        else
        {
            newProject.project_name = projectName;
            DataAccess.CreateProject(newProject);
            Console.WriteLine(newProject.project_name);
            Console.ReadKey();
        }
    }
    // DU ÄR HÄR , FÖRSÖK SICKA IN PROEJECT NAME OCH HOURS!!!!!!!!!! 
    internal static void AddHours()
    {
        ProjectPersonModel hours = new ProjectPersonModel();
        Console.WriteLine("To what project to you want to add hours to ?");
        Console.Write("Project: ");
        string project = Console.ReadLine();
        if (!DataAccess.LoadProjcetByName(project))
        {
            Console.WriteLine("Something went wrong");
            return;
        }
        else
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            if (!DataAccess.CheckPerson(name))
            {
                Console.WriteLine("Something went wrong");
                return;      
            }
            else
            {
                Console.WriteLine("Enter the amount of hours.");
                Console.Write("Hours: ");
                int addHours = int.Parse(Console.ReadLine());
                if (addHours <= 0) 
                {
                    Console.WriteLine("Hours cant be 0.");
                }
                else
                {
                    hours.hours = addHours;
                    DataAccess.AddHours(project,hours);
                    Console.WriteLine(hours.hours);
                }
            }
        }
    }
}