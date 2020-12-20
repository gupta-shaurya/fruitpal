using Fruitpal.Models.Common;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Fruitpal.DataAccess.Common
{
    public class FileDataProvider : IDataProvider
    {
        #region Public Methods

        public TResponse GetData<TRequest, TResponse>(TRequest request)
        {
            TResponse response = default(TResponse);

            var fileDataRequest = request as FileDataRequest;

            var filePath = Path.Combine(fileDataRequest.FilePath, fileDataRequest.FileName);

            using (StreamReader r = new StreamReader(filePath))
            {
                string data = r.ReadToEnd();

                switch (fileDataRequest.FileType)
                {
                    case FileType.JSON:
                        response = JsonConvert.DeserializeObject<TResponse>(data);
                        break;

                    case FileType.Text:
                    default:
                        response = (TResponse)Convert.ChangeType(data, typeof(TResponse));
                        break;
                }
            }

            return response;
        }

        #endregion Public Methods
    }
}