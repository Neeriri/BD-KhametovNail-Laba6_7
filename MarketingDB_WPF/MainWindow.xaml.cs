using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MarketingDB_WPF
{
    public partial class MainWindow : Window
    {
        private DatabaseContext _context;
        private string _currentEntity = "Clients";
        private object? _selectedItem;

        public MainWindow()
        {
            InitializeComponent();
            _context = new DatabaseContext();
            EntityComboBox.SelectedIndex = 0;
            LoadData();
        }

        private void EntityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EntityComboBox.SelectedItem is ComboBoxItem item)
            {
                _currentEntity = item.Content?.ToString() ?? "Clients";
                SetupColumns();
                LoadData();
            }
        }

        private void SetupColumns()
        {
            MainDataGrid.Columns.Clear();

            switch (_currentEntity)
            {
                case "Clients":
                    AddColumn("ClientID", "ClientID", 60);
                    AddColumn("FullName", "Full Name", 150);
                    AddColumn("Email", "Email", 150);
                    AddColumn("Phone", "Phone", 100);
                    AddColumn("Address", "Address", 200);
                    break;

                case "Campaigns":
                    AddColumn("CampaignID", "ID", 60);
                    AddColumn("CampaignName", "Campaign Name", 180);
                    AddColumn("ClientID", "Client ID", 80);
                    AddColumn("Budget", "Budget", 100);
                    AddColumn("StartDate", "Start Date", 100);
                    AddColumn("EndDate", "End Date", 100);
                    AddColumn("Status", "Status", 100);
                    break;

                case "Employees":
                    AddColumn("EmployeeID", "ID", 60);
                    AddColumn("FullName", "Full Name", 150);
                    AddColumn("Position", "Position", 120);
                    AddColumn("Email", "Email", 150);
                    AddColumn("HourlyRate", "Hourly Rate", 100);
                    break;
            }
        }

        private void AddColumn(string binding, string header, int width)
        {
            var column = new DataGridTextColumn
            {
                Header = header,
                Binding = new Binding(binding),
                Width = width
            };
            MainDataGrid.Columns.Add(column);
        }

        private void LoadData()
        {
            try
            {
                switch (_currentEntity)
                {
                    case "Clients":
                        MainDataGrid.ItemsSource = _context.GetClients();
                        break;
                    case "Campaigns":
                        MainDataGrid.ItemsSource = _context.GetCampaigns();
                        break;
                    case "Employees":
                        MainDataGrid.ItemsSource = _context.GetEmployees();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MainDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (MainDataGrid.SelectedItem != null)
            {
                _selectedItem = MainDataGrid.SelectedItem;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var addWindow = _currentEntity switch
                {
                    "Clients" => new AddEditWindow(new Client()),
                    "Campaigns" => new AddEditWindow(new Campaign()),
                    "Employees" => new AddEditWindow(new Employee()),
                    _ => throw new InvalidOperationException("Unknown entity")
                };

                if (addWindow.ShowDialog() == true)
                {
                    SaveAddedItem(addWindow.Item);
                    LoadData();
                    MessageBox.Show("Record added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding record: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveAddedItem(object item)
        {
            switch (item)
            {
                case Client client:
                    _context.AddClient(client);
                    break;
                case Campaign campaign:
                    _context.AddCampaign(campaign);
                    break;
                case Employee employee:
                    _context.AddEmployee(employee);
                    break;
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedItem == null)
            {
                MessageBox.Show("Please select a record to edit.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var editWindow = _selectedItem switch
                {
                    Client client => new AddEditWindow(CloneClient(client)),
                    Campaign campaign => new AddEditWindow(CloneCampaign(campaign)),
                    Employee employee => new AddEditWindow(CloneEmployee(employee)),
                    _ => throw new InvalidOperationException("Unknown entity")
                };

                if (editWindow.ShowDialog() == true)
                {
                    SaveEditedItem(editWindow.Item, _selectedItem);
                    LoadData();
                    MessageBox.Show("Record updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing record: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveEditedItem(object newItem, object originalItem)
        {
            switch (originalItem)
            {
                case Client originalClient when newItem is Client newClient:
                    newClient.ClientID = originalClient.ClientID;
                    _context.UpdateClient(newClient);
                    break;
                case Campaign originalCampaign when newItem is Campaign newCampaign:
                    newCampaign.CampaignID = originalCampaign.CampaignID;
                    _context.UpdateCampaign(newCampaign);
                    break;
                case Employee originalEmployee when newItem is Employee newEmployee:
                    newEmployee.EmployeeID = originalEmployee.EmployeeID;
                    _context.UpdateEmployee(newEmployee);
                    break;
            }
        }

        private Client CloneClient(Client client)
        {
            return new Client
            {
                ClientID = client.ClientID,
                FullName = client.FullName,
                Email = client.Email,
                Phone = client.Phone,
                Address = client.Address
            };
        }

        private Campaign CloneCampaign(Campaign campaign)
        {
            return new Campaign
            {
                CampaignID = campaign.CampaignID,
                CampaignName = campaign.CampaignName,
                ClientID = campaign.ClientID,
                Budget = campaign.Budget,
                StartDate = campaign.StartDate,
                EndDate = campaign.EndDate,
                Status = campaign.Status
            };
        }

        private Employee CloneEmployee(Employee employee)
        {
            return new Employee
            {
                EmployeeID = employee.EmployeeID,
                FullName = employee.FullName,
                Position = employee.Position,
                Email = employee.Email,
                HourlyRate = employee.HourlyRate
            };
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedItem == null)
            {
                MessageBox.Show("Please select a record to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            try
            {
                switch (_selectedItem)
                {
                    case Client client:
                        _context.DeleteClient(client.ClientID);
                        break;
                    case Campaign campaign:
                        _context.DeleteCampaign(campaign.CampaignID);
                        break;
                    case Employee employee:
                        _context.DeleteEmployee(employee.EmployeeID);
                        break;
                }

                LoadData();
                _selectedItem = null;
                MessageBox.Show("Record deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting record: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
