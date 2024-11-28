using System;
using System.Collections.Generic;
using System.Text;

namespace AdoEX.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAdoExCommand: 
                     IDisposable
    {
        IAdoExCommand AddParameter(string parameterName, object? value = null);
    }
}
