﻿namespace CongestionTaxCalculator.Domain.Models;

public class Entity
{
  public string Id { get; set; } = Guid.NewGuid().ToString();
}
