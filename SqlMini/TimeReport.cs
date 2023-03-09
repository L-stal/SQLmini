using System.Text.RegularExpressions;

namespace SqlMini
{
    public class TimeReport
    {
        public static void Run()
        {
            //Creates a simple swithc/case menu for the user
            Console.Clear();
            bool menu = true;
            while (menu)
            {
                Console.Clear();
                Console.WriteLine(" Time To Time Report");
                Console.WriteLine(" Choose option");
                Console.WriteLine(" [1] Person related options");
                Console.WriteLine(" [2] Project related options");
                Console.Write(" Select: ");
                //checks user option
                string command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        //Calls personMenu method
                        PersonMenu();
                        break;
                    case "2":
                        ProjectMenu();
                        break;
                    default:
                        //if user option does not exist run this code
                        Console.WriteLine(" Choose an option above");
                        Console.WriteLine(" Press [Any Key] to continue.");
                        Console.ReadKey();
                        break;

                }
            }

        }
        //PersonMenu and PrjoectMenu structure are tha same and the first menu, checks user input if exist ,runs method and prints out errors.
        internal static void PersonMenu()
        {
            Console.Clear();
            bool menu = true;
            while (menu)
            {
                Console.Clear();
                Console.WriteLine(" Person Menu");
                Console.WriteLine(" [1] Get Person info. \n [2] Add a new person. \n [3] Update existing person. \n [4] Go back");
                Console.Write(" Select: ");
                string command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        Console.Clear();
                        GetPersonInfo();
                        break;
                    case "2":
                        Console.Clear();
                        CreatePerson();
                        break;
                    case "3":
                        Console.Clear();
                        UpdatePerson();
                        break;
                    case "4":
                        menu = false;
                        break;
                    default:
                        Console.WriteLine(" Choose an option above");
                        Console.WriteLine(" Press [Any Key] to continue.");
                        Console.ReadKey();
                        break;
                }

            }
        }
        internal static void ProjectMenu()
        {
            Console.Clear();
            bool menu = true;
            while (menu)
            {
                Console.Clear();
                Console.WriteLine(" Project Menu");
                Console.WriteLine(" [1] Create a new project.\n [2] Add hours to a project.\n [3] Edit hours on project. \n [4] Update name on a existing project. \n [5] Go back");
                Console.Write(" Select: ");
                string command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        Console.Clear();
                        CreateProject();
                        break;
                    case "2":
                        Console.Clear();
                        AddHours();
                        break;
                    case "3":
                        Console.Clear();
                        EditHours();
                        break;
                    case "4":
                        Console.Clear();
                        UpdateProject();
                        break;
                    case "5":
                        menu = false;
                        break;
                    default:
                        Console.WriteLine(" Choose an option above");
                        Console.WriteLine(" Press [Any Key] to continue.");
                        Console.ReadKey();
                        break;
                }
            }
        }
        internal static void GetPersonInfo()
        {
            //Gets data from gerperson data and adds to person list
            List<PersonModel> persons = DataAccess.GetPersonData();
            Console.WriteLine(" Persons in database.");
            //foreach loop that prints out the name of each person in persons list
            foreach (PersonModel person in persons)
            {
                Console.WriteLine(" " + person.person_name);
            }
            Console.WriteLine(" Press [Any Key] to continue.");
            Console.ReadKey();

        }

        //Lets user creat person and send data to database
        internal static void CreatePerson()
        {
            PersonModel newPerson = new PersonModel();
            Console.Write(" Please enter the name of the person: ");
            //Help.FormatString formats inputs to uppcase first letter and everything after to lowercase
            string personName = Helper.FormatString(Console.ReadLine());
            //Regex checks if user only inputs letter and not anything else
            if (!Regex.IsMatch(personName, @"^[a-öA-Ö]+$"))
            {
                Console.WriteLine(" Please only use letters when adding a new person");
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
        //Same as above
        internal static void CreateProject()
        {
            ProjectModel newProject = new ProjectModel();
            Console.WriteLine(" Enter the name of the project.");
            Console.Write(" New project: ");
            string projectName = Helper.FormatString(Console.ReadLine());
            if (!Regex.IsMatch(projectName, @"^[a-öA-Ö]+$"))
            {
                Console.WriteLine(" Something went wrong");
                Console.ReadKey();
            }
            else
            {
                newProject.project_name = projectName;
                DataAccess.CreateProject(newProject);
                Console.WriteLine(" New project created: " + newProject.project_name);
                Console.ReadKey();
            }
        }
        internal static void AddHours()
        {
            ProjectPersonModel addedHours = new ProjectPersonModel();
            Console.WriteLine(" To what project to you want to add hours to ?");
            Console.Write(" Project: ");
            string project = Helper.FormatString(Console.ReadLine());
            //Checks if project exist in data base , if exist lets user continue
            if (!DataAccess.LoadProjectByName(project))
            {
                Console.WriteLine(" No project with that name exist");
                Console.WriteLine(" Press [Any Key] to continue.");
                Console.ReadKey();
            }
            else
            {
                //Sends in user input to get project id
                int projectId = DataAccess.GetProjectId(project);
                Console.Write(" Enter your name: ");
                //Checks if user exist in database
                string name = Helper.FormatString(Console.ReadLine());
                if (!DataAccess.CheckPerson(name))
                {
                    Console.WriteLine(" Something went wrong");
                    Console.WriteLine(" Press [Any Key] to continue.");
                    Console.ReadKey();
                }
                else
                {
                    int personId = DataAccess.GetPersonId(name);
                    Console.WriteLine(" Enter the amount of hours.");
                    Console.Write(" Hours: ");
                    //Wont let user register 0 hours
                    bool tryHours = int.TryParse(Console.ReadLine(), out int addHours);
                    if (!tryHours)
                    {
                        Console.WriteLine(" Hours cant be 0.");
                    }
                    else
                    {
                        Console.WriteLine($" {addedHours} added to {project}");
                        addedHours.hours = addHours;
                        addedHours.project_id = projectId;
                        addedHours.person_id = personId;
                        DataAccess.AddHours(addedHours);
                        Console.WriteLine(addedHours.hours);
                        Console.WriteLine(" Press [Any Key] to continue.");
                        Console.ReadKey();
                    }
                }
            }
        }
        internal static void EditHours()
        {
            //int is used to create menu options
            int i = 1;
            List<ProjectPersonModel> projectInfo = new List<ProjectPersonModel>();
            Console.WriteLine(" Enter your name");
            Console.Write(" Name: ");
            string name = Helper.FormatString(Console.ReadLine());
            //Checks if person exist in DB
            if (!DataAccess.CheckPerson(name))
            {
                Console.WriteLine(" Something went wrong");
                Console.WriteLine(" Press [Any Key] to continue.");
                Console.ReadKey();
            }
            else
            {
                //sends in person name to get project person have been working on
                int personID = DataAccess.GetPersonId(name);
                projectInfo = DataAccess.ProjectSelection(personID);
                foreach (var item in projectInfo)
                {
                    Console.WriteLine($" [{i++}] {item.project_name} Hours:{item.hours}");
                }
                Console.WriteLine(" Choose a project you want to edit hours on");
                string choice = Console.ReadLine();
                bool choiceCheck = int.TryParse(choice, out int choiceInt);
                if (!choiceCheck)
                {
                    Console.WriteLine(" Please select a project from above.");
                    Console.WriteLine(" Press [Any Key] to continue.");
                    Console.ReadKey();

                }
                else
                {
                    Console.Write(" Update hours: ");
                    string newHours = Console.ReadLine();
                    bool hoursChecked = int.TryParse(newHours, out int hoursInt);
                    if (!hoursChecked)
                    {
                        Console.WriteLine(" Error");
                        Console.WriteLine(" Press [Any Key] to continue.");
                        Console.ReadKey();
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
        //same structure as edit hours
        internal static void UpdatePerson()
        {
            int i = 1;
            List<PersonModel> persons = DataAccess.GetPersonData();
            Console.WriteLine(" Please select the person you want to update below.");
            foreach (var person in persons)
            {
                Console.WriteLine($" [{i++}] {person.person_name} ");
            }
            Console.Write(" Select: ");
            int.TryParse(Console.ReadLine(), out int selectedPerson);
            if (selectedPerson > 0 && selectedPerson <= persons.Count())
            {
                Console.WriteLine(" Enter the name to update to.");
                Console.Write(" New name: ");
                string newName = Helper.FormatString(Console.ReadLine());
                if (Regex.IsMatch(newName, @"^[a-öA-Ö]+$"))
                {
                    if (DataAccess.CheckPerson(newName))
                    {
                        Console.WriteLine(" Error ");
                        Console.WriteLine(" Press [Any Key] to continue.");
                        Console.ReadKey();

                    }
                    else
                    {
                        persons[selectedPerson - 1].person_name = newName;
                        DataAccess.UpdatePerson(persons[selectedPerson - 1]);
                        Console.WriteLine(" Name updated to " + newName);
                        Console.WriteLine(" Press [Any Key] to continue.");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine(" Please only user letters.");
                    Console.WriteLine(" Press [Any Key] to continue.");
                    Console.ReadKey();

                }
            }
            else
            {
                Console.WriteLine(" Invalid input.");
                Console.WriteLine(" Press [Any Key] to continue.");
                Console.ReadKey();
            }
        }
        internal static void UpdateProject()
        {
            int i = 1;
            List<ProjectModel> projects = DataAccess.GetProjectData();
            Console.WriteLine(" Please select the project you want to change name on");
            foreach (var project in projects)
            {
                Console.WriteLine($" [{i++}] {project.project_name}");
            }
            Console.Write(" Select: ");
            int.TryParse(Console.ReadLine(), out int selectedProject);
            if (selectedProject > 0 && selectedProject <= projects.Count())
            {
                Console.WriteLine(" Enter the new name of the project.");
                Console.Write(" New project name: ");
                string newProjectName = Helper.FormatString(Console.ReadLine());
                if (DataAccess.LoadProjectByName(newProjectName))
                {
                    Console.WriteLine(" Can't update new name to current name. ");
                }
                else
                {
                    Console.WriteLine(" Project name updated to " + newProjectName);
                    projects[selectedProject - 1].project_name = newProjectName;
                    DataAccess.UpdateProject(projects[selectedProject - 1]);
                    Console.WriteLine(" Press [Any Key] to continue.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine(" Invalid input");
                Console.ReadKey();
            }
        }


    } 
}
