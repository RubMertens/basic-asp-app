using FluentValidation;
using Questioning.Contracts;

namespace Questioning.Core.Validation;

public class ExamValidator : AbstractValidator<Exam>
{
    public ExamValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(250);
        RuleFor(e => e.Description).NotNull();
        
    }
}