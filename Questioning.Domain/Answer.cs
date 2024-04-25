using System.ComponentModel.DataAnnotations;

namespace Questioning.Domain;

public class Answer
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Value { get; set; }
    public bool IsCorrect { get; set; }
}