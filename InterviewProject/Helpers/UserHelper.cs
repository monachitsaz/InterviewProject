using Dapper;
using InterviewProject.DataAccess;
using InterviewProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace InterviewProject.Helpers
{
    public interface IUserHelper
    {
        public IEnumerable<User> GetUsers();
        public string InsertUser(User model);
        public User GetById(int id);
        public string UpdateUser(User model);
        public Task<string> DeleteUser(int id);
    }

    public class UserHelper : IUserHelper
    {
        ISqlUtility _sqlUtility;
        public UserHelper(ISqlUtility sqlUtility)
        {
            this._sqlUtility = sqlUtility;
        }

        public IEnumerable<User> GetUsers()
        {
            try
            {
                using (IDbConnection dapper = _sqlUtility.GetNewConnection())
                {
                    var query = "sp_Users_GetAll";
                    var result = dapper.Query<User>(query, commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string InsertUser(User model)
        {
            string message = "";
            try
            {
                using (IDbConnection dapper = _sqlUtility.GetNewConnection())
                {
                    var query = "sp_Users_Add";
                    var parameters = new DynamicParameters();
                    parameters.Add("Name", model.Name);
                    parameters.Add("Email", model.Email);
                    dapper.Execute(query, parameters, commandType: CommandType.StoredProcedure);
                    message = "The user has been created successfully";
                }
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return message;
        }

        public User GetById(int id)
        {
            try
            {
                using (IDbConnection dapper = _sqlUtility.GetNewConnection())
                {
                    var query = "sp_Users_GetById";
                    var parameters = new DynamicParameters();
                    parameters.Add("Id", id);
                    var result = dapper.QuerySingleOrDefault<User>(query, parameters, commandType: CommandType.StoredProcedure);
                    return result;

                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string UpdateUser(User model)
        {
            var message = "";
            try
            {
                using (IDbConnection dapper = _sqlUtility.GetNewConnection())
                {
                    var query = "sp_Users_Update";
                    var parameters = new DynamicParameters();
                    parameters.Add("Name", model.Name);
                    parameters.Add("Email", model.Email);
                    parameters.Add("Id", model.Id);
                    dapper.Execute(query, parameters, commandType: CommandType.StoredProcedure);
                    message = "The user has been updated successfully";

                }
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return message;
        }

        public async Task<string> DeleteUser(int id)
        {
            var message = "";
            try
            {
                using (IDbConnection dapper = _sqlUtility.GetNewConnection())
                {
                    var query = "sp_Users_Delete";
                    var parameter = new DynamicParameters();
                    parameter.Add("Id", id);
                   await dapper.ExecuteAsync(query, parameter, commandType: CommandType.StoredProcedure);
                    message = "The user has been deleted successfully";

                }
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return message;

        }

    }
}
