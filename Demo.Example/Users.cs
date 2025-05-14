namespace Demo.Example
{
    public class Users : DataBase, IUsers
    {
        bool _isLoggedIn = false;
        public Users() { }
        public Users(string name) { Name = name; }
        public string Name { get; set; } = "Default";
        public string Description { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public bool Login(string password)
        {
            _isLoggedIn = password?.Length > 8 && password == Password;
            return _isLoggedIn;
        }
    }
}
