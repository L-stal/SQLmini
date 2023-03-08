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
            Console.WriteLine(" 1 edit hours");
            Console.WriteLine(" 2 Add new person");
            Console.WriteLine(" 3 Create project");
            Console.WriteLine(" 4 add hours");
            string command = Console.ReadLine();
            switch (command)
            {
                case "1":
                    EditHours();
                    break;
                case "2":
                    CreatePerson();
                    break;
                case "3":
                    CreateProject();
                    break;
                case "4":
                    AddHours();
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
    internal static void AddHours()
    {
        ProjectPersonModel addedHours = new ProjectPersonModel();
        Console.WriteLine("To what project to you want to add hours to ?");
        Console.Write("Project: ");
        string project = Helper.FormatString(Console.ReadLine());
        if (!DataAccess.LoadProjectByName(project))
        {
            Console.WriteLine("Something went wrong");
            return;
        }
        else
        {
            Console.WriteLine(project);
            int projectId = DataAccess.GetProjectId(project);
            Console.WriteLine(projectId);
            Console.Write("Enter your name: ");
            string name = Helper.FormatString(Console.ReadLine());
            if (!DataAccess.CheckPerson(name))
            {
                Console.WriteLine("Something went wrong");
                return;
            }
            else
            {
                int personId = DataAccess.GetPersonId(name);
                Console.WriteLine(name);
                Console.WriteLine("Enter the amount of hours.");
                Console.Write("Hours: ");
                bool tryHours = int.TryParse(Console.ReadLine(), out int addHours);
                if (!tryHours)
                {
                    Console.WriteLine("Hours cant be 0.");
                }
                else
                {
                    addedHours.hours = addHours;
                    addedHours.project_id = projectId;
                    addedHours.person_id = personId;
                    DataAccess.AddHours(addedHours);
                    Console.WriteLine(addedHours.hours);
                }
            }
        }
    }
    internal static void EditHours()
    {
        int i = 1;
        List<ProjectPersonModel> projectInfo = new List<ProjectPersonModel>();
        Console.WriteLine("Enter your name");
        Console.Write("Name: ");
        string name = Helper.FormatString(Console.ReadLine());
        if (!DataAccess.CheckPerson(name))
        {
            Console.WriteLine("Something went wrong");
            return;
        }
        else
        {
            int personID = DataAccess.GetPersonId(name);
            projectInfo = DataAccess.ProjectSelection(personID);
            foreach (var item in projectInfo)
            {
                //Skriv även ut timmar !!!!!
                Console.WriteLine(i++ + item.project_name + item.id);
                Console.ReadKey();

            }
            Console.WriteLine("Choose a project you want to edit hours on");
            string choice = Console.ReadLine();
            bool choiceCheck = int.TryParse(choice, out int choiceInt);
            if (!choiceCheck)
            {
                Console.WriteLine("plese selecet a project from aboive");
                return;

            }
            else
            {
                Console.Write("Update hours: ");
                string newHours = Console.ReadLine();
                bool hoursChecked = int.TryParse(newHours, out int hoursInt);
                if (!hoursChecked)
                {
                    Console.WriteLine("Error");
                    return;
                }
                else
                {
                    projectInfo[choiceInt - 1].hours = hoursInt;
                    DataAccess.UpdateHours(projectInfo[choiceInt - 1]);
                    Console.WriteLine(hoursInt + projectInfo[choiceInt - 1].project_name);
                    Console.ReadKey();
                }

            }

        }

    }
}