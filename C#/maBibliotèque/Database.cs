namespace Library;

using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

// Our database work with SQLite
public class Database
{
    private static Database? _instance;
    private SqliteConnection? connection;

    private Database()
    {
        connection = new SqliteConnection("Data Source=library.db");
        connection.Open();

        // Création de la table si elle n'existe pas - sans utiliser Instance
        CreateTable("users",
            new string[] { "email TEXT", "password TEXT", "firstname TEXT", "lastname TEXT", "role TEXT" });
        CreateTable("authors", new string[] { "firstname TEXT", "lastname TEXT" });
        CreateTable("categories", new string[] { "name TEXT" });
        CreateTable("books",
            new string[]
            {
                "title TEXT", "author_id INTEGER", "category_id INTEGER", "year INTEGER",
                "FOREIGN KEY(author_id) REFERENCES authors(id)", "FOREIGN KEY(category_id) REFERENCES categories(id)"
            });
    }

    public static Database Instance
    {
        get { return _instance ??= new Database(); }
    }

    // Méthodes comme un ORM qui permettent de faire des requêtes SQL
    public void Insert(SqliteCommand command)
    {
        try
        {
            command.Connection = connection;
            command.ExecuteNonQuery(); // Utiliser ExecuteNonQuery pour les opérations d'insertion
        }
        catch (Exception ex)
        {
            // Gestion des exceptions
            Console.WriteLine("Erreur lors de l'insertion : " + ex.Message);
        }
    }

    /**
     * Create a table with a name and columns
     * @param tableName
     * @param columns
     * @return void
     */
    public void CreateTable(string tableName, string[] columns)
    {
        SqliteCommand? cmd = connection?.CreateCommand();
        if (cmd != null)
        {
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS " + tableName + " (id INTEGER PRIMARY KEY AUTOINCREMENT";
            foreach (string column in columns)
            {
                cmd.CommandText += ", " + column;
            }

            cmd.CommandText += ")";
            cmd.ExecuteNonQuery();
        }
        else
        {
            Console.WriteLine("Erreur lors de la création de la table " + tableName);
        }
    }

    public List<Dictionary<string, string>> Find(SqliteCommand command)
    {
        var results = new List<Dictionary<string, string>>();

        try
        {
            command.Connection = connection;
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var result = new Dictionary<string, string>();
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    result.Add(reader.GetName(i), reader.GetString(i));
                }

                results.Add(result);
            }
        }
        catch (Exception ex)
        {
            // Gestion des exceptions
            Console.WriteLine("Erreur lors de la recherche : " + ex.Message);
        }

        return results;
    }

    public List<Dictionary<string, string>> FindOne(string tableName, string[] columns, string[] values)
    {
        SqliteCommand? cmd = connection?.CreateCommand();
        if (cmd != null)
        {
            cmd.CommandText = "SELECT * FROM " + tableName + " WHERE ";
            for (int i = 0; i < columns.Length; i++)
            {
                cmd.CommandText += columns[i] + " = '" + values[i] + "'";
                if (i < columns.Length - 1)
                {
                    cmd.CommandText += " AND ";
                }
            }

            return Find(cmd);
        }

        return new List<Dictionary<string, string>>();
    }

    // Méthode pour fermer la connexion
    public void CloseConnection()
    {
        if (connection != null)
        {
            connection.Close();
        }
    }
}

// N'oublie pas de fermer la connexion quand tu as fini avec la base de données, par exemple :
// Database.Instance.CloseConnection();