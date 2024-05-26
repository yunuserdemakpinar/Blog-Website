namespace Blog_Website.Models.View
{
    public class Users
    {
        public List<User> User { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool AdminRoleCheckbox { get; set; }
    }
}
