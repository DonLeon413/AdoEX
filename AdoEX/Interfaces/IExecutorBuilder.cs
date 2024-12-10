using System;
using System.Collections.Generic;
using System.Text;

namespace AdoEX.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IExecutorBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        IExecutorBuilder SetCommandText(string commandText);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <returns></returns>
        IExecutorBuilder SetStoredProcedure(string storedProcedure);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oarameterName"></param>
        /// <param name="parameterValue"></param>
        /// <returns></returns>
        IExecutorBuilder AddParameter(string oarameterName, object? parameterValue = null);
    }
}
