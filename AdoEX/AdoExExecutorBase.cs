using AdoEX.Executors;
using AdoEX.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace AdoEX
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AdoExExecutorBase                    
    {
        /// <summary>
        /// CTOR
        /// </summary>
        public AdoExExecutorBase()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract DbCommand GetCommand();


        #region interface IAdoExConnection

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public Task<object> ExecuteScalarAsync(Action<IExecutorBuilder> builder)
        {
            using(ScalarExecutor executor = new ScalarExecutor( GetCommand() ) )
            {
                if(!(builder is null))
                {
                    builder(executor);
                }
                Task<object> result = executor.ExecuteAsync();

                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public Task<int> ExecuteNonQueryAsync(Action<IExecutorBuilder> builder)
        {
            using(NonQueryExecutor executor = new NonQueryExecutor( GetCommand() ) )
            {
                if(!(builder is null))
                {
                    builder(executor);
                }

                Task<int> result = executor.ExecuteAsync();

                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="builderAction"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IAsyncEnumerable<TEntity> ExecuteDataReaderAsync<TEntity>(Action<IExecutorBuilder> builderAction)
                                                        where TEntity : class, new()
        {
            if(builderAction is null)
            {
                throw new ArgumentNullException(nameof(builderAction));
            }

            using(ReaderExecutor<TEntity> reader = new ReaderExecutor<TEntity>( GetCommand() ) )
            {
                builderAction(reader);

                IAsyncEnumerable<TEntity> result = reader.ExecuteAsync();

                return result;
            }
        }

        #endregion
    }
}
