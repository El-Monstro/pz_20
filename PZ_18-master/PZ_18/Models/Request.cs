using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PZ_18.Models.Interfaces;

namespace PZ_18.Models
{
	/// <summary>
	/// Заявка на ремонт.
	/// </summary>
	public class Request : INotifyPropertyChanged, IRequest
	{
		private int _requestId;
		private DateTime _startDate;
		private int _techTypeId;
		private string _homeTechModel;
		private string _problemDescription;
		private string _requestStatus;
		private DateTime? _completionDate;
		private int? _masterId;
		private int? _clientId;
		private string _clientFIO;
		private string _clientPhone;

		[Key]
		public int RequestID
		{
			get => _requestId;
			private set { _requestId = value; OnPropertyChanged(); }
		}

		[Required]
		public DateTime StartDate
		{
			get => _startDate;
			set { _startDate = value; OnPropertyChanged(); }
		}

		[Required]
		public int TechTypeID
		{
			get => _techTypeId;
			set { _techTypeId = value; OnPropertyChanged(); }
		}

		[ForeignKey("TechTypeID")]
		[JsonIgnore]
		public virtual HomeTechType HomeTechType { get; private set; }

		// Вычисляемое свойство для имени типа техники
		public string TechTypeName => HomeTechType?.TechTypeName ?? "Не указан";

		[Required]
		[MaxLength(255)]
		public string HomeTechModel
		{
			get => _homeTechModel;
			set { _homeTechModel = value; OnPropertyChanged(); }
		}

		[Required]
		public string ProblemDescription
		{
			get => _problemDescription;
			set { _problemDescription = value; OnPropertyChanged(); }
		}

		[Required]
		[MaxLength(50)]
		public string RequestStatus
		{
			get => _requestStatus;
			set { _requestStatus = value; OnPropertyChanged(); }
		}

		public DateTime? CompletionDate
		{
			get => _completionDate;
			set { _completionDate = value; OnPropertyChanged(); }
		}

		public int? MasterID
		{
			get => _masterId;
			set { _masterId = value; OnPropertyChanged(); }
		}

		[JsonIgnore]
		public virtual User Master { get; private set; }

		public int? ClientID
		{
			get => _clientId;
			set { _clientId = value; OnPropertyChanged(); }
		}

		[JsonIgnore]
		public virtual User Client { get; private set; }

		[Required]
		[MaxLength(255)]
		public string ClientFIO
		{
			get => _clientFIO;
			set { _clientFIO = value; OnPropertyChanged(); }
		}

		[Required]
		[MaxLength(15)]
		public string ClientPhone
		{
			get => _clientPhone;
			set { _clientPhone = value; OnPropertyChanged(); }
		}

		public void UpdateStatus(string newStatus)
		{
			RequestStatus = newStatus;
			if (newStatus == "Готова к выдаче")
			{
				CompletionDate = DateTime.Now;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
