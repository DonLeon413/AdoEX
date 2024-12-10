using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace AdoEX.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAdoExConnection: 
                     IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<object> ExecuteScalarAsync( Action<IExecutorBuilder> action );
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        Task<int> ExecuteNonQueryAsync( Action<IExecutorBuilder> action );

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="builderAction"></param>
        /// <returns></returns>
        IAsyncEnumerable<TEntity> ExecuteDataReaderAsync<TEntity>(Action<IExecutorBuilder> builderAction)
                                                        where TEntity : class, new();

        //Task ExecuteTransactionAsync( string trnasactionName );
    }
}
