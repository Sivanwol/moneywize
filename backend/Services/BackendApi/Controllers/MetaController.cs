using System.Diagnostics;
using Application.Configs;
using Application.Contract;
using Application.Utils.Service;
using Domain.DTO.Test;
using Infrastructure.Services.Email;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
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
        var testList = new List<TestList> {
            new() {
                Id = random.Next(1, 100),
                Name = $"Test {random.Next(1, 100)}",
                AddedOn = DateTime.Now.ToString("dd/MM/yyyy"),
                Bio = "Test Bio",
                Class = "Test Class",
                ColorCode = "#000000",
                UpdatedOn = DateTime.Now.ToString("dd/MM/yyyy")
            },
            new() {
                Id = random.Next(1, 100),
                Name = $"Test {random.Next(1, 100)}",
                AddedOn = DateTime.Now.ToString("dd/MM/yyyy"),
                Bio = "Test Bio",
                Class = "Test Class",
                ColorCode = "#000000",
                UpdatedOn = DateTime.Now.ToString("dd/MM/yyyy")
            }
        };
        try {
            var template = await _templateService.GetTemplateHtmlAsStringAsync("MailingTemplates/test", testList);
            Log.Information($"Template: {template}");
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
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occured. The Mail could not be sent.");
        }
        catch (Exception ex) {
            Log.Error(ex, "An error occured while sending the mail.");
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