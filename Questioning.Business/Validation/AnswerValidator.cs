using FluentValidation;
using Questioning.Domain;

namespace Questioning.Business.Validation;

public class AnswerValidator : AbstractValidator<Answer>
{
    public AnswerValidator()
    {
        RuleFor(a => a.Value).NotEmpty().MaximumLength(250);
    }
}