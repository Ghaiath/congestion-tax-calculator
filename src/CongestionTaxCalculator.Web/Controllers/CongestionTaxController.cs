using CongestionTaxCalculator.Application.Queries;
using CongestionTaxCalculator.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace CongestionTaxCalculator.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class CongestionTaxController : ControllerBase
{
  private readonly ILogger<CongestionTaxController> _logger;
  private readonly IMediator _mediator;

  public CongestionTaxController(ILogger<CongestionTaxController> logger, IMediator mediator)
  {
    _logger = logger;
    _mediator = mediator;
  }

  [HttpPost(Name = "GetCongestionTaxQuery")]
  public async Task<IActionResult> Get([FromBody] GetCongestionTaxRequest getCongestionTaxQuery)
  {
    var dates = new List<DateTime>();

    foreach (var date in getCongestionTaxQuery.Dates)
    {
      var correctDate = DateTime.TryParse(date.ToString(), out DateTime outDate);
      if (!correctDate)
      {
        throw new Exception($"Date {date} is not corrrect");
      }
      dates.Add(outDate);
    }
    var response = await _mediator.Send(new GetCongestionTaxQuery(getCongestionTaxQuery.VehicleType, dates.ToArray()));

    return Ok(response);
  }
}