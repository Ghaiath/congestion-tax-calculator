using CongestionTaxCalculator.Application.Queries;
using CongestionTaxCalculator.Domain.Models;
using CongestionTaxCalculator.Infrastructure;
using MediatR;

namespace CongestionTaxCalculator.Application.Handlers;

public class GetTaxRulesQueryHandler : IRequestHandler<GetTaxRulesQuery, List<TaxRule>>
{
  protected readonly CongestionTaxCalculatorDbContext _dbContext;
  public GetTaxRulesQueryHandler(CongestionTaxCalculatorDbContext congestionTaxCalculatorDbContext)
  {
    _dbContext = congestionTaxCalculatorDbContext;
  }

  public async Task<List<TaxRule>> Handle(GetTaxRulesQuery request, CancellationToken cancellationToken)
  {
    var taxRules = _dbContext.TaxRules.AsEnumerable().ToList();
    return await Task.FromResult(taxRules);
  }
}