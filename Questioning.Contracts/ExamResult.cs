namespace Questioning.Contracts;

public class ExamResult
{
    public int Id { get; set; }
    public int ExamId { get; set; }
    public required Exam Exam { get; set; }
    public List<QuestionResult> QuestionResults { get; set; } = new();
}