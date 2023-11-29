using Library.Models;
using Library.Repositories;

namespace Library;

class Program
{
    static UserRepository userRepository = new UserRepository();
    static BookRepository bookRepository = new BookRepository();
    static Boolean isFirst = true;
    static User? user = null;

    static Dictionary<string, object> title = new Dictionary<string, object>
    {
        { "text", isFirst ? "*** Bienvenue dans la bibliothèque ***" : "*** Accueil ***" },
        { "isCentered", true },
        { "color", ConsoleColor.DarkCyan }
    };

    static void Main()
    {
        // Créer le dictionnaire pour le titre
        if (user == null) MainWelcome();
        else MainMenu();
    }

    static void MainMenu()
    {
        Dictionary<string, object>[] items = new Dictionary<string, object>[]
        {
            // List des livres, liste des catégories, liste des auteurs, ajouter un livre, enlever un livre, rechercher un livre, quitter
            new Dictionary<string, object>
                { { "text", "1. Rechercher un livre" } },
            new Dictionary<string, object>
                { { "text", "2. Liste des livres" } },
            new Dictionary<string, object>
                { { "text", "3. Liste des catégories" } },
            new Dictionary<string, object>
                { { "text", "4. Liste des auteurs" } },
            new Dictionary<string, object>
                { { "text", "5. Ajouter un livre" } },
            new Dictionary<string, object>
                { { "text", "6. Enlever un livre" } },
            new Dictionary<string, object>
                { { "text", "7. Se déconnecter" } },
            new Dictionary<string, object>
                { { "text", "8. Quitter" } }
        };
        Dictionary<string, object> footer = new Dictionary<string, object>
        {
            { "text", "Connecté en tant que " + user.Firstname + " " + user.Lastname },
            { "isCentered", true },
            { "color", ConsoleColor.DarkCyan }
        };
        string choice = Menu(title, items, footer);
        if (choice == "1")
        {
        }
        else if (choice == "2")
        {
            List<Dictionary<string, string>> books = bookRepository.FindAll();
            if (books.Count == 0)
            {
                Menu(new Dictionary<string, object>
                {
                    { "text", "Il n'y a pas de livre" },
                    { "isCentered", true },
                    { "color", ConsoleColor.Red }
                }, null, null, "continue");
                Main();
            }
            else
            {
                Dictionary<string, object>[]? booksItems = new Dictionary<string, object>[books.Count];
                for (int i = 0; i < books.Count; i++)
                {
                    Dictionary<string, string> book = books[i];
                    items[i] = new Dictionary<string, object>
                    {
                        { "text", book["title"] + " (" + book["year"] + ")" }
                    };
                }

                if (Menu(new Dictionary<string, object>
                    {
                        { "text", "Liste des livres" },
                        { "isCentered", true },
                        { "color", ConsoleColor.DarkCyan }
                    }, booksItems, null, "continue") == "Y") Main();
                else Main();
            }
        }
        else if (choice == "7")
        {
            if (Menu(new Dictionary<string, object>
                {
                    { "text", "Vous vous apprêtez à vous déconnecter!" },
                    { "isCentered", true },
                    { "color", ConsoleColor.Red }
                }, null, null, "continue") == "Y")
            {
                user = null;
                Main();
            }
            else Main();
        }
        else if (choice == "8") exit();
    }

    static void MainWelcome()
    {
        // Créer le tableau pour les items de menu
        Dictionary<string, object>[] startItems = new Dictionary<string, object>[]
        {
            new Dictionary<string, object>
                { { "text", "1. S'authentifier" } },
            new Dictionary<string, object>
                { { "text", "2. S'inscrire" } },
            new Dictionary<string, object>
                { { "text", "3. Liste des utilisateurs" } },
            new Dictionary<string, object>
                { { "text", "4. Quitter" } }
        };
        isFirst = false;
        string choice = Menu(title, startItems, null);
        if (choice == "1")
        {
            Menu(new Dictionary<string, object>
            {
                { "text", "Veillez vous authentifier!" },
                { "isCentered", true },
                { "color", ConsoleColor.Green }
            }, null, null, "alert");
            Console.Write("Adresse email : ");
            string? email = Console.ReadLine();
            Console.Write("Mot de passe : ");
            string? password = ReadPassword();
            if (email != null && password != null) user = userRepository.FindByEmailAndPassword(email, password);
            else
            {
                Console.WriteLine("Vous n'avez pas renseigné d'email ou de mot de passe");
            }

            if (user == null)
            {
                Menu(new Dictionary<string, object>
                {
                    { "text", "Cette combinaison email/mot de passe n'existe pas!" },
                    { "isCentered", true },
                    { "color", ConsoleColor.Red }
                }, null, null, "continue");
                Main();
            }
            else
            {
                Console.WriteLine("Vous êtes connecté en tant que " + user.Firstname + " " + user.Lastname);
                Main();
            }
        }
        else if (choice == "2")
        {
            Console.WriteLine("Vous avez choisi de vous inscrire");
            Console.Write("Prénom : ");
            string? firstname = Console.ReadLine();
            Console.Write("Nom : ");
            string? lastname = Console.ReadLine();
            Console.Write("Email : ");
            string? email = Console.ReadLine();
            Console.Write("Mot de passe : ");
            string? password = ReadPassword();
            Console.Write("Rôle : ");
            string? role = Console.ReadLine();
            User user = new User(email, password, firstname, lastname, role);
            userRepository.Insert(user);
            Console.WriteLine("Vous êtes inscrit !");
            Main();
        }
        else if (choice == "3")
        {
            List<Dictionary<string, string>> users = userRepository.FindAll();
            if (users.Count == 0)
            {
                Console.WriteLine("Il n'y a pas d'utilisateur");
            }
            else
            {
                Dictionary<string, object>[]? items = new Dictionary<string, object>[users.Count];
                for (int i = 0; i < users.Count; i++)
                {
                    Dictionary<string, string> user = users[i];
                    items[i] = new Dictionary<string, object>
                    {
                        { "text", user["firstname"] + " " + user["lastname"] + " (" + user["email"] + ")" }
                    };
                }

                if (Menu(new Dictionary<string, object>
                    {
                        { "text", "Liste des utilisateurs" },
                        { "isCentered", true },
                        { "color", ConsoleColor.DarkCyan }
                    }, items, null, "continue") == "Y") Main();
                else Main();
            }
        }
        else if (choice == "4") exit();
        else Main();
    }

    static Boolean Continuer()
    {
        Console.Write("Continuer (Y) : ");
        string continueChoice = Console.ReadLine();
        if (continueChoice == null || continueChoice == "") continueChoice = "Y";
        if (continueChoice == "Y") return true;
        else return false;
    }

    /***
     * Créer un menu
     * @param Title
     * @paramTitle.isCentered : Boolean
     * @paramTitle.color : ConsoleColor
     * @paramTitle.text : string
     * @param Items
     * @paramItems.text : string
     * @paramItems.color : ConsoleColor
     * @param Footer
     * @paramFooter.isCentered : Boolean
     * @paramFooter.color : ConsoleColor
     * @paramFooter.text : string
     */
    public static string Menu(Dictionary<string, object> title, Dictionary<string, object>[]? items,
        Dictionary<string, object>? footer, string type = "default")
    {
        // CLear console
        Console.Clear();
        // Afficher le titre
        int tableauWidth = 56;
        Console.WriteLine("╔══════════════════════════════════════════════════════╗");
        if (title.ContainsKey("isCentered") && (bool)title["isCentered"])
        {
            int titleLength = ((string)title["text"]).Length;
            int leftPadding =
                (tableauWidth - titleLength) /
                2; // Calculer l'espace nécessaire pour centrer le texte dans le tableau
            string padding = new String(' ', leftPadding - 1);
            Console.Write("║" + padding);
            Console.ForegroundColor = (ConsoleColor)title["color"]; // Définir la couleur du texte
            Console.Write(title["text"]);
            Console.ResetColor(); // Réinitialiser la couleur du texte
            Console.WriteLine(padding + "║");
        }
        else
        {
            Console.Write("║");
            if (title.ContainsKey("color")) Console.ForegroundColor = (ConsoleColor)title["color"];
            Console.Write(title["text"]);
            Console.ResetColor(); // Réinitialiser la couleur du texte
            Console.WriteLine("║");
        }

        // Afficher les items
        if (items != null)
        {
            Console.WriteLine("║------------------------------------------------------║");
            foreach (Dictionary<string, object> item in items)
            {
                int itemLength = ((string)item["text"]).Length;
                int rightPadding = tableauWidth - itemLength - 6;
                string padding = new String(' ', 4);
                Console.Write("║" + padding);

                // if item["color"] is set use the color, else use white
                if (item.ContainsKey("color")) Console.ForegroundColor = (ConsoleColor)item["color"];
                Console.Write(item["text"]);
                Console.ResetColor(); // Réinitialiser la couleur du texte
                padding = new String(' ', rightPadding);
                Console.WriteLine(padding + "║");
            }
        }

        if (footer != null)
        {
            Console.WriteLine("║------------------------------------------------------║");
            if (footer.ContainsKey("isCentered") && (bool)footer["isCentered"])
            {
                int footerLength = ((string)footer["text"]).Length;
                int leftPadding =
                    (tableauWidth - footerLength) /
                    2; // Calculer l'espace nécessaire pour centrer le texte dans le tableau
                string padding = new String(' ', leftPadding - 1);
                Console.Write("║" + padding);
                Console.ForegroundColor = (ConsoleColor)footer["color"]; // Définir la couleur du texte
                Console.Write(footer["text"]);
                Console.ResetColor(); // Réinitialiser la couleur du texte
                Console.WriteLine(padding + "║");
            }
            else
            {
                Console.Write("║");
                Console.ForegroundColor = (ConsoleColor)footer["color"]; // Définir la couleur du texte
                Console.Write(footer["text"]);
                Console.ResetColor(); // Réinitialiser la couleur du texte
                Console.WriteLine("║");
            }
        }

        Console.WriteLine("╚══════════════════════════════════════════════════════╝");
        if (type == "default")
        {
            Console.Write("Votre choix : ");
            string choice = Console.ReadLine();
            if (choice == null) return Menu(title, items, footer);
            return choice;
        }
        else if (type == "continue")
        {
            Console.Write("Continuer (Y) : ");
            string continueChoice = Console.ReadLine();
            if (continueChoice == null || continueChoice == "") continueChoice = "Y";
            if (continueChoice == "Y") return "Y";
            else return "N";
        }
        else
        {
            return "";
        }
    }

    public static string ReadPassword()
    {
        string password = "";
        ConsoleKeyInfo info = Console.ReadKey(true);
        while (info.Key != ConsoleKey.Enter)
        {
            if (info.Key != ConsoleKey.Backspace)
            {
                Console.Write("*");
                password += info.KeyChar;
            }
            else if (info.Key == ConsoleKey.Backspace)
            {
                if (!string.IsNullOrEmpty(password))
                {
                    // Enlève le dernier caractère du mot de passe
                    password = password.Substring(0, password.Length - 1);

                    // Enlève le dernier * de l'écran
                    int pos = Console.CursorLeft;
                    Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    Console.Write(" ");
                    Console.SetCursorPosition(pos - 1, Console.CursorTop);
                }
            }

            info = Console.ReadKey(true);
        }

        // Ajouter une nouvelle ligne après la saisie du mot de passe
        Console.WriteLine();
        return password;
    }

    static void exit()
    {
        Menu(new Dictionary<string, object>
        {
            { "text", "Vous avez quittez le programme, à bientôt!" },
            { "isCentered", true },
            { "color", ConsoleColor.Red }
        }, null, null, "alert");
        Environment.Exit(0);
    }
}