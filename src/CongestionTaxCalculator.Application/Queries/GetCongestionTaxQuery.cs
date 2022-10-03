
using MediatR;

namespace CongestionTaxCalculator.Application.Queries;

public record GetCongestionTaxQuery(string VehicleType, DateTime[] Dates) : IRequest<List<(DateOnly, int)>> { }