using Microsoft.EntityFrameworkCore;
using Questioning.Contracts;

namespace Questioning.Persistance;

public class ExamDbContext : DbContext
{
    public required DbSet<Exam> Exams { get; set; }
    public required DbSet<Question> Questions { get; set; }
    public required DbSet<Answer> Answers { get; set; }
    public required DbSet<QuestionResult> QuestionResults { get; set; }
    public required DbSet<ExamResult> ExamResults { get; set; }

    public ExamDbContext(DbContextOptions<ExamDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}

public class DataSeeder(ExamDbContext context)
{
    public void Seed()
    {
        if (context.Exams.Any())
            return;
        var exam = new Exam()
        {
            Description = "A exam about code quality metrics in C#",
            Name = "Code Quality Metrics",
            Questions = new()
            {
                new()
                {
                    Value = "What is cyclomatic complexity?",
                    QuestionType = Question.Type.MultipleChoice,
                    PossibleAnswers = new()
                    {
                        new()
                        {
                            Value =
                                "A metric that measures the number of linearly independent paths through a program's source code",
                            IsCorrect = true
                        },
                        new()
                        {
                            Value = "A metric that measures the number of lines of code in a program",
                            IsCorrect = false
                        },
                        new()
                        {
                            Value = "A metric that measures the number of methods in a class",
                            IsCorrect = false
                        }
                    }
                },
                new Question()
                {
                    Value = "What is the purpose of the SOLID principles?",
                    QuestionType = Question.Type.MultipleChoice,
                    PossibleAnswers = new()
                    {
                        new()
                        {
                            Value = "To make software easier to maintain and extend",
                            IsCorrect = true
                        },
                        new()
                        {
                            Value = "To make software run faster",
                            IsCorrect = false
                        },
                        new()
                        {
                            Value = "To make software more secure",
                            IsCorrect = false
                        }
                    }
                },
            }
        };
        context.Exams.Add(exam);
        context.SaveChanges();
    }
}