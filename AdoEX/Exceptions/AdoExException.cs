using System;
using System.Collections.Generic;
using System.Text;

namespace AdoEX.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class AdoExException: 
                 Exception
    {

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public AdoExException( string? message = null, Exception? innerException = null ):
                    base( message, innerException )
        { 
        }

    }
}
