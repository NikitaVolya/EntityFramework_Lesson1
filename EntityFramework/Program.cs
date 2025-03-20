namespace EntityFrameworkLesson1
{
    internal class Program
    {
        static private AppDbContext _context;

        static Student? InputNewStudent()
        {
            Console.WriteLine("Adding new student.");
            Console.Write("Name: ");
            string? name = Console.ReadLine();
            if (name is null)
            {
                Console.WriteLine("Name must be not null");
                return null;
            }

            int age;
            try
            {
                Console.Write("Age: ");
                age = int.Parse(Console.ReadLine());
            } catch
            {
                Console.WriteLine("Age must be a number");
                return null;
            }

            Console.Write("Email: ");
            string? email = Console.ReadLine();
            return new Student { Name = name, Age = age, Email = email };
        }

        static void ConsolePaus()
        {
            Console.WriteLine("press any key to continue...");
            Console.ReadKey();
        }


        static void menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Add new student\n2. Show all students\n3. Exit");
                string? choice = Console.ReadLine();
                switch (choice) 
                {
                    case "1":
                        Student? new_student = InputNewStudent();
                        if (new_student is null)
                            continue;
                        _context.Students.Add(new_student);
                        _context.SaveChanges();
                        Console.WriteLine("Adding Successful!");
                        ConsolePaus();
                        break;
                    case "2":
                        Console.WriteLine("Students:");
                        foreach (Student student in _context.Students)
                            Console.WriteLine($"ID: {student.Id} NAME: {student.Name} AGE: {student.Age} EMAIL: {student.Email}");
                        ConsolePaus();
                        break;
                    case "3":
                        return;
                }
            }
            
        }

        static void Main(string[] args)
        {
            _context = new AppDbContext();

            /*var student = new Student
            {
                Name = "John",
                Age = 25,
                Email = null
            };
            context.Students.Add(student);
            context.SaveChanges();*/

            menu();
        }
    }
}
