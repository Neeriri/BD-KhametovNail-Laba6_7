using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LabaBD
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Laba4Entities _context;
        private Clients _currentClient;
        private Campaigns _currentCampaign;
        private Employees _currentEmployee;

        public MainWindow()
        {
            InitializeComponent();
            _context = new Laba4Entities();
            LoadClients();
            LoadCampaigns();
            LoadEmployees();
        }

        #region Клиенты (Clients)

        private void LoadClients()
        {
            try
            {
                DgClients.ItemsSource = _context.Clients.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки клиентов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAddClient_Click(object sender, RoutedEventArgs e)
        {
            ClearClientForm();
            _currentClient = null;
        }

        private void BtnEditClient_Click(object sender, RoutedEventArgs e)
        {
            if (DgClients.SelectedItem == null)
            {
                MessageBox.Show("Выберите клиента для редактирования", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _currentClient = (Clients)DgClients.SelectedItem;
            TxtClientFio.Text = _currentClient.Фио;
            TxtClientEmail.Text = _currentClient.email;
            TxtClientPhone.Text = _currentClient.Телефон;
            TxtClientAddress.Text = _currentClient.адрес;
        }

        private void BtnDeleteClient_Click(object sender, RoutedEventArgs e)
        {
            if (DgClients.SelectedItem == null)
            {
                MessageBox.Show("Выберите клиента для удаления", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Вы уверены, что хотите удалить этого клиента?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var client = (Clients)DgClients.SelectedItem;
                    _context.Clients.Remove(client);
                    _context.SaveChanges();
                    LoadClients();
                    ClearClientForm();
                    MessageBox.Show("Клиент успешно удален", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления клиента: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnRefreshClient_Click(object sender, RoutedEventArgs e)
        {
            LoadClients();
            ClearClientForm();
        }

        private void BtnSaveClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TxtClientFio.Text))
                {
                    MessageBox.Show("ФИО обязательно для заполнения", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (_currentClient == null)
                {
                    // Создание нового клиента
                    _currentClient = new Clients
                    {
                        Фио = TxtClientFio.Text,
                        email = TxtClientEmail.Text,
                        Телефон = TxtClientPhone.Text,
                        адрес = TxtClientAddress.Text
                    };
                    _context.Clients.Add(_currentClient);
                }
                else
                {
                    // Обновление существующего клиента
                    _currentClient.Фио = TxtClientFio.Text;
                    _currentClient.email = TxtClientEmail.Text;
                    _currentClient.Телефон = TxtClientPhone.Text;
                    _currentClient.адрес = TxtClientAddress.Text;
                }

                _context.SaveChanges();
                LoadClients();
                ClearClientForm();
                MessageBox.Show("Клиент успешно сохранен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения клиента: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancelClient_Click(object sender, RoutedEventArgs e)
        {
            ClearClientForm();
        }

        private void ClearClientForm()
        {
            TxtClientFio.Clear();
            TxtClientEmail.Clear();
            TxtClientPhone.Clear();
            TxtClientAddress.Clear();
            _currentClient = null;
        }

        #endregion

        #region Кампании (Campaigns)

        private void LoadCampaigns()
        {
            try
            {
                DgCampaigns.ItemsSource = _context.Campaigns.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки кампаний: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAddCampaign_Click(object sender, RoutedEventArgs e)
        {
            ClearCampaignForm();
            _currentCampaign = null;
        }

        private void BtnEditCampaign_Click(object sender, RoutedEventArgs e)
        {
            if (DgCampaigns.SelectedItem == null)
            {
                MessageBox.Show("Выберите кампанию для редактирования", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _currentCampaign = (Campaigns)DgCampaigns.SelectedItem;
            TxtCampaignName.Text = _currentCampaign.название;
            TxtCampaignClientId.Text = _currentCampaign.id_клиента?.ToString();
            TxtCampaignBudget.Text = _currentCampaign.бюджет?.ToString();
            DpCampaignStart.SelectedDate = _currentCampaign.дата_начала;
            DpCampaignEnd.SelectedDate = _currentCampaign.дата_окончания;
            TxtCampaignStatus.Text = _currentCampaign.статус;
        }

        private void BtnDeleteCampaign_Click(object sender, RoutedEventArgs e)
        {
            if (DgCampaigns.SelectedItem == null)
            {
                MessageBox.Show("Выберите кампанию для удаления", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Вы уверены, что хотите удалить эту кампанию?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var campaign = (Campaigns)DgCampaigns.SelectedItem;
                    _context.Campaigns.Remove(campaign);
                    _context.SaveChanges();
                    LoadCampaigns();
                    ClearCampaignForm();
                    MessageBox.Show("Кампания успешно удалена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления кампании: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnRefreshCampaign_Click(object sender, RoutedEventArgs e)
        {
            LoadCampaigns();
            ClearCampaignForm();
        }

        private void BtnSaveCampaign_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TxtCampaignName.Text))
                {
                    MessageBox.Show("Название обязательно для заполнения", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (_currentCampaign == null)
                {
                    // Создание новой кампании
                    _currentCampaign = new Campaigns
                    {
                        название = TxtCampaignName.Text,
                        id_клиента = string.IsNullOrEmpty(TxtCampaignClientId.Text) ? (int?)null : int.Parse(TxtCampaignClientId.Text),
                        бюджет = string.IsNullOrEmpty(TxtCampaignBudget.Text) ? (decimal?)null : decimal.Parse(TxtCampaignBudget.Text),
                        дата_начала = DpCampaignStart.SelectedDate,
                        дата_окончания = DpCampaignEnd.SelectedDate,
                        статус = TxtCampaignStatus.Text
                    };
                    _context.Campaigns.Add(_currentCampaign);
                }
                else
                {
                    // Обновление существующей кампании
                    _currentCampaign.название = TxtCampaignName.Text;
                    _currentCampaign.id_клиента = string.IsNullOrEmpty(TxtCampaignClientId.Text) ? (int?)null : int.Parse(TxtCampaignClientId.Text);
                    _currentCampaign.бюджет = string.IsNullOrEmpty(TxtCampaignBudget.Text) ? (decimal?)null : decimal.Parse(TxtCampaignBudget.Text);
                    _currentCampaign.дата_начала = DpCampaignStart.SelectedDate;
                    _currentCampaign.дата_окончания = DpCampaignEnd.SelectedDate;
                    _currentCampaign.статус = TxtCampaignStatus.Text;
                }

                _context.SaveChanges();
                LoadCampaigns();
                ClearCampaignForm();
                MessageBox.Show("Кампания успешно сохранена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения кампании: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancelCampaign_Click(object sender, RoutedEventArgs e)
        {
            ClearCampaignForm();
        }

        private void ClearCampaignForm()
        {
            TxtCampaignName.Clear();
            TxtCampaignClientId.Clear();
            TxtCampaignBudget.Clear();
            DpCampaignStart.SelectedDate = null;
            DpCampaignEnd.SelectedDate = null;
            TxtCampaignStatus.Clear();
            _currentCampaign = null;
        }

        #endregion

        #region Сотрудники (Employees)

        private void LoadEmployees()
        {
            try
            {
                DgEmployees.ItemsSource = _context.Employees.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки сотрудников: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            ClearEmployeeForm();
            _currentEmployee = null;
        }

        private void BtnEditEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (DgEmployees.SelectedItem == null)
            {
                MessageBox.Show("Выберите сотрудника для редактирования", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _currentEmployee = (Employees)DgEmployees.SelectedItem;
            TxtEmployeeFio.Text = _currentEmployee.ФИО;
            TxtEmployeePosition.Text = _currentEmployee.Должность;
            TxtEmployeeEmail.Text = _currentEmployee.Email;
            TxtEmployeeRate.Text = _currentEmployee.Cтавка_в_час?.ToString();
        }

        private void BtnDeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (DgEmployees.SelectedItem == null)
            {
                MessageBox.Show("Выберите сотрудника для удаления", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Вы уверены, что хотите удалить этого сотрудника?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var employee = (Employees)DgEmployees.SelectedItem;
                    _context.Employees.Remove(employee);
                    _context.SaveChanges();
                    LoadEmployees();
                    ClearEmployeeForm();
                    MessageBox.Show("Сотрудник успешно удален", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления сотрудника: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnRefreshEmployee_Click(object sender, RoutedEventArgs e)
        {
            LoadEmployees();
            ClearEmployeeForm();
        }

        private void BtnSaveEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TxtEmployeeFio.Text))
                {
                    MessageBox.Show("ФИО обязательно для заполнения", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (_currentEmployee == null)
                {
                    // Создание нового сотрудника
                    _currentEmployee = new Employees
                    {
                        ФИО = TxtEmployeeFio.Text,
                        Должность = TxtEmployeePosition.Text,
                        Email = TxtEmployeeEmail.Text,
                        Cтавка_в_час = string.IsNullOrEmpty(TxtEmployeeRate.Text) ? (decimal?)null : decimal.Parse(TxtEmployeeRate.Text)
                    };
                    _context.Employees.Add(_currentEmployee);
                }
                else
                {
                    // Обновление существующего сотрудника
                    _currentEmployee.ФИО = TxtEmployeeFio.Text;
                    _currentEmployee.Должность = TxtEmployeePosition.Text;
                    _currentEmployee.Email = TxtEmployeeEmail.Text;
                    _currentEmployee.Cтавка_в_час = string.IsNullOrEmpty(TxtEmployeeRate.Text) ? (decimal?)null : decimal.Parse(TxtEmployeeRate.Text);
                }

                _context.SaveChanges();
                LoadEmployees();
                ClearEmployeeForm();
                MessageBox.Show("Сотрудник успешно сохранен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения сотрудника: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancelEmployee_Click(object sender, RoutedEventArgs e)
        {
            ClearEmployeeForm();
        }

        private void ClearEmployeeForm()
        {
            TxtEmployeeFio.Clear();
            TxtEmployeePosition.Clear();
            TxtEmployeeEmail.Clear();
            TxtEmployeeRate.Clear();
            _currentEmployee = null;
        }

        #endregion

        protected override void OnClosed(EventArgs e)
        {
            _context.Dispose();
            base.OnClosed(e);
        }
    }
}
