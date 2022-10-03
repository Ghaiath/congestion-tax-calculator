using CongestionTaxCalculator.Domain.Models;
using MediatR;

namespace CongestionTaxCalculator.Application.Commands;

public record class AddTaxRulesCommand(List<TaxRule> TaxRule) : IRequest<List<TaxRule>>;