using System.Collections.Generic;

namespace Fruitpal.DataAccess.Fruit
{
    public interface IFruitDao
    {
        #region Public Methods

        List<Dictionary<string, object>> GetFruitData(string fruitName);

        #endregion Public Methods
    }
}