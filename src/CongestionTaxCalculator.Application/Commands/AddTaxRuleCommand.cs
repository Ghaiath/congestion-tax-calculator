using CongestionTaxCalculator.Domain.Models;
using MediatR;

namespace CongestionTaxCalculator.Application.Commands;

public record class AddTaxRuleCommand(TaxRule TaxRule) : IRequest<List<TaxRule>>;
