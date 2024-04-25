namespace Questioning.Domain;

public class QuestionResult
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public required Question Question { get; set; }
    public List<Answer> Answers { get; set; } = new();
}