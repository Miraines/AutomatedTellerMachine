using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.Lections;
using Itmo.ObjectOrientedProgramming.Lab2.MaterialsCreate;
using Itmo.ObjectOrientedProgramming.Lab2.SubjectBuilderInterfaces;
using Itmo.ObjectOrientedProgramming.Lab2.SubjectBuilders;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects;
using Itmo.ObjectOrientedProgramming.Lab2.User;

namespace Itmo.ObjectOrientedProgramming.Lab2;

public class Program
{
    public static void Main()
    {
        // 1. Создаем экземпляр DefaultUserCreator
        UserCreator userCreator = new DefaultUserCreator();

        // 2. Создаем пользователя, передавая имя
        string userName = "John Doe";
        string userNames = "John Doe2";
        IUser author = userCreator.CreateUser(userName);
        IUser notauthor = userCreator.CreateUser(userNames);

        // Создаем лекцию с помощью LectionCreator
        string lectionTitle = "Introduction to Programming";
        string lectionDescription = "A beginner's guide to programming.";
        string lectionContent = "Content of the lection goes here.";
        int lectionPoints = 10;

        var lectionCreator =
            new LectionCreator(lectionTitle, author, lectionDescription, lectionContent, lectionPoints);
        IMaterials lection = lectionCreator.CreateMaterials();

        // Создаем лабораторную работу с помощью LabworkCreator
        string labworkTitle = "Lab 1: Basics of C#";
        string labworkDescription = "This lab covers the basics of the C# programming language.";
        int labworkPoints = 15;

        var labworkCreator = new LabworkCreator(labworkTitle, author, labworkDescription, labworkPoints);
        IMaterials labwork = labworkCreator.CreateMaterials();

        // 2. Создаем экземпляр фабрики
        ISubjectFactory factory = new SubjectFactory();

        // 3. Создаем экземпляр директора с фабрикой
        var director = new SubjectDirectorWithFactory(factory);

        // 4. Создаем SubjectBuilder и настраиваем его
        SubjectBuilder builder = new SubjectBuilder()
            .SetName("Mathematics")
            .SetAuthor(author)
            .SetLabworks(new List<Labwork> { (Labwork)labwork })
            .SetLectures(new List<Lection> { (Lection)lection });

        // 5. Создаем предмет с экзаменом, передавая билдера
        int maxPoints = 75;
        ExamSubject examSubject = director.CreateExamSubject(builder, maxPoints);

        Subject clonedSubject = examSubject.Clone();

        // Проверка оригинального идентификатора
        Console.WriteLine($"Original Subject ID: {examSubject.Id}"); // Выводит идентификатор examSubject
        Console.WriteLine(
            $"Cloned Subject Original ID: {clonedSubject.OriginalSubjectId}"); // Выводит идентификатор оригинала
        Console.WriteLine(
            $"Cloned Subject ID: {clonedSubject.Id}"); // Выводит новый идентификатор, отличный от оригинального

        examSubject.Update(
            notauthor,
            "математика3",
            new List<Labwork> { (Labwork)labwork },
            new List<Lection> { (Lection)lection },
            maxPoints: 10);

        // Вывод информации
        Console.WriteLine($"Subject ID: {examSubject.Id}");
        Console.WriteLine($"Subject Name: {examSubject.Name}");
        Console.WriteLine($"Author ID: {examSubject.Author.Id}");
        Console.WriteLine($"Labwork ID: {((Labwork)labwork).Id}");
        Console.WriteLine($"Labwork Author ID: {((Labwork)labwork).Author.Id}");
        Console.WriteLine($"Lection ID: {((Lection)lection).Id}");
        Console.WriteLine($"Lection Author ID: {((Lection)lection).Author.Id}");
        Console.WriteLine($"Max Points for Exam: {examSubject.MaxPoints}");
    }
}