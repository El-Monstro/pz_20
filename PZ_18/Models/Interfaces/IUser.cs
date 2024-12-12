namespace PZ_18.Models.Interfaces
{
    /// <summary>
    /// Интерфейс пользователя.
    /// </summary>
    public interface IUser
    {
        int UserID { get; }
        string FIO { get; set; }
        string Phone { get; set; }
        string Login { get; set; }
        string Password { get; set; }
        int TypeID { get; set; }

        /// <summary>
        /// Смена пароля.
        /// </summary>
        void ChangePassword(string newHashedPassword);
    }
}