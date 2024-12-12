using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PZ_18.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace PZ_18.Models
{
    /// <summary>
    /// Пользователь системы.
    /// </summary>
    public class User : INotifyPropertyChanged, IUser
    {
        private int _userId;
        private string _fio;
        private string _phone;
        private string _login;
        private string _password;
        private int _typeId;

        [Key]
        public int UserID
        {
            get => _userId;
            private set
            {
                _userId = value;
                OnPropertyChanged();
            }
        }

        [Required]
        [MaxLength(255)]
        public string FIO
        {
            get => _fio;
            set
            {
                _fio = value;
                OnPropertyChanged();
            }
        }

        [Required]
        [MaxLength(15)]
        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged();
            }
        }

        [Required]
        [MaxLength(50)]
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        [Required]
        [MaxLength(255)]
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        [Required]
        public int TypeID
        {
            get => _typeId;
            set
            {
                _typeId = value;
                OnPropertyChanged();
            }
        }

        [ForeignKey("TypeID")]
        [JsonIgnore]
        public virtual UserType UserType { get; private set; }

        public void ChangePassword(string newHashedPassword)
        {
            Password = newHashedPassword;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
