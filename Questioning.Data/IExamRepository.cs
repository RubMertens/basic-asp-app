﻿using Microsoft.EntityFrameworkCore;
using Questioning.Domain;

namespace Questioning.Data;

public interface IExamRepository
{
    void CreateQuestion(Question question);
    void UpdateQuestion(Question question);

    IEnumerable<Question> GetQuestionsByExam(int examId);
}

public class ExamRepository(IExamDbContext context) : IExamRepository
{
    public void CreateQuestion(Question question)
    {
        context.Questions.Add(question);
        context.SaveChanges();
    }

    public void UpdateQuestion(Question question)
    {
        context.Questions.Update(question);
        context.SaveChanges();
    }

    public IEnumerable<Question> GetQuestionsByExam(int examId)
    {
        return context.Questions
                .Include(q => q.PossibleAnswers)
                .Where(q => q.ExamId == examId)
            ;
    }
}