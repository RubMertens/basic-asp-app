using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Questioning.Contracts;

public class Exam
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [MaxLength(100)]
    public string Description { get; set; } = string.Empty;
    public List<Question> Questions { get; set; } = new();
}