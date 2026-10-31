using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace StudentCodeFirstDatabaseApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new StudentContext())
            {
                // Ask user to input students first and last name as well as their GPA
                Console.WriteLine("Enter the First Name for the new Student:");
                var firstName = Console.ReadLine();
                
                Console.WriteLine("Enter the Last Name for the new Student:");
                var lastName = Console.ReadLine();
                
                bool isValidGPA = false;
                double gpa = 0.0;

                //Ensure valid entry for GPA
                while (!isValidGPA)
                {
                    Console.WriteLine("Enter student's GPA (0.0 - 4.0):");
                    var input = Console.ReadLine();

                    if(double.TryParse(input, out gpa) && gpa >= 0 && gpa <= 4.0)
                    {
                        isValidGPA = true;
                    } else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number in range.");
                    }
                }

                //Create a new student object with user entered data
                var student = new Student
                {
                    FirstName = firstName,
                    LastName = lastName,
                    GPA = gpa
                };

                //Add the object to the Student database and save changes
                db.Students.Add(student);
                db.SaveChanges();

                //Query and display all Students from the database
                var query = from s in db.Students
                            orderby s.LastName
                            select s;

                Console.WriteLine("\nAll students in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.FirstName + " " + item.LastName + "\tGPA: " + item.GPA);
                }

                //Pause application until user presses a key
                Console.ReadKey();

            }
        }
    }
    
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double GPA { get; set; }
    }

    public class StudentContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
    }
}

