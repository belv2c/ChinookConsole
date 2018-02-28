using ChinookConsole.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookConsole.DataAccess
{
    class InvoiceInfoQuery
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString;

        // Provide a query that shows the invoices associated with each sales agent. 
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

        // Provide a query that shows the Invoice Total, Customer name, Country and Sale Agent name for all invoices.
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

        // Looking at the InvoiceLine table, provide a query that COUNTs the number of line items for an Invoice with a parameterized Id from user input
        public int GetLineItems(int consoleUserInput)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"select count(*) as [LineItemTotals] 
                                        from InvoiceLine
                                        where InvoiceId = @invoiceId";

                var invoiceId = new SqlParameter("@invoiceId", SqlDbType.Int);
                invoiceId.Value = consoleUserInput;
                cmd.Parameters.Add(invoiceId);


                var invoiceLineCount = (int.Parse(cmd.ExecuteScalar().ToString()));
                return invoiceLineCount;
            }
        }
    }
}

