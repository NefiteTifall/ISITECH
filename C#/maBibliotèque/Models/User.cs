namespace Library.Models;

public class User
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string? Firstname { get; set; }
    public string Lastname { get; set; }
    public string Role { get; set; }

    public User(string email, string password, string? firstname, string lastname, string role)
    {
        Email = email;
        Password = password;
        Firstname = firstname;
        Lastname = lastname;
        Role = role;
    }
}