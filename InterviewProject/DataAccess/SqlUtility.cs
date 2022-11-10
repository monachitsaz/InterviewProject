using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewProject.DataAccess
{
    public interface ISqlUtility
    {
        SqlConnection GetNewConnection();
       

    }
    public class SqlUtility : ISqlUtility
    {
        private IConfiguration Configuration;

        public SqlUtility(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public SqlConnection GetNewConnection()
        {
            var connectionString = Configuration.GetConnectionString("InterviewProject");
            var sc = new SqlConnection(connectionString);
            return sc;
        }
    }
}
