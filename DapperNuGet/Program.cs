using Microsoft.Data.SqlClient;
using Dapper;
using System.ComponentModel.DataAnnotations.Schema;

namespace DapperNuGet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = "Server=223-10;Database=ChatDb;Trusted_Connection=true;Encrypt=false";
            using var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            const string selectSqlQuery = $@"SELECT user_id as {nameof(GroupMessage.AuthorName)}, message as {nameof(GroupMessage.Message)}, create_date as {nameof(GroupMessage.CreateDate)} FROM [GroupMessages] ORDER BY [create_date]";
            var groupMessages = sqlConnection.Query<GroupMessage>(selectSqlQuery).ToList();
            foreach (var groupMessage in groupMessages)
            {
                Console.WriteLine($"{groupMessage.AuthorName} : {groupMessage.Message} " +
                                    $"{groupMessage.CreateDate}");
            }

        }
    }

    public class GroupMessage
    {
        public string AuthorName { get; set; }
        public string Message { get; set; }
        [Column("create_date")]
        public DateTime CreateDate { get; set; }
        [Column("user_id")]
        //public int User_id { get; set; }
        public int GroupId { get; set; }
    }
}