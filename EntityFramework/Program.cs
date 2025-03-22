using EntityFrameworkLesson1.Entities;
using System.Linq;

namespace EntityFrameworkLesson1
{
    internal class Program
    {
        static private AppDbContext _context;

        static Student? InputStudent()
        {
            Console.Write("Name: ");
            string? name = Console.ReadLine();
            if (name is null)
            {
                Console.WriteLine("Name must be not null");
                Console.ReadKey();
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
                Console.ReadKey();
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

        static Student? SelectStudent()
        {
            Student? user_input = InputStudent();
            if (user_input is null)
                return null; 
            Student? selected_student = _context.Students.Where(s => s.Name == user_input.Name && s.Age == user_input.Age && s.Email == user_input.Email).FirstOrDefault();
            if (selected_student is null)
            {
                Console.WriteLine("Student not found");
                ConsolePaus();
                return null;
            }
            return selected_student;
        }

        static Student? UpdateStudent()
        {
            Student? selected_student = SelectStudent();
            if (selected_student is null)
                return null;
            Console.WriteLine("Enter new data:");   
            Student? user_input = InputStudent();
            if (user_input is null)
                return null;
            selected_student.Name = user_input.Name;
            selected_student.Age = user_input.Age;
            selected_student.Email = user_input.Email;
            return selected_student;
        }

        static Student? DeleteStudent()
        {
            Student? selected_student = SelectStudent();
            if (selected_student is null)
                return null;
            _context.Students.Remove(selected_student);
            _context.SaveChanges();
            return selected_student;
        }

        static void menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Add new student\n2. Show all students\n3. Update student\n4. Delete user\n5. Exit");
                string? choice = Console.ReadLine();
                switch (choice) 
                {
                    case "1":
                        Console.WriteLine("Adding new student.");
                        Student? new_student = InputStudent();
                        if (new_student is null)
                            continue;
                        _context.Students.Add(new_student);
                        _context.SaveChanges();
                        Console.WriteLine("Adding Successful!");
                        ConsolePaus();
                        break;
                    case "2":
                        Console.WriteLine("Students:");
                        var students = _context.Students;
                        foreach (Student student in students)
                            Console.WriteLine($"ID: {student.Id} NAME: {student.Name} AGE: {student.Age} EMAIL: {student.Email}");
                        ConsolePaus();
                        break;
                    case "3":
                        Console.WriteLine("Updating student.");
                        Student? updated_student = UpdateStudent();
                        if (updated_student is null)
                            continue;
                        _context.Students.Update(updated_student);
                        _context.SaveChanges();
                        Console.WriteLine("Updating Successful!");
                        ConsolePaus();
                        break;
                    case "4":
                        Console.WriteLine("Deleting student.");
                        Student? deleted_student = DeleteStudent();
                        if (deleted_student is null)
                            continue;
                        Console.WriteLine("Deleting Successful!");
                        ConsolePaus();
                        break;
                    case "5":
                        return;
                }
            }
            
        }

        static void DeleteDoublicates()
        {
            var studentsDb = _context.Students.ToList();
            var students = studentsDb
                .GroupBy(st => new { st.Name, st.Age, st.Email })
                .Select(s => s.First());

            _context.Students.RemoveRange(studentsDb.Except(students));
            _context.SaveChanges();
        }


        static void Main(string[] args)
        {
            _context = new AppDbContext();
            

            menu();
        }
    }
}
