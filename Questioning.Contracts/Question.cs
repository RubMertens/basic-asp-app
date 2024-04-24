using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questioning.Contracts;

public class Question
{
    public int Id { get; set; }
    public string Value { get; set; } = string.Empty;
    public List<Answer> PossibleAnswers { get; set; } = new();
    public QuestionType QuestionType { get; set; }
    public int ExamId { get; set; }
    public Exam Exam { get; set; }
}

public enum QuestionType
{
    None = 0,
    SingleChoice = 1,
    MultipleChoice = 2,
}