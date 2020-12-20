using Fruitpal.Models.Commodity;
using System.Collections.Generic;

namespace Fruitpal.Services.Fruit
{
    public interface IFruitService
    {
        #region Public Methods

        List<CommodityCost> GetCosts(string fruitName, string calculationFormula, decimal volume, decimal pricePerVolume);

        #endregion Public Methods
    }
}