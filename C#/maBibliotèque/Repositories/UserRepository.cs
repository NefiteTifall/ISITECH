using Library.Models;

namespace Library.Repositories;

using Microsoft.Data.Sqlite;

public class UserRepository : Repository<User>
{
    private string _tableName = "users";

    public void Insert(User item)
    {
        SqliteCommand insertCommand = new SqliteCommand();
        insertCommand.CommandText = "INSERT INTO " + _tableName +
                                    " (email, password, firstname, lastname, role) VALUES (@Email, @Password, @Firstname, @Lastname, @Role)";
        insertCommand.Parameters.AddWithValue("@Email", item.Email);
        insertCommand.Parameters.AddWithValue("@Password", item.Password);
        insertCommand.Parameters.AddWithValue("@Firstname", item.Firstname);
        insertCommand.Parameters.AddWithValue("@Lastname", item.Lastname);
        insertCommand.Parameters.AddWithValue("@Role", item.Role);
        Database.Instance.Insert(insertCommand);
    }

    public List<Dictionary<string, string>> FindAll()
    {
        SqliteCommand insertCommand = new SqliteCommand("SELECT * FROM users");
        return Database.Instance.Find(insertCommand);
    }

    public void Update(User item)
    {
        throw new NotImplementedException();
    }

    public void Delete(User item)
    {
        throw new NotImplementedException();
    }
    
    public User? FindByEmailAndPassword(string email, string password)
    {
        SqliteCommand insertCommand = new SqliteCommand("SELECT * FROM users WHERE email = @Email AND password = @Password");
        insertCommand.Parameters.AddWithValue("@Email", email);
        insertCommand.Parameters.AddWithValue("@Password", password);
        List<Dictionary<string, string>> users = Database.Instance.Find(insertCommand);
        if (users.Count == 0)
        {
            return null;
        }

        Dictionary<string, string> user = users[0];
        return new User(user["email"], user["password"], user["firstname"], user["lastname"], user["role"]);
    }
}