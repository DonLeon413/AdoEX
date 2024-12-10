using AdoEX.Exceptions;
using AdoEX.Executors;
using AdoEX.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdoEX
{
    //
    public class AdoExConnection:
                 AdoExExecutorBase,
                 IAdoExConnection
    {
        private DbConnection _DbConnection;
        private bool disposedValue;

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="iDbConnection"></param>
        private AdoExConnection( DbConnection dbConnection )
        {
            this._DbConnection = dbConnection;
        }

        /// <summary>
        /// Creator
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static AdoExConnection Create(Func<DbConnection> func)
        {
            if(func is null)
            {
                throw new AdoExException($"Parameter is null.");
            }

            DbConnection connection = func();
            connection.Open();

            return new AdoExConnection( connection );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override DbCommand GetCommand()
        {
            return this._DbConnection.CreateCommand();
        }

        #region interface IAdoEXConnection       

        #region interface IDisposable

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if(!disposedValue)
            {
                if(disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~AdoExConnection()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        
        #endregion

        #endregion
    }
}
