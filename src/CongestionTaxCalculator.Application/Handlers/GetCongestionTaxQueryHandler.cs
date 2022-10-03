using CongestionTaxCalculator.Application.Queries;
using CongestionTaxCalculator.Application.Utilities;
using CongestionTaxCalculator.Domain.Enums;
using CongestionTaxCalculator.Infrastructure;
using MediatR;

namespace CongestionTaxCalculator.Application.Handlers;

public class GetCongestionTaxQueryHandler : IRequestHandler<GetCongestionTaxQuery, List<(DateOnly, int)>>
{
  protected readonly CongestionTaxCalculatorDbContext _dbContext;
  public GetCongestionTaxQueryHandler(CongestionTaxCalculatorDbContext congestionTaxCalculatorDbContext)
  {
    _dbContext = congestionTaxCalculatorDbContext;
  }

  public async Task<List<(DateOnly, int)>> Handle(GetCongestionTaxQuery request, CancellationToken cancellationToken)
  {
    var isTollFreeVehicle = Enum.TryParse(request.VehicleType, true, out TollFreeVehicles vehicle);
    var result = new List<(DateOnly, int)>();
    if (isTollFreeVehicle) return result;

    var dateGroups = request.Dates.GroupBy(d => d.Date.Day).Select(grp => grp.ToList()).Select(x => x.OrderBy(x => x)).ToList();

    foreach (var dateGroup in dateGroups)
    {
      var prevDate = DateTime.MinValue;
      var prevTollFee = 0;
      var stillInTimeSpan = false;
      var totalForDay = 0;
      var amountList = new List<int>();

      foreach (var date in dateGroup)
      {
        stillInTimeSpan = (date - prevDate).TotalMinutes <= 60;

        var tollFeeForOneEntry = GetTollFeeForOneEntry(date);

        if (stillInTimeSpan)
        {
          amountList.Add(prevTollFee);
          if (tollFeeForOneEntry >= amountList.Max())
          {
            totalForDay -= prevTollFee;
            totalForDay += tollFeeForOneEntry;
          }
        }
        else
        {
          totalForDay += tollFeeForOneEntry;
          amountList.Clear();
        }

        prevDate = date;
        prevTollFee = tollFeeForOneEntry;
      }
      totalForDay = totalForDay <= 60 ? totalForDay : 60;
      result.Add((DateOnly.FromDateTime(dateGroup.First()), totalForDay));
    }
    return await Task.FromResult(result);
  }
  private int GetTollFeeForOneEntry(DateTime accessTime)
  {
    if (CongestionTaxUtilities.IsTollFreeDate(accessTime)) return 0;

    var taxRules = _dbContext.TaxRules.AsEnumerable().ToList();
    var onlyTime = TimeOnly.FromDateTime(accessTime);
    return taxRules.First(taxRule => onlyTime.IsBetween(TimeOnly.Parse(taxRule.StartDate), TimeOnly.Parse(taxRule.EndDate).AddMinutes(1))).Amount;
  }
}