using Itmo.ObjectOrientedProgramming.Lab2.SubjectBuilderInterfaces;

namespace Itmo.ObjectOrientedProgramming.Lab2.SubjectBuilders;

public class SubjectFactory : ISubjectFactory
{
    public IExamSubjectBuilder CreateExamSubjectBuilder()
    {
        return new ExamSubjectBuilder();
    }

    public ICreditSubjectBuilder CreateCreditSubjectBuilder()
    {
        return new CreditSubjectBuilder();
    }
}