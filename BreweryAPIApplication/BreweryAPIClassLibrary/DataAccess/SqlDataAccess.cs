using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BreweryAPIClassLibrary.DataAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        // Method to Load Data (Multiple Records)
        public async Task<List<T>> LoadData<T, U>(
            string storedProcedure,
            U parameters,
            string connectionStringName)
        {
            string connectionString = _config.GetConnectionString(connectionStringName);

            using IDbConnection connection = new SqlConnection(connectionString);

            var rows = await connection.QueryAsync<T>(storedProcedure, parameters,
                commandType: CommandType.StoredProcedure);

            return rows.ToList();
        }

        // Method to Save Data (Insert, Update, Delete)
        public async Task SaveData<T>(
            string storedProcedure,
            T parameters,
            string connectionStringName)
        {
            string connectionString = _config.GetConnectionString(connectionStringName);

            using IDbConnection connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync(
                storedProcedure,
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        // New Method to Get a Single Record (QuerySingleOrDefault)
        public async Task<T> GetDataById<T>(
            string storedProcedure,
            object parameters,
            string connectionStringName)
        {
            string connectionString = _config.GetConnectionString(connectionStringName);

            using IDbConnection connection = new SqlConnection(connectionString);

            // This is where we use Dapper's QuerySingleOrDefault to fetch a single result
            return await connection.QuerySingleOrDefaultAsync<T>(
                storedProcedure,
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        // New Method for Querying a Single Record or List (Generic)
        public async Task<T> QuerySingleOrDefaultAsync<T>(
            string storedProcedure,
            object parameters,
            string connectionStringName)
        {
            string connectionString = _config.GetConnectionString(connectionStringName);

            using IDbConnection connection = new SqlConnection(connectionString);

            return await connection.QuerySingleOrDefaultAsync<T>(
                storedProcedure,
                parameters,
                commandType: CommandType.StoredProcedure);
        }
    }
}
