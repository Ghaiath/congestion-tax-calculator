namespace CongestionTaxCalculator.Application.Responses;

public class GetCongestionTaxResponse
{
  public string VehicleType { get; set; } = string.Empty;
  public Dictionary<DateTime, int> Taxes { get; set; } = new Dictionary<DateTime, int>();
  public int Total { get; set; }
}
