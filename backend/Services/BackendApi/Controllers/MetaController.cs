using System.Diagnostics;
using Application.Configs;
using Application.Contract;
using Application.Utils.Service;
using Infrastructure.Services.Email;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace BackendApi.Controllers;

public class MetaController : BaseApiController {
    private readonly IMailService _mailService;
    private readonly ITemplateService _templateService;
    public MetaController(IMediator _mediator, BackendApplicationConfig _configuration, IMailService mailService, ITemplateService templateService) : base(_mediator,
        _configuration) {
        _mailService = mailService;
        _templateService = templateService;
    }
    [HttpGet("/test-mail")]
    public async Task<ActionResult<string>> TestMail() {
        var random = new Random();
        List<Domain.DTO.Test.TestList> testList = new List<Domain.DTO.Test.TestList> {
            new Domain.DTO.Test.TestList {
                Id = random.Next(1, 100),
                Name = $"Test {random.Next(1, 100)}",
                AddedOn = DateTime.Now.ToString("dd/MM/yyyy"),
                Bio = "Test Bio",
                Class = "Test Class",
                ColorCode = "#000000",
                UpdatedOn = DateTime.Now.ToString("dd/MM/yyyy")
            },
            new Domain.DTO.Test.TestList {
                Id = random.Next(1, 100),
                Name = $"Test {random.Next(1, 100)}",
                AddedOn = DateTime.Now.ToString("dd/MM/yyyy"),
                Bio = "Test Bio",
                Class = "Test Class",
                ColorCode = "#000000",
                UpdatedOn = DateTime.Now.ToString("dd/MM/yyyy")
            },
        };
        var template = await _templateService.GetTemplateHtmlAsStringAsync("TestMail", testList);
        var mailData = new MailData(
            new List<string> {
                "test@test.com"
            },
            "Test Mail",
            template
        );
        var result = await _mailService.SendAsync(mailData, new CancellationToken());
        if (result) {
            return StatusCode(StatusCodes.Status200OK, "Mail has successfully been sent.");
        } else {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occured. The Mail could not be sent.");
        }
    }
    [HttpGet("/info")]
    public ActionResult<string> Info() {
        var assembly = typeof(Program).Assembly;

        var lastUpdate = System.IO.File.GetLastWriteTime(assembly.Location);
        var version = FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;

        return Ok($"Version: {version}, Last Updated: {lastUpdate}");
    }
}