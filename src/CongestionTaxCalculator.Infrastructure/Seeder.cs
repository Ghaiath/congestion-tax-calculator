using CongestionTaxCalculator.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CongestionTaxCalculator.Infrastructure;

public static class Seeder
{
  public async static void SeedTaxRules(this IServiceProvider sp, IConfiguration configuration)
  {
    var dbContext = sp.GetRequiredService<CongestionTaxCalculatorDbContext>();

    if (!dbContext.TaxRules.ToList().Any())
    {
      var taxRules = new List<TaxRule>
      {
        new TaxRule
        {
          StartDate = TimeOnly.Parse("06:00").ToString(),
          EndDate = TimeOnly.Parse("06:29").ToString(),
          Amount = 8,
        },
        new TaxRule
        {
          StartDate = TimeOnly.Parse("06:30").ToString(),
          EndDate = TimeOnly.Parse("06:59").ToString(),
          Amount = 13,
        },
        new TaxRule
        {
          StartDate = TimeOnly.Parse("07:00").ToString(),
          EndDate = TimeOnly.Parse("07:59").ToString(),
          Amount = 18,
        },
        new TaxRule
        {
          StartDate = TimeOnly.Parse("08:00").ToString(),
          EndDate = TimeOnly.Parse("08:29").ToString(),
          Amount = 13,
        },
        new TaxRule
        {
          StartDate = TimeOnly.Parse("08:30").ToString(),
          EndDate = TimeOnly.Parse("14:59").ToString(),
          Amount = 8,
        },
        new TaxRule
        {
          StartDate = TimeOnly.Parse("15:00").ToString(),
          EndDate = TimeOnly.Parse("15:29").ToString(),
          Amount = 13,
        },
        new TaxRule
        {
          StartDate = TimeOnly.Parse("15:30").ToString(),
          EndDate = TimeOnly.Parse("16:59").ToString(),
          Amount = 18,
        },
        new TaxRule
        {
          StartDate = TimeOnly.Parse("17:00").ToString(),
          EndDate = TimeOnly.Parse("17:59").ToString(),
          Amount = 13,
        },
        new TaxRule
        {
          StartDate = TimeOnly.Parse("18:00").ToString(),
          EndDate = TimeOnly.Parse("18:29").ToString(),
          Amount = 8,
        },
        new TaxRule
        {
          StartDate = TimeOnly.Parse("18:30").ToString(),
          EndDate = TimeOnly.Parse("05:59").ToString(),
          Amount = 0,
        },
      };

      dbContext.TaxRules.AddRange(taxRules);
      await dbContext.SaveChangesAsync();
    }
  }
}