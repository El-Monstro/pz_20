using System;

namespace PZ_18.Interfaces
{

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


        void UpdateStatus(string newStatus);
    }
}