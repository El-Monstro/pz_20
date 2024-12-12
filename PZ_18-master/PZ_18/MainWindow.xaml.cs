using System.Windows;
using PZ_18.ViewModels;
using PZ_18.Views;
using PZ_18.Models;
using PZ_18.Data;

namespace PZ_18
{
    /// <summary>
    /// Главное окно приложения.
    /// Позволяет просматривать и редактировать заявки и пользователей.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddRequest_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddEditRequestWindow();
            addWindow.ShowDialog();

            // После закрытия окна добавления заявки обновим список
            if (DataContext is MainViewModel vm)
            {
                vm.SearchRequests(null);
            }
        }

        private void EditRequest_Click(object sender, RoutedEventArgs e)
        {
            if (RequestDataGrid.SelectedItem is Request selectedRequest)
            {
                var editWindow = new AddEditRequestWindow(selectedRequest);
                editWindow.ShowDialog();

                // После редактирования обновляем список
                if (DataContext is MainViewModel vm)
                {
                    vm.SearchRequests(null);
                }
            }
            else
            {
                MessageBox.Show("Выберите заявку для редактирования");
            }
        }

        private void DeleteRequest_Click(object sender, RoutedEventArgs e)
        {
            if (RequestDataGrid.SelectedItem is Request selectedRequest)
            {
                var result = MessageBox.Show("Удалить выбранную заявку?", "Подтверждение", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    using (var context = new CoreContext())
                    {
                        // Сначала удаляем связанные комментарии
                        var comments = context.Comments.Where(c => c.RequestID == selectedRequest.RequestID).ToList();
                        context.Comments.RemoveRange(comments);

                        // Затем удаляем связанные запчасти
                        var parts = context.RepairParts.Where(p => p.RequestID == selectedRequest.RequestID).ToList();
                        context.RepairParts.RemoveRange(parts);

                        // Теперь удаляем заявку
                        context.Requests.Remove(selectedRequest);
                        context.SaveChanges();
                    }

                    if (DataContext is MainViewModel vm)
                    {
                        vm.SearchRequests(null);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите заявку для удаления");
            }
        }
    }
}
