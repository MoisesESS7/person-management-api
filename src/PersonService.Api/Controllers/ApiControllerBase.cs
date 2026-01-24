using PersonService.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using PersonService.Shared.Results;

namespace PersonService.Api.Controllers;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected ActionResult ToActionResult<T>(ResultOfT<T> result)
        => ControllerExtensions.Map(this, result);

    protected ActionResult ToActionResult(Result result)
        => ControllerExtensions.Map(this, result);
}