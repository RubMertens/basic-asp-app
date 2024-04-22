namespace Questioning.Contracts;

public class Question
{
    public int Id { get; set; }
    public string Value { get; set; } = string.Empty;
    public List<Answer> PossibleAnswers { get; set; } = new();
    public Type QuestionType { get; set; }
    public int ExamId { get; set; }

    public enum Type
    {
        None = 0,
        SingleChoice = 1,
        MultipleChoice = 2,
    }
}