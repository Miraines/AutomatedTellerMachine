using Itmo.ObjectOrientedProgramming.Lab2.SubjectBuilderInterfaces;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects;

namespace Itmo.ObjectOrientedProgramming.Lab2.SubjectBuilders;

public class SubjectFactory : ISubjectFactory
{
    public ExamSubject CreateExamSubject(SubjectBuilder builder, int maxPoints)
    {
        Subject subject = builder.Build();
        return new ExamSubject(subject.Name, subject.Author, subject.Labworks, subject.Lections, maxPoints);
    }

    public CreditSubject CreateCreditSubject(SubjectBuilder builder, int minPointsRequired)
    {
        Subject subject = builder.Build();
        return new CreditSubject(subject.Name, subject.Author, subject.Labworks, subject.Lections, minPointsRequired);
    }
}