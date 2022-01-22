public class CustomRetryStrategy : IRetryStrategy
{
    public async Task<T> RetryOnException<T>(Func<Task<T>> action,
                                  int maxRetry,
                                  List<Type> retryOnExceptions)
    {
        T? result = default(T);
        for (int retry = 0; retry <= maxRetry; retry++)
        {
            try
            {
                result = await action();
                break;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                var isRetryRequired = retryOnExceptions.Where(exce => exce.IsAssignableFrom(ex.GetType()));
                if (!isRetryRequired.Any() || retry == maxRetry) throw;
            }
        }
        return result;
    }

    
}