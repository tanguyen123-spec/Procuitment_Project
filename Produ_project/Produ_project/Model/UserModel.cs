namespace Produ_project.Model
{
    public class UserModel
    {
        public string UserId { get; set; } = null!;
        public string? NameUser { get; set; }
        public bool? Role { get; set; }
        public string? Password { get; set; }
    }
}
