using Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using Shared.Results;

namespace Api.Controllers;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected ActionResult ToActionResult<T>(ResultOfT<T> result)
        => ControllerExtensions.Map(this, result);

    protected ActionResult ToActionResult(Result result)
        => ControllerExtensions.Map(this, result);
}