using CongestionTaxCalculator.Domain.Models;
using MongoFramework;

namespace CongestionTaxCalculator.Infrastructure;

public class CongestionTaxCalculatorDbContext : MongoDbContext
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public CongestionTaxCalculatorDbContext(IMongoDbConnection connection) : base(connection) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

  public MongoDbSet<TaxRule> TaxRules { get; set; }
}
