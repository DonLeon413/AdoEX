using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace AdoEX.Executors
{
    /// <summary>
    /// 
    /// </summary>
    internal class NonQueryExecutor: 
                   ExecutorBase
    {
        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="dbCommand"></param>
        public NonQueryExecutor( DbCommand dbCommand ):
                base( dbCommand )
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<int> ExecuteAsync()
        {
            int result = await this._DbCommand.ExecuteNonQueryAsync();
            
            return result;
        }
    }
}
