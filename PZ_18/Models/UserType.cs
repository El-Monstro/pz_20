using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PZ_18.Models
{
    /// <summary>
    /// Тип пользователя (роль).
    /// </summary>
    public class UserType : INotifyPropertyChanged
    {
        private int _typeId;
        private string _typeName;

        [Key]
        public int TypeID
        {
            get => _typeId;
            set { _typeId = value; OnPropertyChanged(); }
        }

        [Required]
        [MaxLength(50)]
        public string TypeName
        {
            get => _typeName;
            set { _typeName = value; OnPropertyChanged(); }
        }

        public override string ToString() => TypeName;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}