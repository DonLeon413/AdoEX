using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace AdoEX.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAdoExTransaction: 
            IAdoExConnection
    {
        /// <summary>
        /// 
        /// </summary>
        void Commit();

        /// <summary>
        /// 
        /// </summary>
        void Rollback();
    }
}
