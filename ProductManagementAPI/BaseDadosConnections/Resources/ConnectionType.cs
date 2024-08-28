using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDadosConnections.Resources
{
    public class ConnectionType : IConnectionType
    {

        // Dapper Connection
        public async Task<IDbConnection> GetDapperConnectionAsync()
        {
            var connection = new SqlConnection(ConnectionString._connectionString);
            await connection.OpenAsync();
            return connection;
        }

        // ADO.NET Connection
        public async Task<IDbConnection> GetAdoNetConnectionAsync()
        {
            var connection = new SqlConnection(ConnectionString._connectionString);
            await connection.OpenAsync();
            return connection;
        }

        // Entity Framework Core Connection
        public async Task<ApplicationDbContext> GetEfCoreContextAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseSqlServer(ConnectionString._connectionString)
                            .Options;

            var context = new ApplicationDbContext(options);

            await context.Database.OpenConnectionAsync();

            return context;
        }

        // SQLKata Connection
        public async Task<QueryFactory> GetSqlKataConnectionAsync()
        {
            var connection = new SqlConnection(ConnectionString._connectionString);
            await connection.OpenAsync();

            var compiler = new SqlKata.Compilers.SqlServerCompiler();
           
            var db = new QueryFactory(connection, compiler);

            //// Exemplo de uma consulta SQLKata
            var query = db.Query("Invoices")
                          .Join("Vendors", "Invoices.VendorID", "Vendors.VendorID")
                          .Select("Invoices.*", "Vendors.VendorName")
                          .Where("InvoiceTotal", ">", 1000)
                          .OrderBy("InvoiceDate");

            //// Converter a query para SQL
            var sqlResult = compiler.Compile(query);

            return db;
        }

        public async Task<string> QuerySql(SqlKata.Query query)
        {
            var compiler = new SqlKata.Compilers.SqlServerCompiler();

            var sqlResult = compiler.Compile(query);

            return await Task.FromResult(sqlResult.Sql);
        }
    }
}
