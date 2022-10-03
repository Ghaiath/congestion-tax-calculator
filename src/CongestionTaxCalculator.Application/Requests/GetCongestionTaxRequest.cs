﻿namespace CongestionTaxCalculator.Application.Requests;

public class GetCongestionTaxRequest
{
    public string VehicleType { get; set; } = string.Empty;
    public string[] Dates { get; set; } = Array.Empty<string>();
}
