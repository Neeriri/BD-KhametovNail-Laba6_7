using System;
using System.Windows;
using System.Windows.Controls;

namespace MarketingDB_WPF
{
    public partial class AddEditWindow : Window
    {
        public object Item { get; private set; }
        private string _entityType = "";

        public AddEditWindow(object item)
        {
            InitializeComponent();
            Item = item;
            Owner = Application.Current.MainWindow;
            SetupForm();
        }

        private void SetupForm()
        {
            FormGrid.Children.Clear();

            switch (Item)
            {
                case Client client:
                    _entityType = "Client";
                    TitleLabel.Content = client.ClientID == 0 ? "Добавить клиента" : "Изменить клиента";
                    SetupClientForm(client);
                    break;

                case Campaign campaign:
                    _entityType = "Campaign";
                    TitleLabel.Content = campaign.CampaignID == 0 ? "Добавить кампанию" : "Изменить кампанию";
                    SetupCampaignForm(campaign);
                    break;

                case Employee employee:
                    _entityType = "Employee";
                    TitleLabel.Content = employee.EmployeeID == 0 ? "Добавить сотрудника" : "Изменить сотрудника";
                    SetupEmployeeForm(employee);
                    break;
            }
        }

        private TextBox CreateTextBox(string text, int row)
        {
            var textBox = new TextBox
            {
                Text = text,
                FontSize = 14,
                Padding = new Thickness(5),
                VerticalContentAlignment = VerticalAlignment.Center
            };
            Grid.SetRow(textBox, row);
            Grid.SetColumn(textBox, 1);
            return textBox;
        }

        private Label CreateLabel(string content, int row)
        {
            var label = new Label
            {
                Content = content,
                FontSize = 14,
                VerticalContentAlignment = VerticalAlignment.Center,
                Foreground = System.Windows.Media.Brushes.White
            };
            Grid.SetRow(label, row);
            Grid.SetColumn(label, 0);
            return label;
        }

        private void SetupClientForm(Client client)
        {
            FormGrid.Children.Add(CreateLabel("Full Name:", 0));
            var txtFullName = CreateTextBox(client.FullName, 0);
            FormGrid.Children.Add(txtFullName);

            FormGrid.Children.Add(CreateLabel("Email:", 1));
            var txtEmail = CreateTextBox(client.Email ?? "", 1);
            FormGrid.Children.Add(txtEmail);

            FormGrid.Children.Add(CreateLabel("Phone:", 2));
            var txtPhone = CreateTextBox(client.Phone ?? "", 2);
            FormGrid.Children.Add(txtPhone);

            FormGrid.Children.Add(CreateLabel("Address:", 3));
            var txtAddress = CreateTextBox(client.Address ?? "", 3);
            FormGrid.Children.Add(txtAddress);

            SaveButton.Click += (s, e) =>
            {
                client.FullName = txtFullName.Text;
                client.Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text;
                client.Phone = string.IsNullOrWhiteSpace(txtPhone.Text) ? null : txtPhone.Text;
                client.Address = string.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text;
                DialogResult = true;
            };
        }

        private void SetupCampaignForm(Campaign campaign)
        {
            FormGrid.Children.Add(CreateLabel("Campaign Name:", 0));
            var txtName = CreateTextBox(campaign.CampaignName, 0);
            FormGrid.Children.Add(txtName);

            FormGrid.Children.Add(CreateLabel("Client ID:", 1));
            var txtClientId = CreateTextBox(campaign.ClientID.ToString(), 1);
            FormGrid.Children.Add(txtClientId);

            FormGrid.Children.Add(CreateLabel("Budget:", 2));
            var txtBudget = CreateTextBox(campaign.Budget?.ToString() ?? "", 2);
            FormGrid.Children.Add(txtBudget);

            FormGrid.Children.Add(CreateLabel("Start Date:", 3));
            var dtStart = new DatePicker
            {
                SelectedDate = campaign.StartDate,
                FontSize = 14,
                Padding = new Thickness(5)
            };
            Grid.SetRow(dtStart, 3);
            Grid.SetColumn(dtStart, 1);
            FormGrid.Children.Add(dtStart);

            FormGrid.Children.Add(CreateLabel("End Date:", 4));
            var dtEnd = new DatePicker
            {
                SelectedDate = campaign.EndDate,
                FontSize = 14,
                Padding = new Thickness(5)
            };
            Grid.SetRow(dtEnd, 4);
            Grid.SetColumn(dtEnd, 1);
            FormGrid.Children.Add(dtEnd);

            FormGrid.Children.Add(CreateLabel("Status:", 5));
            var txtStatus = CreateTextBox(campaign.Status ?? "", 5);
            FormGrid.Children.Add(txtStatus);

            SaveButton.Click += (s, e) =>
            {
                if (!int.TryParse(txtClientId.Text, out int clientId))
                {
                    MessageBox.Show("Client ID must be a number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                campaign.CampaignName = txtName.Text;
                campaign.ClientID = clientId;
                campaign.Budget = decimal.TryParse(txtBudget.Text, out var budget) ? budget : null;
                campaign.StartDate = dtStart.SelectedDate;
                campaign.EndDate = dtEnd.SelectedDate;
                campaign.Status = string.IsNullOrWhiteSpace(txtStatus.Text) ? null : txtStatus.Text;
                DialogResult = true;
            };
        }

        private void SetupEmployeeForm(Employee employee)
        {
            FormGrid.Children.Add(CreateLabel("Full Name:", 0));
            var txtFullName = CreateTextBox(employee.FullName, 0);
            FormGrid.Children.Add(txtFullName);

            FormGrid.Children.Add(CreateLabel("Position:", 1));
            var txtPosition = CreateTextBox(employee.Position ?? "", 1);
            FormGrid.Children.Add(txtPosition);

            FormGrid.Children.Add(CreateLabel("Email:", 2));
            var txtEmail = CreateTextBox(employee.Email ?? "", 2);
            FormGrid.Children.Add(txtEmail);

            FormGrid.Children.Add(CreateLabel("Hourly Rate:", 3));
            var txtRate = CreateTextBox(employee.HourlyRate?.ToString() ?? "", 3);
            FormGrid.Children.Add(txtRate);

            SaveButton.Click += (s, e) =>
            {
                employee.FullName = txtFullName.Text;
                employee.Position = string.IsNullOrWhiteSpace(txtPosition.Text) ? null : txtPosition.Text;
                employee.Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text;
                employee.HourlyRate = decimal.TryParse(txtRate.Text, out var rate) ? rate : null;
                DialogResult = true;
            };
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Validation is handled in each setup method
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
