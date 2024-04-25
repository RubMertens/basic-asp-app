using FluentValidation;
using Questioning.Domain;

namespace Questioning.Business.Validation;

public class QuestionResultValidator : AbstractValidator<QuestionResult>
{
    public QuestionResultValidator()
    {
        RuleFor(qr => qr.QuestionId).NotEmpty();
        RuleFor(qr => qr.Answers).NotEmpty();
    }
}