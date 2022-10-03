using CongestionTaxCalculator.Application.Commands;
using CongestionTaxCalculator.Domain.Models;
using CongestionTaxCalculator.Infrastructure;
using MediatR;

namespace CongestionTaxCalculator.Application.Handlers;

public class AddTaxRulesCommandHandler : IRequestHandler<AddTaxRulesCommand, List<TaxRule>>
{
  protected readonly CongestionTaxCalculatorDbContext _dbContext;

  public AddTaxRulesCommandHandler(CongestionTaxCalculatorDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<List<TaxRule>> Handle(AddTaxRulesCommand request, CancellationToken cancellationToken)
  {
    var taxRules = _dbContext.TaxRules.ToList();
    taxRules.AddRange(request.TaxRule);
    await _dbContext.SaveChangesAsync(cancellationToken);
    return taxRules;
  }
}
