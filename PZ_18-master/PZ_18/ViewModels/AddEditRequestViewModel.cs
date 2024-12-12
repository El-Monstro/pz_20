using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore; // Добавлено
using PZ_18.Data;
using PZ_18.Models;

namespace PZ_18.ViewModels
{
    /// <summary>
    /// ViewModel для окна добавления/редактирования заявки.
    /// </summary>
    public class AddEditRequestViewModel : INotifyPropertyChanged
    {
        private Request _currentRequest;
        private HomeTechType _selectedHomeTechType;

        /// <summary>
        /// Текущая заявка.
        /// </summary>
        public Request CurrentRequest
        {
            get => _currentRequest;
            set
            {
                _currentRequest = value;
                OnPropertyChanged(nameof(CurrentRequest));
            }
        }

        /// <summary>
        /// Список возможных статусов заявки.
        /// </summary>
        public ObservableCollection<string> StatusOptions { get; } = new ObservableCollection<string>
        {
            "Новая заявка",
            "В процессе ремонта",
            "Ожидание запчастей",
            "Готова к выдаче"
        };

        /// <summary>
        /// Список доступных типов техники, загружаемых из БД.
        /// </summary>
        public ObservableCollection<HomeTechType> HomeTechTypes { get; set; }

        /// <summary>
        /// Выбранный тип техники.
        /// </summary>
        public HomeTechType SelectedHomeTechType
        {
            get => _selectedHomeTechType;
            set
            {
                _selectedHomeTechType = value;
                OnPropertyChanged();
                // При выборе типа техники сразу назначаем TechTypeID заявке
                if (CurrentRequest != null && _selectedHomeTechType != null)
                {
                    CurrentRequest.TechTypeID = _selectedHomeTechType.TechTypeID;
                }
            }
        }

        /// <summary>
        /// Команда для сохранения заявки.
        /// </summary>
        public ICommand SaveCommand { get; }

        /// <summary>
        /// Событие, вызываемое после успешного сохранения заявки.
        /// </summary>
        public event Action RequestSaved;

        /// <summary>
        /// Конструктор ViewModel.
        /// </summary>
        /// <param name="request">Заявка, которую редактируем или добавляем.</param>
        public AddEditRequestViewModel(Request request = null)
        {
            using (var context = new CoreContext())
            {
                // Загружаем типы техники из БД
                HomeTechTypes = new ObservableCollection<HomeTechType>(context.HomeTechTypes.ToList());
            }

            if (request == null)
            {
                // Создаем новую заявку
                CurrentRequest = new Request
                {
                    StartDate = DateTime.Now,
                    RequestStatus = "Новая заявка"
                };
                // По умолчанию выберем первый тип техники, если есть
                if (HomeTechTypes.Count > 0)
                {
                    SelectedHomeTechType = HomeTechTypes[0];
                }
            }
            else
            {
                // Редактируем существующую
                CurrentRequest = request;
                // Найдем соответствующий тип техники в списке
                SelectedHomeTechType = HomeTechTypes.FirstOrDefault(ht => ht.TechTypeID == CurrentRequest.TechTypeID);
            }

            SaveCommand = new RelayCommand(SaveRequest);
        }

        /// <summary>
        /// Логика сохранения заявки.
        /// </summary>
        private void SaveRequest(object obj)
        {
            // Перед сохранением убеждаемся, что выбран тип техники
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

                // Вызываем событие, чтобы закрыть окно
                RequestSaved?.Invoke();
            }
            catch (DbUpdateException ex)
            {
                // Логирование ошибки и уведомление пользователя
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
