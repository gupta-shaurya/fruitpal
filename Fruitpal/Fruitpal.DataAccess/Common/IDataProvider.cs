namespace Fruitpal.DataAccess.Common
{
    public interface IDataProvider
    {
        TResponse GetData<TRequest, TResponse>(TRequest request);
    }
}