using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace AdoEX.Interfaces
{
    public interface IAdoExTransaction: 
            IAdoExCommand
    {
        void Commit();
        void ROllback();
    }
}
