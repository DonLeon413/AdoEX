using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace AdoEX.Executors
{
    internal class ScalarExecutor: 
                   ExecutorBase
    {
        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="dbCommand"></param>
        public ScalarExecutor( DbCommand dbCommand ):
                base( dbCommand )
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<object> ExecuteAsync()
        {
            object result = await this._DbCommand.ExecuteScalarAsync();
            
            return result;
        }
    }
}
