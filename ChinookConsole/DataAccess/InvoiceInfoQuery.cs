using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using ChinookConsole.DataAccess.Models;

namespace ChinookConsole.DataAccess
{
    class InvoiceInfoQuery
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString;

        public List<SalesAgent> GetAgent()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = @"select i.InvoiceId, e.FirstName + ' ' + e.LastName as SalesAgent 
                                        from Invoice i, Employee e
                                        where i.InvoiceId = e.EmployeeId
                                        ORDER BY SalesAgent;";

                var reader = cmd.ExecuteReader();

                var salesAgentEmployees = new List<SalesAgent>();

                while (reader.Read())
                {
                    var employee = new SalesAgent
                    {
                        Name = reader["SalesAgent"].ToString(),
                        InvoiceId = int.Parse(reader["InvoiceId"].ToString())
                    };

                    salesAgentEmployees.Add(employee);
                }
                return salesAgentEmployees;
            }
        }
    }
}

