using FluentValidation;
using Questioning.Contracts;

namespace Questioning.Core.Validation;

public class AnswerValidator : AbstractValidator<Answer>
{
    public AnswerValidator()
    {
        RuleFor(a => a.Value).NotEmpty().MaximumLength(250);
    }
}