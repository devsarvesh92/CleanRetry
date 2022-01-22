public interface IRetryStrategy
{
    Task<T> RetryOnException<T>(Func<Task<T>> action, int maxRetry, List<Type> retryOnExceptions);
}