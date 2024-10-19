using Itmo.ObjectOrientedProgramming.Lab2.SubjectBuilders;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects;

namespace Itmo.ObjectOrientedProgramming.Lab2.SubjectBuilderInterfaces;

public interface ISubjectFactory
{
    ExamSubject CreateExamSubject(SubjectBuilder builder, int maxPoints);

    CreditSubject CreateCreditSubject(SubjectBuilder builder, int minPointsRequired);
}