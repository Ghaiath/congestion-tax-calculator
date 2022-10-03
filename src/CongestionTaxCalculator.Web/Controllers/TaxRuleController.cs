using CongestionTaxCalculator.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CongestionTaxCalculator.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class TaxRuleController : ControllerBase
{
  private readonly ILogger<TaxRuleController> _logger;
  private readonly IMediator _mediator;

  public TaxRuleController(ILogger<TaxRuleController> logger, IMediator mediator)
  {
    _logger = logger;
    _mediator = mediator;
  }

  [HttpGet(Name = "GetTaxRules")]
  public async Task<IActionResult> Get()
  {
    var response = await _mediator.Send(new GetTaxRulesQuery());

    return Ok(response);
  }
}
