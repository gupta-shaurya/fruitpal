using Fruitpal.DataAccess.Common;
using Fruitpal.DataAccess.Fruit;
using Fruitpal.Services.Fruit;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Fruitpal.ConsoleApp
{
    internal class Program
    {
        #region Private Fields

        private static IServiceProvider _serviceProvider;

        #endregion Private Fields

        #region Public Methods

        public static void Main(string[] args)
        {
            RegisterServices();

            string fruitName = args[0].Trim();

            if (string.IsNullOrWhiteSpace(fruitName))
                throw new ArgumentException("Invalid input value for fruitName");

            if (!Decimal.TryParse(args[1], out decimal pricePerVolume))
                throw new ArgumentException("Invalid input value for pricePerVolume");

            if (!Decimal.TryParse(args[2], out decimal volume))
                throw new ArgumentException("Invalid input value for volume");

            string calculationFormula = "(([price_per_volume] + [variable_overhead]) * [volume]) + [fixed_overhead]";

            IServiceScope scope = _serviceProvider.CreateScope();
            var costEstimates = scope.ServiceProvider.GetRequiredService<IFruitService>()
                .GetFruitCostEstimates(fruitName, calculationFormula, volume, pricePerVolume);

            foreach (var costEstimate in costEstimates)
            {
                Console.WriteLine(string.Format("{0} {1} | {2}", costEstimate.Country, costEstimate.TotalCost, costEstimate.CostBreakdown));
            }

            DisposeServices();
        }

        #endregion Public Methods

        #region Private Methods

        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddScoped<IDataProvider, FileDataProvider>();
            services.AddScoped<IFruitDao, FruitDao>();
            services.AddScoped<IFruitService, FruitService>();
            _serviceProvider = services.BuildServiceProvider(true);
        }

        #endregion Private Methods
    }
}