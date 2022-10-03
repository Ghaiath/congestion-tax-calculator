using CongestionTaxCalculator.Application.Commands;
using CongestionTaxCalculator.Domain.Models;
using CongestionTaxCalculator.Infrastructure;
using MediatR;

namespace CongestionTaxCalculator.Application.Handlers;

public class AddTaxRuleCommandHandler : IRequestHandler<AddTaxRuleCommand, List<TaxRule>>
{
  protected readonly CongestionTaxCalculatorDbContext _dbContext;

  public AddTaxRuleCommandHandler(CongestionTaxCalculatorDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<List<TaxRule>> Handle(AddTaxRuleCommand request, CancellationToken cancellationToken)
  {
    var taxRules = _dbContext.TaxRules.ToList();
    taxRules.Add(request.TaxRule);
    await _dbContext.SaveChangesAsync(cancellationToken);
    return taxRules;
  }
}
