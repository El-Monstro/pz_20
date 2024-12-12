using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace PZ_18.Models
{
    /// <summary>
    /// Запчасть для ремонта.
    /// </summary>
    public class RepairPart : INotifyPropertyChanged
    {
        private int _partID;
        private int _requestID;
        private string _partName;
        private int _quantity;

        [Key]
        public int PartID
        {
            get => _partID;
            set { _partID = value; OnPropertyChanged(); }
        }

        [Required]
        public int RequestID
        {
            get => _requestID;
            set { _requestID = value; OnPropertyChanged(); }
        }

        [JsonIgnore]
        public virtual Request Request { get; private set; }

        [Required]
        [MaxLength(255)]
        public string PartName
        {
            get => _partName;
            set { _partName = value; OnPropertyChanged(); }
        }

        public int Quantity
        {
            get => _quantity;
            set { _quantity = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}