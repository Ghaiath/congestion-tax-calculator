using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoFramework;

namespace CongestionTaxCalculator.Infrastructure;

public static class DependencyInjection
{
  public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddSingleton<IMongoDbConnection>(MongoDbConnection.FromConnectionString(configuration.GetConnectionString("CongestionTaxCalculatorConnection")));
    services.AddSingleton<CongestionTaxCalculatorDbContext>();
  }
}
