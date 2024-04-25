using FluentValidation;
using FluentValidation.Results;
using Questioning.Contracts;
using Questioning.Persistance;

namespace Questioning.Core;

public class ExamManager(
    IValidator<Exam> examValidator,
    IValidator<Question> questionValidator,
    IValidator<Answer> answerValidator,
    IExamDbContext context
)
{

    public void DeleteExam(Exam exam)
    {

    }

    public IResult<Exam> CreateExam(Exam exam)
    {
        var validation = examValidator.Validate(exam);
        if (validation.IsValid)
        {
            context.Exams.Add(exam);
            context.SaveChanges();
        }

        return new Result<Exam>(exam, validation);
    }

    public IResult<Question> CreateQuestion(string name, List<Answer> possibleAnswers, QuestionType questionQuestionType)
    {
        var question = new Question
        {
            Value = name,
            PossibleAnswers = possibleAnswers,
            QuestionType = questionQuestionType
        };

        var validation = questionValidator.Validate(question);
        if (validation.IsValid)
        {
            context.Questions.Add(question);
            context.SaveChanges();
        }

        return new Result<Question>(question, validation);
    }

    public Result<Question> UpdateQuestion(Question question)
    {
        var validation = questionValidator.Validate(question);
        if (validation.IsValid)
        {
            context.Questions.Update(question);
            context.SaveChanges();
        }

        return new Result<Question>(question, validation);
    }
}

public class Result<T>(T value, ValidationResult validations) : IResult<T>
{
    public T Value { get; set; } = value;
    public ValidationResult Validations { get; set; } = validations;

    public bool IsValid => Validations.IsValid;
}

public interface IResult<T>
{
    public T Value { get; set; }
    public ValidationResult Validations { get; set; }
    public bool IsValid => Validations.IsValid;
}