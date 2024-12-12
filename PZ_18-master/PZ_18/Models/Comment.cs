using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace PZ_18.Models
{
    /// <summary>
    /// Комментарий к заявке.
    /// </summary>
    public class Comment : INotifyPropertyChanged
    {
        private int _commentID;
        private string _message;
        private int? _masterID;
        private int? _requestID;

        [Key]
        public int CommentID
        {
            get => _commentID;
            set { _commentID = value; OnPropertyChanged(); }
        }

        [Required]
        public string Message
        {
            get => _message;
            set { _message = value; OnPropertyChanged(); }
        }

        public int? MasterID
        {
            get => _masterID;
            set { _masterID = value; OnPropertyChanged(); }
        }

        [JsonIgnore]
        public virtual User Master { get; private set; }

        public int? RequestID
        {
            get => _requestID;
            set { _requestID = value; OnPropertyChanged(); }
        }

        [JsonIgnore]
        public virtual Request Request { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}