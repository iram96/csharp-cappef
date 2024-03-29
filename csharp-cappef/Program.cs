﻿// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

Console.WriteLine("Hello, World!");

static System.Collections.Generic.IEnumerable<int> Power(int number, int exponent)
{
    int result = 1;

    for (int i = 0; i < exponent; i++)
    {
        result = result * number;
        yield return result;
    }
};

static Func<int> accumul(int number)
{  //Closure
    int result = 1;

    return () =>
    {
        result = result * number;
        return result;
    };
}



foreach (var n in Power(2, 16))
{
    Console.WriteLine(n);
}


return;

using (SchoolContext db = new SchoolContext())
{
    // Create
    Student nuovoStudente =
        new Student { Name = "Francesco", Surname = "Cossiga", Email = "coxy@gmail.com" };
    db.Add(nuovoStudente);
    db.SaveChanges();

    // Read
    Console.WriteLine("Ottenere lista di Studenti");
    List<Student> students = db.Students
       .OrderBy(student => student.Name).ToList<Student>();
}

[Table("student")]
[Index(nameof(Email), IsUnique = true)]
public class Student
{
    [Key]
    public int StudentId { get; set; }
    [Required]
    public string Name { get; set; }
    public string Surname { get; set; }
    [Column("student_email")]
    public string Email { get; set; }

    public List<Course> FrequentedCourses { get; set; }
}

[Table("course")]
public class Course
{
    [Key]
    public int CourseId { get; set; }
    public string Name { get; set; }

    public CourseImage CourseImage { get; set; }

    public List<Student> StudentsEnrolled { get; set; }

    public List<Review> Reviews { get; set; }
}


[Table("course_image")]
public class CourseImage
{
    [Key]
    public int CourseImageId { get; set; }
    public byte[] Image { get; set; }
    public string Caption { get; set; }

    public int CourseId { get; set; }
    public Course Course { get; set; }
}


[Table("review")]
public class Review
{

    [Key]
    public int ReviewId { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }

    public int StudentId { get; set; }
    public Student Student { get; set; }
}

public class SchoolContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseImage> CourseImages { get; set; }
    public DbSet<Review> Reviews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=University;Integrated Security=True;Pooling=False");
    }
}

