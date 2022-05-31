using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace DigitaalProgrammaBoekje.Pages.Database
{
    public class DbUtils
    {   
        public IDbConnection Connect()
        {
            string connectionString = "Server=127.0.0.1;Port=3306;Database=progboekdb;Uid=root;Pwd=jorislops-2022;";
            return new MySqlConnection(connectionString);
        }
    }
}