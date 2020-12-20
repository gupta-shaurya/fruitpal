using Fruitpal.Common;
using Fruitpal.Common.Extensions;
using Fruitpal.DataAccess.Fruit;
using Fruitpal.Models.Fruit;
using NCalc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fruitpal.Services.Fruit
{
    public class FruitService : IFruitService
    {
        #region Private Fields

        private readonly IFruitDao _fruitDao;

        #endregion Private Fields

        #region Public Constructors

        public FruitService(IFruitDao fruitDao)
        {
            _fruitDao = fruitDao;
        }

        #endregion Public Constructors

        #region Public Methods

        public List<FruitCost> GetFruitCostEstimates(string fruitName, string calculationFormula, decimal volume, decimal pricePerVolume)
        {
            List<FruitCost> fruitCostEstimates = new List<FruitCost>();

            if (string.IsNullOrWhiteSpace(fruitName))
                throw new ArgumentException("Invalid input value for fruitName");

            var fruitData = _fruitDao.GetFruitData(fruitName);

            foreach (var data in fruitData)
            {
                fruitCostEstimates.Add(new FruitCost
                {
                    Country = data[Constants.Country].ToString(),
                    TotalCost = CalculateTotalCost(calculationFormula, data, volume, pricePerVolume),
                    CostBreakdown = FormatCostBreakdown(calculationFormula, data)
                });
            }

            return fruitCostEstimates.OrderByDescending(data => data.TotalCost).ToList();
        }

        #endregion Public Methods

        #region Private Methods

        private decimal CalculateTotalCost(string calculationFormula, Dictionary<string, object> data, decimal volume, decimal pricePerVolume)
        {
            Expression expression = new Expression(calculationFormula)
            {
                Parameters = data
            };

            expression.Parameters.Add(Constants.Volume, volume);

            expression.Parameters.Add(Constants.PricePerVolume, pricePerVolume);

            Decimal.TryParse(expression.Evaluate().ToString(), out decimal totalCost);

            return totalCost;
        }

        private string FormatCostBreakdown(string calculationFormula, Dictionary<string, object> parameters)
        {
            return calculationFormula
                .Replace("[", "{")
                .Replace("]", "}")
                .Replace(parameters);
        }

        #endregion Private Methods
    }
}