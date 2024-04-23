using System.Net.Http.Headers;
using AngleSharp;
using AngleSharp.Html.Dom;
using AngleSharp.Io;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using Questioning.Core;

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

    [Fact]
    public async Task
        ExamManager_UpdateQuestion_UpdatesWithoutInsertingMoreAnswers()
    {
        using var scope = factory.Services.CreateScope();
        var manager = scope.ServiceProvider
            .GetRequiredService<ExamManager>();
    }

    public static async Task<IHtmlDocument> GetDocumentAsync(
        HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        var document = await BrowsingContext.New()
            .OpenAsync(ResponseFactory, CancellationToken.None);
        return (IHtmlDocument)document;

        void ResponseFactory(VirtualResponse htmlResponse)
        {
            htmlResponse
                .Address(response.RequestMessage.RequestUri)
                .Status(response.StatusCode);

            MapHeaders(response.Headers);
            MapHeaders(response.Content.Headers);

            htmlResponse.Content(content);

            void MapHeaders(HttpHeaders headers)
            {
                foreach (var header in headers)
                {
                    foreach (var value in header.Value)
                    {
                        htmlResponse.Header(header.Key, value);
                    }
                }
            }
        }
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