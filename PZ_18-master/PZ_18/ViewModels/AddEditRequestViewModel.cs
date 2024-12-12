using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore; 
using PZ_18.Data;
using PZ_18.Models;

namespace PZ_18.ViewModels
{

    public class AddEditRequestViewModel : INotifyPropertyChanged
    {
        private Request _currentRequest;
        private HomeTechType _selectedHomeTechType;

        public Request CurrentRequest
        {
            get => _currentRequest;
            set
            {
                _currentRequest = value;
                OnPropertyChanged(nameof(CurrentRequest));
            }
        }

        public ObservableCollection<string> StatusOptions { get; } = new ObservableCollection<string>
        {
            "Новая заявка",
            "В процессе ремонта",
            "Ожидание запчастей",
            "Готова к выдаче"
        };

        public ObservableCollection<HomeTechType> HomeTechTypes { get; set; }

        public HomeTechType SelectedHomeTechType
        {
            get => _selectedHomeTechType;
            set
            {
                _selectedHomeTechType = value;
                OnPropertyChanged();
                if (CurrentRequest != null && _selectedHomeTechType != null)
                {
                    CurrentRequest.TechTypeID = _selectedHomeTechType.TechTypeID;
                }
            }
        }

        public ICommand SaveCommand { get; }
        public event Action RequestSaved;

        /// <param name="request">Заявка, которую редактируем или добавляем.</param>
        public AddEditRequestViewModel(Request request = null)
        {
            using (var context = new CoreContext())
            {
                HomeTechTypes = new ObservableCollection<HomeTechType>(context.HomeTechTypes.ToList());
            }

            if (request == null)
            {
                CurrentRequest = new Request
                {
                    StartDate = DateTime.Now,
                    RequestStatus = "Новая заявка"
                };
                if (HomeTechTypes.Count > 0)
                {
                    SelectedHomeTechType = HomeTechTypes[0];
                }
            }
            else
            {
                CurrentRequest = request;
                SelectedHomeTechType = HomeTechTypes.FirstOrDefault(ht => ht.TechTypeID == CurrentRequest.TechTypeID);
            }

            SaveCommand = new RelayCommand(SaveRequest);
        }

        private void SaveRequest(object obj)
        {
            if (SelectedHomeTechType == null)
            {
                MessageBox.Show("Пожалуйста, выберите тип техники.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            CurrentRequest.TechTypeID = SelectedHomeTechType.TechTypeID;

            try
            {
                using (var context = new CoreContext())
                {
                    if (CurrentRequest.RequestID == 0)
                    {
                        context.Requests.Add(CurrentRequest);
                    }
                    else
                    {
                        context.Requests.Update(CurrentRequest);
                    }
                    context.SaveChanges();
                }

                RequestSaved?.Invoke();
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show($"Ошибка при сохранении заявки: {ex.InnerException?.Message ?? ex.Message}", 
                                "Ошибка", 
                                MessageBoxButton.OK, 
                                MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
