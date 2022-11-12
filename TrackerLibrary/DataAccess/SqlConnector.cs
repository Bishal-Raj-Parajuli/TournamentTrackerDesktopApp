using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    public class SqlConnector : IDataConnection
    {
        public PrizeModel CreatePrize(PrizeModel modelObj)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("DefaultConnection")))
            {
                var p = new DynamicParameters();
                p.Add("@PlaceNumber", modelObj.PlaceNumber);
                p.Add("@PlaceName", modelObj.PlaceName);
                p.Add("@PrizeAmount", modelObj.PrizeAmount);
                p.Add("@PrizePercentage", modelObj.PrizePercentage);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spCreatePrizes",p,commandType: CommandType.StoredProcedure);

                modelObj.Id = p.Get<int>("@id");

                return modelObj;
            }
        }
    }
}
