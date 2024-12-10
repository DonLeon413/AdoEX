using AdoEX.Interfaces;
using System;
using System.Data;
using System.Data.Common;

namespace AdoEX.Executors
{
    /// <summary>
    /// 
    /// </summary>
    internal class ExecutorBase:
                   IExecutorBuilder,
                   IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        protected DbCommand _DbCommand;
        

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="command"></param>
        public ExecutorBase(DbCommand command)
        {
            this._DbCommand = command;
        }

        #region interface IExecutorBuilder

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public IExecutorBuilder SetCommandText(string commandText)
        {
            this._DbCommand.CommandText = commandText;
            this._DbCommand.CommandType = CommandType.Text;

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <returns></returns>
        public IExecutorBuilder SetStoredProcedure(string storedProcedure)
        {
            this._DbCommand.CommandText += storedProcedure;
            this._DbCommand.CommandType = CommandType.StoredProcedure;

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oarameterName"></param>
        /// <param name="parameterValue"></param>
        /// <returns></returns>
        public IExecutorBuilder AddParameter(string oarameterName, object? parameterValue = null)
        {
            var p = this._DbCommand.CreateParameter();
            p.ParameterName = oarameterName;
            p.Value = parameterValue;

            this._DbCommand.Parameters.Add(p);

            return this;
        }

        #endregion

        #region interface IDisposable

        private bool disposedValue;
        
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
                    this._DbCommand.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ExceutorBase()
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
    }
}
