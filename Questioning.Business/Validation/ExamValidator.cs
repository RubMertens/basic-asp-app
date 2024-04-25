using FluentValidation;
using Questioning.Domain;

namespace Questioning.Business.Validation;

public class ExamValidator : AbstractValidator<Exam>
{
    public ExamValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(250);
        RuleFor(e => e.Description).NotNull();
        
    }
}