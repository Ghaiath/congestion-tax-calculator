
using CongestionTaxCalculator.Domain.Models;
using MediatR;

namespace CongestionTaxCalculator.Application.Queries;

public record GetTaxRulesQuery() : IRequest<List<TaxRule>> { }