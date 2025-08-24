namespace Dezhban.Core.Entities
{
    public class User : BaseEntity
    {
        public bool IsInitialized { get; set; }
        public string Password { get; set; }
    }
}
