using FluentValidation;
using Questioning.Contracts;

namespace Questioning.Core.Validation;

public class QuestionValidator : AbstractValidator<Question>
{
    public QuestionValidator()
    {
        RuleFor(q => q.Value).NotEmpty().MaximumLength(250);
        RuleFor(q => q.PossibleAnswers).NotEmpty();
        RuleFor(q => q.QuestionType).NotEqual(Question.Type.None);
        
        RuleFor(q => new { q.PossibleAnswers, q.QuestionType }).Must(props =>
        {
            if(props.QuestionType == Question.Type.MultipleChoice && !props.PossibleAnswers.Any(pa => pa.IsCorrect))
                return false;
            return true;
        }).WithMessage("At least one answer must be correct for multiple choice questions.");
        
        RuleFor(q => new { q.PossibleAnswers, q.QuestionType }).Must(props =>
        {
            if(props.QuestionType == Question.Type.SingleChoice && props.PossibleAnswers.Count(pa => pa.IsCorrect) != 1)
                return false;
            return true;
        }).WithMessage("Exactly one answer must be correct for single choice questions.");
    }
}