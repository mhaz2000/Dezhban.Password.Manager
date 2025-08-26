namespace Dezhban.Core.Entities
{
    public class PasswordModel : BaseEntity
    {
        public string Title { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string AdditionalData { get; set; }
    }
}
