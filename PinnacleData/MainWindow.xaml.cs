using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace PinnacleData
{
    public partial class MainWindow : Window
    {
        private FormManager formManager;

        public MainWindow()
        {
            InitializeComponent();
            formManager = new FormManager(DataInputTextBox, SerialNumberTextBox, ResultDataGrid, SerialNumberListBox, CurrentFileLabel);
        }

        private void ProcessButton_Click(object sender, RoutedEventArgs e)
        {
            formManager.ProcessData();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            formManager.ClearForm();
        }

        private void SerialNumberListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SerialNumberListBox.SelectedItem is string selectedSerialNumber)
            {
                formManager.DisplaySelectedData(selectedSerialNumber);
            }
        }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            formManager.SaveDataAs();
        }

        private void LoadMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                DefaultExt = "json"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                formManager.LoadData(openFileDialog.FileName);
                MessageBox.Show("Data loaded successfully!", "Load Complete");
            }
        }
    }
}