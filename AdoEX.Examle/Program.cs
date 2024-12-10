using AdoEX.Interfaces;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AdoEX.Example
{
    /// <summary>
    /// 
    /// </summary>
    public class Person
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string LastName 
        { 
            get; 
            set; 
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main(string[] args)
        {
            /*
             1) Create BBDD

             2) Create table
            CREATE TABLE [dbo].[Tab1](
	                [Id] [int] IDENTITY(1,1) NOT NULL,
	                [FIrstName] [nvarchar](max) NOT NULL,
	                [LastName] [nvarchar](max) NOT NULL,
                    CONSTRAINT [PK_Tab1] PRIMARY KEY CLUSTERED 
            (
	            [Id] ASC
                )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
            ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
            GO
             */


            string stringConnection = "Server = localhost; Database = AdoEX; Trusted_Connection = True; TrustServerCertificate = True; Connection Timeout=600";
            
            using( IAdoExConnection connection = AdoExConnection.Create( () => new SqlConnection(stringConnection)))
            {                
                // execute scalar
                object o_result = await connection.ExecuteScalarAsync( ( IExecutorBuilder  builder ) => 
                {
                    builder.SetCommandText("INSERT INTO Tab1 ( FirstName, LastName ) VALUES ( @FirstName, @LastName ); SELECT CAST(scope_identity() AS int)")
                           .AddParameter("@FirstName", "John")
                           .AddParameter("@LastName", "Smith");
                });


                // execute NonQuery
                int id = (int)o_result;
                int int_result = await connection.ExecuteNonQueryAsync((IExecutorBuilder builder) =>
                {
                    builder.SetCommandText(" UPDATE Tab1 SET FirstName = @FirstName, LastName = @LastName WHERE id = @Id")
                           .AddParameter("@FirstName", $"John_{id}")
                           .AddParameter("@LastName", $"Smith_{id}")
                           .AddParameter("@Id", id);
                });

                // execute data reader
                await foreach(Person person in connection.ExecuteDataReaderAsync<Person>((IExecutorBuilder builder) => {
                    builder.SetCommandText("SELECT * FROM Tab1");
                }))
                {
                    Console.WriteLine($"Id: {person.Id}\tFirst name: {person.FirstName}\tLastName: {person.LastName}");
                }

                await connection.BeginTransactionAsync<TResult>((IAdoExTransaction transaction) =>
                {
                    try
                    {

                        transaction.Commit();

                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.ToString());

                        transaction.Rollback();

                        throw;
                    }
                });
            }
        }
    }
}
