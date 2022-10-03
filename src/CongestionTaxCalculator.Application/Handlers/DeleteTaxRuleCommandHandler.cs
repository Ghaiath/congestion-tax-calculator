using CongestionTaxCalculator.Application.Commands;
using CongestionTaxCalculator.Domain.Models;
using CongestionTaxCalculator.Infrastructure;
using MediatR;

namespace CongestionTaxCalculator.Application.Handlers;

public class DeleteTaxRuleCommandHandler : IRequestHandler<DeleteTaxRuleCommand, List<TaxRule>>
{
  protected readonly CongestionTaxCalculatorDbContext _dbContext;

  public DeleteTaxRuleCommandHandler(CongestionTaxCalculatorDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<List<TaxRule>> Handle(DeleteTaxRuleCommand request, CancellationToken cancellationToken)
  {
    var taxRules = _dbContext.TaxRules.ToList();
    taxRules.Remove(request.TaxRule);
    await _dbContext.SaveChangesAsync(cancellationToken);
    return taxRules;
  }
}
