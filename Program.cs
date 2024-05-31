// See https://aka.ms/new-console-template for more information


using CodeFirst.Models;

using (var context = new SchoolsDbContext())
{
    /*var courses = context.Courses.ToList();
    Console.WriteLine("Course count=" + courses.Count);
    */

    /*var course = context.Courses.Single(c => c.CourseId == 3);
    
    Console.WriteLine("Course with Id 3=" + course.Title);*/

    /*Course newCourse = new Course();
    newCourse.CourseId = 5;
    newCourse.Title = "Course 5";
    newCourse.Credits = 10;

    context.Courses.Add(newCourse);
    context.SaveChanges();*/

    var courseToDelete = context.Courses.Single(c => c.CourseId == 5);

    Console.WriteLine("Course count=" + context.Courses.Count());

    context.Courses.Remove(courseToDelete);
    context.SaveChanges();

    Console.WriteLine("Course count=" + context.Courses.Count());

}

Console.ReadLine();