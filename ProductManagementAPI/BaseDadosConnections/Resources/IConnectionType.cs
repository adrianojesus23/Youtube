using SqlKata.Execution;
using System.Data;

namespace BaseDadosConnections.Resources
{
    public interface IConnectionType
    {
        Task<IDbConnection> GetDapperConnectionAsync();
        Task<IDbConnection> GetAdoNetConnectionAsync();
        Task<ApplicationDbContext> GetEfCoreContextAsync();
        Task<QueryFactory> GetSqlKataConnectionAsync();
    }
}