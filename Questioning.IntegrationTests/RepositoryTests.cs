using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Moq;
using Questioning.Business;
using Questioning.Data;
using Questioning.Domain;
using Questioning.Persistance;

namespace Questioning.IntegrationTests;

public class RepositoryTests
{
    [Fact]
    public void Create()
    {
        var validator = new Mock<IValidator<Exam>>();
        validator.Setup(v => v.Validate(It.IsAny<Exam>()))
            .Returns(new ValidationResult())            
            ;
        var examDbContext = new Mock<IExamDbContext>();
        var examDbSet = new Mock<DbSet<Exam>>();
        examDbSet.Setup(e => e.Add(It.IsAny<Exam>()));
        examDbContext.SetupProperty(e => e.Exams, examDbSet.Object);
        var manager = new ExamManager(
            validator.Object,
            new Mock<IValidator<Question>>().Object,
            new Mock<IValidator<Answer>>().Object,
            examDbContext.Object
        );
        var result = manager.CreateExam(It.IsAny<Exam>());
        Assert.True(result.IsValid);
    }
}