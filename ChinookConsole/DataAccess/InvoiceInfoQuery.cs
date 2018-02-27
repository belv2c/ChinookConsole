using System.Collections.Generic;
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

        public List<InvoiceInfo> GetInvoiceTotal()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = @"select i.Total, e.firstname + ' ' + e.lastname as [SalesAgent], i.BillingCountry, c.FirstName + ' ' + c.LastName as [CustomerName]
                                    from Employee e
                                    join customer c on c.SupportRepId = e.EmployeeId
                                    join invoice i on i.CustomerId = c.CustomerId
                                    where e.title = 'Sales Support Agent'";


              
                var reader = cmd.ExecuteReader();

                var InvoiceTotal = new List<InvoiceInfo>();

                while (reader.Read())
                {
                    var invoiceTotalInfo = new InvoiceInfo
                    {
                        CustomerName = reader["CustomerName"].ToString(),
                        Total = double.Parse(reader["Total"].ToString()),
                        SalesAgent = reader["SalesAgent"].ToString(),
                        BillingCountry = reader["BillingCountry"].ToString()
                    };

                    InvoiceTotal.Add(invoiceTotalInfo);
                }
                return InvoiceTotal;
            }
        }
    }
}

