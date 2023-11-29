using Library.Models;

namespace Library.Repositories;

using Microsoft.Data.Sqlite;

public class BookRepository : Repository<Book>
{
    private string _tableName = "books";

    public void Insert(Book item)
    {
        SqliteCommand insertCommand = new SqliteCommand();
        insertCommand.CommandText = "INSERT INTO " + _tableName +
                                    " (title, author_id, category_id, year) VALUES (@title, @author, @category, @year)";
        insertCommand.Parameters.AddWithValue("@title", item.Title);
        insertCommand.Parameters.AddWithValue("@author",
            Database.Instance.FindOne("authors", new string[] { "firstname", "lastname" },
                new string[] { item.Author.Split(" ")[0], item.Author.Split(" ")[1] })[0]["id"]);
        insertCommand.Parameters.AddWithValue("@category",
            Database.Instance.FindOne("categories", new string[] { "name" }, new string[] { item.Category })[0]["id"]);
        insertCommand.Parameters.AddWithValue("@year", item.Year);
        Database.Instance.Insert(insertCommand);
    }

    public List<Dictionary<string, string>> FindAll()
    {
        SqliteCommand insertCommand = new SqliteCommand("SELECT * FROM " + _tableName);
        return Database.Instance.Find(insertCommand);
    }

    public void Update(Book item)
    {
        throw new NotImplementedException();
    }

    public void Delete(Book item)
    {
        throw new NotImplementedException();
    }
}