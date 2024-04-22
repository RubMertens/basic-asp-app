using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Questioning.Contracts;
using Questioning.Core;
using Questioning.Persistance;

namespace Questioning.Web.Controllers;

[Route("[controller]")]
public class ExamController(ExamDbContext context, ExamManager manager) : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        var exams = context.Exams
            .ToList();
        return View(new ExamsOverview()
        {
            Exams = exams.Select(e => new ExamsOverview.OverviewItem()
            {
                Name = e.Name,
                Id = e.Id
            }).ToList()
        });
    }

    [HttpGet("details/{id}")]
    public IActionResult Details(int id)
    {
        var exam = context.Exams
            .Include(e => e.Questions)
            .ThenInclude(q => q.PossibleAnswers)
            .FirstOrDefault(e => e.Id == id);
        return View(new ExamDetail()
        {
            Name = exam.Name,
            Description = exam.Description,
            Questions = exam.Questions.Select(q => new ExamDetail.ExamQuestion()
            {
                Question = q.Value,
                QuestionType = Enum.GetName(q.QuestionType),
                Id = q.Id,
                Answers = q.PossibleAnswers.Select(a => new ExamDetail.PossibleAnswer()
                {
                    Answer = a.Value,
                    IsCorrect = a.IsCorrect
                }).ToArray()
            }).ToList()
        });
    }

    private EditQuestion? EditQuestionVm(int id)
    {
        var question = context.Questions
            .Include(q => q.PossibleAnswers)
            .FirstOrDefault(q => q.Id == id);
        var editQuestion = new EditQuestion()
        {
            Id = question.Id,
            ExamId = question.ExamId,
            Question = question.Value,
            QuestionType = new EditQuestion.QuestionTypeValue()
            {
                Name = Enum.GetName(question.QuestionType),
                Value = (int)question.QuestionType
            },
            QuestionTypeOptions = Enum.GetValues<Question.Type>().Select(v => new EditQuestion.QuestionTypeValue()
            {
                Name = Enum.GetName(v),
                Value = (int)v
            }).ToArray(),
            Answers = question.PossibleAnswers.Select(a => new EditQuestion.Answer()
            {
                AnswerId = a.Id,
                Value = a.Value,
                IsCorrect = a.IsCorrect
            }).ToList()
        };
        
        return editQuestion;
    }

    [HttpGet("editquestion/{id}")]
    public IActionResult EditQuestion(int id)
    {
        var vm = EditQuestionVm(id);
        if (vm == null)
            return NotFound();
        return View(vm);
    }

    [HttpPost("editquestion/{id:int}")]
    public IActionResult EditQuestion(EditQuestion model)
    {
        var result = manager.UpdateQuestion(new Question()
        {
            Id = model.Id,
            Value = model.Question,
            QuestionType = (Question.Type)model.QuestionType.Value,
            PossibleAnswers = model.Answers.Select(a => new Answer()
            {
                Id = a.AnswerId,
                Value = a.Value,
                IsCorrect = a.IsCorrect
            }).ToList(),
            ExamId = model.ExamId
        });
        if (result.IsValid)
            return RedirectToAction("Details", new { id = model.ExamId });

        foreach (var error in result.Validations.Errors)
        {
            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
        return View(EditQuestionVm(model.Id));
    }
}

public class EditQuestion
{
    public int Id { get; set; }
    public int ExamId { get; set; }
    public string Question { get; set; }
    public QuestionTypeValue QuestionType { get; set; }
    public QuestionTypeValue[]? QuestionTypeOptions { get; set; }

    public List<Answer> Answers { get; set; } = new();

    public class Answer
    {
        public int AnswerId { get; set; }
        public string Value { get; set; }
        public bool IsCorrect { get; set; }
    }

    public class QuestionTypeValue
    {
        public string? Name { get; set; }
        public int Value { get; set; }
    }
}

public class ExamDetail
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public List<ExamQuestion> Questions { get; set; } = new();

    public class ExamQuestion
    {
        public required string Question { get; set; }
        public required string QuestionType { get; set; }
        public int Id { get; set; }
        public required PossibleAnswer[] Answers { get; set; }
    }

    public class PossibleAnswer
    {
        public required string Answer { get; set; }
        public bool IsCorrect { get; set; }
    }
}

public class ExamsOverview
{
    public List<OverviewItem> Exams { get; set; } = new();

    public class OverviewItem
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}