internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("SQL MINI");
        bool menu = true;
        while (menu)
        {
            Console.WriteLine("Choose option");
            Console.WriteLine("option 1");
            Console.WriteLine("option 2");
            Console.WriteLine("option 3");
            string command = Console.ReadLine();
            switch (command)
            {
                case "1":
                    //Clock time
                    break;
                case "2":
                    //Create user/project
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
}