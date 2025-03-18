using System.Data;
using Microsoft.Data.SqlClient;

namespace InternalAPI.Services
{
    public class UserService
    {
        private readonly string? _connectionString;

        public UserService(string? connectionString)
        {
            _connectionString = connectionString;
        }

        public int AddUser(string userName)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("AddUser", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@UserName", userName);
            SqlParameter userIdParam = new SqlParameter("@UserId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(userIdParam);
            command.ExecuteNonQuery();

            return (int)userIdParam.Value;
        }
    }
}