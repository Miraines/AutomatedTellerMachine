using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.Lections;
using Itmo.ObjectOrientedProgramming.Lab2.MaterialsCreate;
using Itmo.ObjectOrientedProgramming.Lab2.Repositories;
using Itmo.ObjectOrientedProgramming.Lab2.SubjectBuilderInterfaces;
using Itmo.ObjectOrientedProgramming.Lab2.SubjectBuilders;
using Itmo.ObjectOrientedProgramming.Lab2.User;
using Xunit;

namespace Lab2.Tests;

public class EducationalTests
{
    [Fact]
    public void NonAuthor_Cannot_Update_Lection()
    {
        var userFactory = new DefaultUserFactory();
        var materialsFactory = new MaterialsFactory();

        IUser author = userFactory.CreateUser("Автор Лекции");
        IUser nonAuthor = userFactory.CreateUser("НеАвтор");

        var lectionRepository = new LectionRepository();
        Lection lection = materialsFactory.CreateLection("Лекция 1", author, "Описание", "Содержание", 10);
        lectionRepository.Add(lection);

        Assert.Throws<UnauthorizedAccessException>(() =>
            lection.Update(
                "Обновленное название",
                "Обновленное описание",
                "Обновленное содержание",
                15,
                nonAuthor));
    }

    [Fact]
    public void NonAuthor_Cannot_Update_Labwork()
    {
        var userFactory = new DefaultUserFactory();
        var materialsFactory = new MaterialsFactory();

        IUser author = userFactory.CreateUser("Автор Лабораторной");
        IUser nonAuthor = userFactory.CreateUser("НеАвтор");

        var labworkRepository = new LabworkRepository();
        Labwork labwork = materialsFactory.CreateLabwork("Лабораторная 1", author, "Описание", 20);
        labworkRepository.Add(labwork);

        Assert.Throws<UnauthorizedAccessException>(() =>
            labwork.Update(
                "Обновленное название",
                "Обновленное описание",
                25,
                nonAuthor));
    }

    [Fact]
    public void Cloned_Lection_Has_Original_Id()
    {
        var userFactory = new DefaultUserFactory();
        var materialsFactory = new MaterialsFactory();

        IUser author = userFactory.CreateUser("Автор Лекции");
        Lection originalLection = materialsFactory.CreateLection("Лекция 1", author, "Описание", "Содержание", 10);

        Lection clonedLection = originalLection.Clone();

        Assert.Equal(originalLection.Id, clonedLection.OriginalId);
        Assert.NotEqual(originalLection.Id, clonedLection.Id);
    }

    [Fact]
    public void Cloned_Labwork_Has_Original_Id()
    {
        var userFactory = new DefaultUserFactory();
        var materialsFactory = new MaterialsFactory();

        IUser author = userFactory.CreateUser("Автор Лабораторной");
        Labwork originalLabwork = materialsFactory.CreateLabwork("Лабораторная 1", author, "Описание", 20);

        Labwork clonedLabwork = originalLabwork.Clone();

        Assert.Equal(originalLabwork.Id, clonedLabwork.OriginalId);
        Assert.NotEqual(originalLabwork.Id, clonedLabwork.Id);
    }

    [Fact]
    public void Creating_Subject_With_Invalid_Total_Points_Throws_Exception()
    {
        var userFactory = new DefaultUserFactory();
        var materialsFactory = new MaterialsFactory();
        var subjectFactory = new SubjectFactory();

        IUser author = userFactory.CreateUser("Автор Предмета");

        Lection lection = materialsFactory.CreateLection(
            "Лекция 1",
            author,
            "Описание",
            "Содержание",
            40);
        Labwork labwork = materialsFactory.CreateLabwork(
            "Лабораторная 1",
            author,
            "Описание",
            50);

        IExamSubjectBuilder examSubjectBuilder = subjectFactory.CreateExamSubjectBuilder();

        var lections = new List<Lection> { lection };
        var labworks = new List<Labwork> { labwork };

        Assert.Throws<InvalidOperationException>(() =>
            examSubjectBuilder
                .SetName("Неверный предмет")
                .SetAuthor(author)
                .SetLectures(lections)
                .SetLabworks(labworks)
                .SetMaxPoints(50)
                .Build());
    }

    [Fact]
    public void Adding_Duplicate_User_Throws_Exception()
    {
        var userFactory = new DefaultUserFactory();
        var userRepository = new UserRepository();

        IUser user = userFactory.CreateUser("Дубликат Пользователя");
        userRepository.Add(user);

        Assert.Throws<ArgumentException>(() => userRepository.Add(user));
    }

    [Fact]
    public void Get_User_By_Invalid_Id_Returns_Null()
    {
        var userRepository = new UserRepository();
        var invalidId = Guid.NewGuid();

        IUser? user = userRepository.GetById(invalidId);

        Assert.Null(user);
    }

    [Fact]
    public void Get_Labwork_By_Invalid_Id_Returns_Null()
    {
        var labworkRepository = new LabworkRepository();
        var invalidId = Guid.NewGuid();

        Labwork? labwork = labworkRepository.GetById(invalidId);

        Assert.Null(labwork);
    }
}