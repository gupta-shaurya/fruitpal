using Fruitpal.Common;
using Fruitpal.DataAccess.Common;
using Fruitpal.Models.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Fruitpal.DataAccess.Fruit
{
    public class FruitDao : IFruitDao
    {
        #region Private Fields

        private readonly IDataProvider _fileDataProvider;

        #endregion Private Fields

        #region Public Constructors

        public FruitDao(IDataProvider fileDataProvider)
        {
            _fileDataProvider = fileDataProvider;
        }

        #endregion Public Constructors

        #region Public Methods

        public List<Dictionary<string, object>> GetFruitData(string fruitName)
        {
            List<Dictionary<string, object>> fruitData = new List<Dictionary<string, object>>();

            FileDataRequest fileDataRequest = CreateFileDataRequest();

            fruitData = _fileDataProvider.GetData<FileDataRequest, List<Dictionary<string, object>>>(fileDataRequest);

            fruitData = fruitData.Where(data => string.Equals(data[Constants.Commodity].ToString(), fruitName, StringComparison.InvariantCultureIgnoreCase)).ToList();

            return fruitData;
        }

        #endregion Public Methods

        #region Private Methods

        private FileDataRequest CreateFileDataRequest()
        {
            return new FileDataRequest { FileName = "SampleInput.json", FilePath = Directory.GetCurrentDirectory(), FileType = FileType.JSON };
        }

        #endregion Private Methods
    }
}