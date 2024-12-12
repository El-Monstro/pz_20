using System.Windows;
using PZ_18.ViewModels;
using PZ_18.Models;

namespace PZ_18.Views
{
    /// <summary>
    /// Окно для добавления или редактирования заявки.
    /// </summary>
    public partial class AddEditRequestWindow : Window
    {
        public AddEditRequestWindow(Request request = null)
        {
            InitializeComponent();
            var viewModel = new AddEditRequestViewModel(request);
            viewModel.RequestSaved += () => this.Close();
            this.DataContext = viewModel;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}