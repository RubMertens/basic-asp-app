using System.ComponentModel.DataAnnotations;

namespace Questioning.Contracts;

public class Answer
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Value { get; set; }
    public bool IsCorrect { get; set; }
}