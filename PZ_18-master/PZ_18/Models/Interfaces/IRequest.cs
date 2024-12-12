using System;

namespace PZ_18.Models.Interfaces
{
    /// <summary>
    /// Интерфейс заявки на ремонт.
    /// </summary>
    public interface IRequest
    {
        int RequestID { get; }
        DateTime StartDate { get; set; }
        int TechTypeID { get; set; }
        string HomeTechModel { get; set; }
        string ProblemDescription { get; set; }
        string RequestStatus { get; set; }
        DateTime? CompletionDate { get; set; }
        int? MasterID { get; set; }
        int? ClientID { get; set; }

        /// <summary>
        /// Обновляет статус заявки. Если статус "Готова к выдаче", устанавливается CompletionDate.
        /// </summary>
        void UpdateStatus(string newStatus);
    }
}