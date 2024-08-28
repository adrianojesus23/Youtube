using SqlKata.Execution;

namespace _001Delegates.Services;

public class VendorService
{
    public async Task<IEnumerable<dynamic>> GetAsync()
    {
        var database = SqlKataSingleton.Instance.QueryFactory;

        var vendors = await database.Query("Vendors").GetAsync();

        return vendors;
    }
}