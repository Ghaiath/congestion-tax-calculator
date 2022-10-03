using CongestionTaxCalculator.Domain.Models;
using MediatR;

namespace CongestionTaxCalculator.Application.Commands;

public record class DeleteTaxRuleCommand(TaxRule TaxRule) : IRequest<List<TaxRule>>;