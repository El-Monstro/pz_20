using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PZ_18.Models
{
    /// <summary>
    /// Тип домашней техники.
    /// </summary>
    public class HomeTechType : INotifyPropertyChanged
    {
        private int _techTypeID;
        private string _techTypeName;

        [Key]
        public int TechTypeID
        {
            get => _techTypeID;
            set { _techTypeID = value; OnPropertyChanged(); }
        }

        [Required]
        [MaxLength(255)]
        public string TechTypeName
        {
            get => _techTypeName;
            set { _techTypeName = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}