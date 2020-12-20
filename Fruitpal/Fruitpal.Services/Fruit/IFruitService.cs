using Fruitpal.Models.Fruit;
using System.Collections.Generic;

namespace Fruitpal.Services.Fruit
{
    public interface IFruitService
    {
        #region Public Methods

        List<FruitCost> GetFruitCostEstimates(string fruitName, string calculationFormula, decimal volume, decimal pricePerVolume);

        #endregion Public Methods
    }
}