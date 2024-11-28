using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdoEX.Executors
{
    internal class ReaderExecutor<TEntity>: 
            ExecutorBase where TEntity : class, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbCommand"></param>
        public ReaderExecutor(DbCommand dbCommand) :
            base(dbCommand)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async IAsyncEnumerable<TEntity> ExecuteAsync() 
        {

            Dictionary<string,PropertyInfo> properties = typeof(TEntity)
                            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                            .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);                        

            using(DbDataReader reader = await this._DbCommand.ExecuteReaderAsync())
            {
                IEnumerable<string> column_names = Enumerable.Range( 0, reader.FieldCount )
                                        .Select( reader.GetName );

                while( await reader.ReadAsync() )
                {
                    TEntity item_result = new TEntity();

                    foreach(var columnName in column_names)
                    {
                        if(properties.TryGetValue(columnName, out var property))
                        {
                            var value = reader[ columnName ];
                            if(value != DBNull.Value)
                            {
                                property.SetValue( item_result, Convert.ChangeType(value, property.PropertyType));
                            }
                        }
                    }

                    yield return item_result;
                }
            }            
        }
    }
}
