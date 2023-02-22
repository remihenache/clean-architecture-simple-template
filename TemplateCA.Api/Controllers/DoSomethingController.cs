using Microsoft.AspNetCore.Mvc;
using TemplateCA.SampleContext.Applications.DoSomething;
using TemplateCA.SharedKernel.Applications;

namespace TemplateCA.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DoSomethingController : ControllerBase
{
    private readonly Dispatcher dispatcher;

    public DoSomethingController(Dispatcher dispatcher)
    {
        this.dispatcher = dispatcher;
    }

    [HttpPost(Name = "Do something")]
    public async Task<IActionResult> DoSomething()
    {
        await this.dispatcher.Send(new DoSomethingCommand(1));
        return Accepted();
    }
}