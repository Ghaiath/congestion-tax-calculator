﻿using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CongestionTaxCalculator.Application;

public static class DependencyInjection
{
  public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddMediatR(Assembly.GetExecutingAssembly());
  }
}
