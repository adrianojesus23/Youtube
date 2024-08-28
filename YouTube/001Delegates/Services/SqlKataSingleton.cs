using System.Data.SqlClient;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace _001Delegates.Services;

public sealed class SqlKataSingleton
{
    private static readonly Lazy<SqlKataSingleton> instance_ = new Lazy<SqlKataSingleton>(()=> new SqlKataSingleton());
    private readonly QueryFactory _queryFactory;

    private SqlKataSingleton()
    {
        var connection = new SqlConnection(ConnectionString.Connection);
        var compiler = new SqlServerCompiler();
        _queryFactory = new QueryFactory(connection, compiler);
    }


    public static SqlKataSingleton Instance => instance_.Value;
    public QueryFactory QueryFactory => _queryFactory;
}