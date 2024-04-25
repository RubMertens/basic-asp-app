using System.Net.Http.Headers;
using AngleSharp;
using AngleSharp.Html.Dom;
using AngleSharp.Io;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;

namespace Questioning.IntegrationTests;

public class ExamApiTests
    (CustomWebApplicationFactory factory) : IClassFixture<
        CustomWebApplicationFactory>
{
    [Fact]
    public async Task ExamOverview_ShouldReturn_One()
    {
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/exam");
        response.EnsureSuccessStatusCode();
        var html = await response.Content.ReadAsStringAsync();
        var document = await BrowsingContext
            .New()
            .OpenAsync(m => m.Content(html));
        var link = document
            .QuerySelectorAll("a[data-testId^='exam-']");
        Assert.Equal(1, link.Length);
        Assert.Equivalent(
            (link[0] as IHtmlAnchorElement)?.Href,
            "http://localhost/Exam/details/1"
        );
    }
}

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT",
            "Development");
        Environment.SetEnvironmentVariable(
            "ConnectionStrings:DefaultConnection",
            "Data Source=file::memory:;Cache=Shared");
        
        
    }
}