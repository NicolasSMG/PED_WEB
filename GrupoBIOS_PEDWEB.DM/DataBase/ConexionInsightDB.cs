using GrupoBIOS_PEDWEB.DM.DataBase.Interfaces;
using Insight.Database;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.DM.DataBase
{
    public class ConexionInsightDB : IConexionBD
    {
        private readonly IConfiguration configuration;

        public ConexionInsightDB(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<IList<T>> QueryAsync<T>(string NombreSP, object Parametros = null)
        {
            var connection = new SqlConnection(configuration.GetConnectionString("PEDWEBConnectionString"));
            IList<T> result = await connection.QueryAsync<T>(NombreSP, Parametros);
            return result;
        }
        public async Task<T> QueryFirstAsync<T>(string NombreSP, object Parametros = null)
        {
            var connection = new SqlConnection(configuration.GetConnectionString("PEDWEBConnectionString"));
            var result = await connection.QueryAsync<T>(NombreSP, Parametros);
            if (result.Count != 0)
            {
                return result.First();
            }
            return default;
        }

        public async Task<T> InsertAsync<T>(string NombreSP, T Entidad)
        {
            var connection = new SqlConnection(configuration.GetConnectionString("PEDWEBConnectionString"));
            var res = await connection.InsertAsync(NombreSP, Entidad);
            return res;
        }

        public async Task<int> ExecuteAsync<T>(string NombreSP, T Entidad)
        {
            var connection = new SqlConnection(configuration.GetConnectionString("PEDWEBConnectionString"));
            var res = await connection.ExecuteAsync(NombreSP, Entidad);
            return res;
        }
    }
}
