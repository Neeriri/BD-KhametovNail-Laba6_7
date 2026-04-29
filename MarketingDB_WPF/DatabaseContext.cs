using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace MarketingDB_WPF
{
    public class DatabaseContext : IDisposable
    {
        private readonly string _connectionString;
        private SqlConnection? _connection;

        public DatabaseContext()
        {
            // Connection string for local SQL Server Express
            // Update server name if needed (e.g., DESKTOP-2G2083T\SQLEXPRESS)
            _connectionString = "Server=localhost\\SQLEXPRESS;Database=MarketingDB;Trusted_Connection=True;TrustServerCertificate=True;";
        }

        public DatabaseContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        private SqlConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new SqlConnection(_connectionString);
            }
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                _connection.Open();
            }
            return _connection;
        }

        #region Clients CRUD

        public List<Client> GetClients()
        {
            var clients = new List<Client>();
            using var cmd = new SqlCommand("SELECT ClientID, FullName, Email, Phone, Address FROM Clients", GetConnection());
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                clients.Add(new Client
                {
                    ClientID = reader.GetInt32(0),
                    FullName = reader.GetString(1),
                    Email = reader.IsDBNull(2) ? null : reader.GetString(2),
                    Phone = reader.IsDBNull(3) ? null : reader.GetString(3),
                    Address = reader.IsDBNull(4) ? null : reader.GetString(4)
                });
            }
            return clients;
        }

        public void AddClient(Client client)
        {
            using var cmd = new SqlCommand(
                "INSERT INTO Clients (FullName, Email, Phone, Address) VALUES (@FullName, @Email, @Phone, @Address); SELECT SCOPE_IDENTITY();",
                GetConnection());
            cmd.Parameters.AddWithValue("@FullName", client.FullName);
            cmd.Parameters.AddWithValue("@Email", (object?)client.Email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Phone", (object?)client.Phone ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Address", (object?)client.Address ?? DBNull.Value);
            var id = cmd.ExecuteScalar();
            client.ClientID = Convert.ToInt32(id);
        }

        public void UpdateClient(Client client)
        {
            using var cmd = new SqlCommand(
                "UPDATE Clients SET FullName=@FullName, Email=@Email, Phone=@Phone, Address=@Address WHERE ClientID=@ClientID",
                GetConnection());
            cmd.Parameters.AddWithValue("@ClientID", client.ClientID);
            cmd.Parameters.AddWithValue("@FullName", client.FullName);
            cmd.Parameters.AddWithValue("@Email", (object?)client.Email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Phone", (object?)client.Phone ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Address", (object?)client.Address ?? DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void DeleteClient(int clientId)
        {
            using var cmd = new SqlCommand("DELETE FROM Clients WHERE ClientID=@ClientID", GetConnection());
            cmd.Parameters.AddWithValue("@ClientID", clientId);
            cmd.ExecuteNonQuery();
        }

        #endregion

        #region Campaigns CRUD

        public List<Campaign> GetCampaigns()
        {
            var campaigns = new List<Campaign>();
            using var cmd = new SqlCommand(
                "SELECT CampaignID, CampaignName, ClientID, Budget, StartDate, EndDate, Status FROM Campaigns",
                GetConnection());
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                campaigns.Add(new Campaign
                {
                    CampaignID = reader.GetInt32(0),
                    CampaignName = reader.GetString(1),
                    ClientID = reader.GetInt32(2),
                    Budget = reader.IsDBNull(3) ? null : reader.GetDecimal(3),
                    StartDate = reader.IsDBNull(4) ? null : reader.GetDateTime(4),
                    EndDate = reader.IsDBNull(5) ? null : reader.GetDateTime(5),
                    Status = reader.IsDBNull(6) ? null : reader.GetString(6)
                });
            }
            return campaigns;
        }

        public void AddCampaign(Campaign campaign)
        {
            using var cmd = new SqlCommand(
                "INSERT INTO Campaigns (CampaignName, ClientID, Budget, StartDate, EndDate, Status) VALUES (@CampaignName, @ClientID, @Budget, @StartDate, @EndDate, @Status); SELECT SCOPE_IDENTITY();",
                GetConnection());
            cmd.Parameters.AddWithValue("@CampaignName", campaign.CampaignName);
            cmd.Parameters.AddWithValue("@ClientID", campaign.ClientID);
            cmd.Parameters.AddWithValue("@Budget", (object?)campaign.Budget ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@StartDate", (object?)campaign.StartDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@EndDate", (object?)campaign.EndDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", (object?)campaign.Status ?? DBNull.Value);
            var id = cmd.ExecuteScalar();
            campaign.CampaignID = Convert.ToInt32(id);
        }

        public void UpdateCampaign(Campaign campaign)
        {
            using var cmd = new SqlCommand(
                "UPDATE Campaigns SET CampaignName=@CampaignName, ClientID=@ClientID, Budget=@Budget, StartDate=@StartDate, EndDate=@EndDate, Status=@Status WHERE CampaignID=@CampaignID",
                GetConnection());
            cmd.Parameters.AddWithValue("@CampaignID", campaign.CampaignID);
            cmd.Parameters.AddWithValue("@CampaignName", campaign.CampaignName);
            cmd.Parameters.AddWithValue("@ClientID", campaign.ClientID);
            cmd.Parameters.AddWithValue("@Budget", (object?)campaign.Budget ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@StartDate", (object?)campaign.StartDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@EndDate", (object?)campaign.EndDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", (object?)campaign.Status ?? DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void DeleteCampaign(int campaignId)
        {
            using var cmd = new SqlCommand("DELETE FROM Campaigns WHERE CampaignID=@CampaignID", GetConnection());
            cmd.Parameters.AddWithValue("@CampaignID", campaignId);
            cmd.ExecuteNonQuery();
        }

        #endregion

        #region Employees CRUD

        public List<Employee> GetEmployees()
        {
            var employees = new List<Employee>();
            using var cmd = new SqlCommand(
                "SELECT EmployeeID, FullName, Position, Email, HourlyRate FROM Employees",
                GetConnection());
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                employees.Add(new Employee
                {
                    EmployeeID = reader.GetInt32(0),
                    FullName = reader.GetString(1),
                    Position = reader.IsDBNull(2) ? null : reader.GetString(2),
                    Email = reader.IsDBNull(3) ? null : reader.GetString(3),
                    HourlyRate = reader.IsDBNull(4) ? null : reader.GetDecimal(4)
                });
            }
            return employees;
        }

        public void AddEmployee(Employee employee)
        {
            using var cmd = new SqlCommand(
                "INSERT INTO Employees (FullName, Position, Email, HourlyRate) VALUES (@FullName, @Position, @Email, @HourlyRate); SELECT SCOPE_IDENTITY();",
                GetConnection());
            cmd.Parameters.AddWithValue("@FullName", employee.FullName);
            cmd.Parameters.AddWithValue("@Position", (object?)employee.Position ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Email", (object?)employee.Email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@HourlyRate", (object?)employee.HourlyRate ?? DBNull.Value);
            var id = cmd.ExecuteScalar();
            employee.EmployeeID = Convert.ToInt32(id);
        }

        public void UpdateEmployee(Employee employee)
        {
            using var cmd = new SqlCommand(
                "UPDATE Employees SET FullName=@FullName, Position=@Position, Email=@Email, HourlyRate=@HourlyRate WHERE EmployeeID=@EmployeeID",
                GetConnection());
            cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
            cmd.Parameters.AddWithValue("@FullName", employee.FullName);
            cmd.Parameters.AddWithValue("@Position", (object?)employee.Position ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Email", (object?)employee.Email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@HourlyRate", (object?)employee.HourlyRate ?? DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void DeleteEmployee(int employeeId)
        {
            using var cmd = new SqlCommand("DELETE FROM Employees WHERE EmployeeID=@EmployeeID", GetConnection());
            cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
            cmd.ExecuteNonQuery();
        }

        #endregion

        public void Dispose()
        {
            _connection?.Close();
            _connection?.Dispose();
        }
    }
}
