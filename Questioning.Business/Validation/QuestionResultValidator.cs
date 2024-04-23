using FluentValidation;
using Questioning.Contracts;

namespace Questioning.Core.Validation;

public class QuestionResultValidator : AbstractValidator<QuestionResult>
{
    public QuestionResultValidator()
    {
        RuleFor(qr => qr.QuestionId).NotEmpty();
        RuleFor(qr => qr.Answers).NotEmpty();
    }
}