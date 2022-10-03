using MongoFramework.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace CongestionTaxCalculator.Domain.Models;

[Table("TaxRuleEntities")]
[RuntimeTypeDiscovery]

public class TaxRule : Entity
{
  public string StartDate { get; set; } = string.Empty;
  public string EndDate { get; set; } = string.Empty;
  public int Amount { get; set; }
  public string TimeRange
  {
    get => $"{StartDate} - {EndDate}";
    set => Console.WriteLine(value);
  }
}