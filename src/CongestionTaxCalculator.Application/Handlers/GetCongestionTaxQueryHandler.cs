using CongestionTaxCalculator.Application.Extensions;
using CongestionTaxCalculator.Application.Queries;
using CongestionTaxCalculator.Application.Responses;
using CongestionTaxCalculator.Application.Utilities;
using CongestionTaxCalculator.Domain.Enums;
using CongestionTaxCalculator.Infrastructure;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace CongestionTaxCalculator.Application.Handlers;

public class GetCongestionTaxQueryHandler : IRequestHandler<GetCongestionTaxQuery, GetCongestionTaxResponse>
{
  protected readonly CongestionTaxCalculatorDbContext _dbContext;
  protected readonly IConfiguration _configuration;
  public GetCongestionTaxQueryHandler(CongestionTaxCalculatorDbContext congestionTaxCalculatorDbContext, IConfiguration configuration)
  {
    _dbContext = congestionTaxCalculatorDbContext;
    _configuration = configuration;
  }

  public async Task<GetCongestionTaxResponse> Handle(GetCongestionTaxQuery request, CancellationToken cancellationToken)
  {
    var getCongestionTaxResponse = new GetCongestionTaxResponse();
    var isTollFreeVehicle = Enum.TryParse(request.VehicleType, true, out TollFreeVehicles vehicle);
    if (isTollFreeVehicle)
    {
      getCongestionTaxResponse.VehicleType = request.VehicleType;
      getCongestionTaxResponse.Total = 0;
      return getCongestionTaxResponse;
    }

    getCongestionTaxResponse.VehicleType = request.VehicleType;
    var singleChargeRuleLimit = int.Parse(_configuration["SingleChargeRuleLimit"]);
    var dateGroups = request.Dates.GroupBy(d => d.Date.Day).Select(grp => grp.ToList()).Select(x => x.OrderBy(x => x).ToList()).ToList();

    var total = 0;
    foreach (var dateGroupByDay in dateGroups)
    {
      var taxDate = dateGroupByDay.First().Date;
      var timeSpans = dateGroupByDay.GroupWhile((groupDate, current) => (current - groupDate).TotalSeconds < singleChargeRuleLimit);
      var totalForDay = 0;

      foreach (var dates in timeSpans)
      {
        totalForDay += dates.Select(x => GetTollFeeForOneEntry(x)).Max();
      }
      totalForDay = totalForDay <= 60 ? totalForDay : 60;

      getCongestionTaxResponse.Taxes.Add(taxDate, totalForDay);
      total += totalForDay;
      getCongestionTaxResponse.Total = total;
    }
    return await Task.FromResult(getCongestionTaxResponse);
  }
  private int GetTollFeeForOneEntry(DateTime accessTime)
  {
    if (CongestionTaxUtilities.IsTollFreeDate(accessTime)) return 0;

    var taxRules = _dbContext.TaxRules.AsEnumerable().ToList();
    var onlyTime = TimeOnly.FromDateTime(accessTime);
    return taxRules.First(taxRule => onlyTime.IsBetween(TimeOnly.Parse(taxRule.StartDate), TimeOnly.Parse(taxRule.EndDate).AddMinutes(1))).Amount;
  }
}