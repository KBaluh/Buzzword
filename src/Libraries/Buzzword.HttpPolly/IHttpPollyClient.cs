namespace Buzzword.HttpPolly
{
    public interface IHttpPollyClient
    {
        /// <summary>
        /// HTTP GET
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="retries"></param>
        /// <returns></returns>
        /// <exception cref="BuzzwordException"></exception>
        /// <exception cref="BuzzwordAccessException"></exception>
        Task<T?> GetResultAsync<T>(Uri uri, int retries = 3);

        /// <summary>
        /// HTTP GET
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="retries"></param>
        /// <returns></returns>
        /// <exception cref="BuzzwordException"></exception>
        /// <exception cref="BuzzwordAccessException"></exception>
        Task<T?> GetResultAsync<T>(Uri uri, CancellationToken cancellationToken, int retries = 3);

        /// <summary>
        /// HTTP POST
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="uri"></param>
        /// <param name="retries"></param>
        /// <returns></returns>
        /// <exception cref="BuzzwordException"></exception>
        /// <exception cref="BuzzwordAccessException"></exception>
        Task<TResult?> PostResultAsync<TResult>(Uri uri, int retries = 1);

        /// <summary>
        /// HTTP POST
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="uri"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="retries"></param>
        /// <returns></returns>
        /// <exception cref="BuzzwordException"></exception>
        /// <exception cref="BuzzwordAccessException"></exception>
        Task<TResult?> PostResultAsync<TResult>(Uri uri, CancellationToken cancellationToken, int retries = 1);

        /// <summary>
        /// HTTP POST
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="uri"></param>
        /// <param name="param"></param>
        /// <param name="retries"></param>
        /// <returns></returns>
        /// <exception cref="BuzzwordException"></exception>
        /// <exception cref="BuzzwordAccessException"></exception>
        Task<TResult?> PostResultAsync<TResult>(Uri uri, TResult param, int retries = 1);

        /// <summary>
        /// HTTP POST
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="uri"></param>
        /// <param name="param"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="retries"></param>
        /// <returns></returns>
        /// <exception cref="BuzzwordException"></exception>
        /// <exception cref="BuzzwordAccessException"></exception>
        Task<TResult?> PostResultAsync<TResult>(Uri uri, TResult param, CancellationToken cancellationToken, int retries = 1);

        /// <summary>
        /// HTTP POST
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TParam"></typeparam>
        /// <param name="uri"></param>
        /// <param name="param"></param>
        /// <param name="retries"></param>
        /// <returns></returns>
        /// <exception cref="BuzzwordException"></exception>
        /// <exception cref="BuzzwordAccessException"></exception>
        Task<TResult?> PostResultAsync<TResult, TParam>(Uri uri, TParam param, int retries = 1);

        /// <summary>
        /// HTTP POST
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TParam"></typeparam>
        /// <param name="uri"></param>
        /// <param name="param"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="retries"></param>
        /// <returns></returns>
        /// <exception cref="BuzzwordException"></exception>
        /// <exception cref="BuzzwordAccessException"></exception>
        Task<TResult?> PostResultAsync<TResult, TParam>(Uri uri, TParam param, CancellationToken cancellationToken, int retries = 1);

        /// <summary>
        /// HTTP POST
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="retries"></param>
        /// <returns></returns>
        /// <exception cref="BuzzwordException"></exception>
        /// <exception cref="BuzzwordAccessException"></exception>
        Task PostAsync(Uri uri, int retries = 1);

        /// <summary>
        /// HTTP POST
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="param"></param>
        /// <param name="retries"></param>
        /// <returns></returns>
        /// <exception cref="BuzzwordException"></exception>
        /// <exception cref="BuzzwordAccessException"></exception>
        Task PostAsync<T>(Uri uri, T param, int retries = 1);

        /// <summary>
        /// HTTP PUT 
        /// </summary>
        /// <typeparam name="TParam"></typeparam>
        /// <param name="uri"></param>
        /// <param name="param"></param>
        /// <param name="retries"></param>
        /// <returns></returns>
        /// <exception cref="BuzzwordException"></exception>
        /// <exception cref="BuzzwordAccessException"></exception>
        Task<TParam?> UpdateResultAsync<TParam>(Uri uri, TParam param, int retries = 3);

        /// <summary>
        /// HTTP PUT 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TParam"></typeparam>
        /// <param name="uri"></param>
        /// <param name="param"></param>
        /// <param name="retries"></param>
        /// <returns></returns>
        /// <exception cref="BuzzwordException"></exception>
        /// <exception cref="BuzzwordAccessException"></exception>
        Task<TResult?> UpdateResultAsync<TResult, TParam>(Uri uri, TParam param, int retries = 3);

        /// <summary>
        /// HTTP DELETE
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="retries"></param>
        /// <returns></returns>
        /// <exception cref="BuzzwordException"></exception>
        /// <exception cref="BuzzwordAccessException"></exception>
        Task<bool> DeleteResultAsync(Uri uri, int retries = 3);

        /// <summary>
        /// HTTP DELETE
        /// </summary>
        /// <param name="deleteUries"></param>
        /// <returns></returns>
        /// <exception cref="BuzzwordException"></exception>
        /// <exception cref="BuzzwordAccessException"></exception>
        Task<int> DeleteRemotesAsync(params Uri[] deleteUries);
    }
}
