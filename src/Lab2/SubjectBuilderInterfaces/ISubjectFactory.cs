namespace Itmo.ObjectOrientedProgramming.Lab2.SubjectBuilderInterfaces;

public interface ISubjectFactory
{
    IExamSubjectBuilder CreateExamSubjectBuilder();

    ICreditSubjectBuilder CreateCreditSubjectBuilder();
}