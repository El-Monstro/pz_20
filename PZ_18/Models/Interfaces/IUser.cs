namespace PZ_18.Models.Interfaces
{
    public interface IUser
    {
        int UserID { get; }
        string FIO { get; set; }
        string Phone { get; set; }
        string Login { get; set; }
        string Password { get; set; }
        int TypeID { get; set; }

        void ChangePassword(string newHashedPassword);
    }
}