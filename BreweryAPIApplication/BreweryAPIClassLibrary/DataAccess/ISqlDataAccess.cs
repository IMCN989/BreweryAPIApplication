
namespace BreweryAPIClassLibrary.DataAccess
{
    public interface ISqlDataAccess
    {
        Task<T> GetDataById<T>(string storedProcedure, object parameters, string connectionStringName);
        Task<List<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName);
        Task<T> QuerySingleOrDefaultAsync<T>(string storedProcedure, object parameters, string connectionStringName);
        Task SaveData<T>(string storedProcedure, T parameters, string connectionStringName);
    }
}