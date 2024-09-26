namespace NotesProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Note n1 = new("Github", "", 1);
            Note n2 = new("facebook", "", 3);
            Note n3 = new("linkedIn", "", 1);
            Note n4 = new("instagram", "", 3);
            Note n5 = new("stackoverflow", "", 2);

            NotesRepo repo = new();
            repo.Add(n1);
            repo.Add(n2);
            repo.Add(n3);
            repo.Add(n4);
            repo.Add(n5);

            while (true)
                if (ShowOptions(repo) == -1) break;

        }

        static ConsoleColor defaultColor = ConsoleColor.White;
        static void ChangeColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
        static void PriortyColor(int priority)
        {
            if (priority == 1)
            {
                ChangeColor(ConsoleColor.Red);
            }
            else if (priority == 2)
            {
                ChangeColor(ConsoleColor.Yellow);
            }
            else if (priority == 3)
            {
                ChangeColor(ConsoleColor.Green);
            }
        }
        static void ErrorMessage(string message)
        {
            ChangeColor(ConsoleColor.Red);
            Console.WriteLine(message);
            ChangeColor(defaultColor);
        }
        static void SuccessMessage(string message)
        {
            ChangeColor(ConsoleColor.Green);
            Console.WriteLine(message);
            ChangeColor(defaultColor);
        }
        static int GetPriority(string defaultPriority = "3" )
        {
            Console.Write($"Enter priority (range[{Note.MinPriority}:{Note.MaxPriority}] 1 is the most priority): ");
            string inputPriority = Console.ReadLine() ?? "";
            if (string.IsNullOrEmpty(inputPriority)) inputPriority = defaultPriority;
            int priority;
            if (!int.TryParse(inputPriority, out priority))
            {
                ErrorMessage("Invalid priority");
                return GetPriority();
            }
            if (priority < Note.MinPriority || priority > Note.MaxPriority)
            {
                ErrorMessage("Invalid priority (out of range)");
                return GetPriority();
            }
            return priority;
        }
        static int GetNumber(string message)
        {
            Console.Write(message);
            if (!int.TryParse(Console.ReadLine() ?? "-1", out int option))
            {
                ErrorMessage("Invalid option");
                return GetNumber(message);
            }
            return option;
        }
        static void AddNoteUI(NotesRepo repo)

        {
            Console.Write("Enter title: ");
            string? title = Console.ReadLine();
            while(string.IsNullOrEmpty(title))
            {
                ErrorMessage("Title can't be empty");
                Console.Write("Enter title: ");
                title = Console.ReadLine();
            }
            Console.Write("Enter content: ");
            string? content = Console.ReadLine() ?? "";
            int priority = GetPriority();
            Note note = new(title, content, priority);
            repo.Add(note);
            SuccessMessage("Note has been added successfully");
        }
        static void UpdateNoteUI(NotesRepo repo, int id)
        {
            Note? n = repo.Get(id);
            if (n is null)
            {
                ErrorMessage("Note not found");
                return;
            }

            Console.WriteLine("1. Update Title");
            Console.WriteLine("2. Update Content");
            Console.WriteLine("3. Update Priority");
            Console.WriteLine("4. Exit");
            int option = GetNumber("Enter your choice: ");

            switch (option)
            {
                case 1:
                    Console.Write("Enter new title: ");
                    string? title = Console.ReadLine();
                    if (repo.UpdateNoteTitle(id, title))
                        SuccessMessage("Title updated successfully");
                    else
                        ErrorMessage("Title can't be empty");
                    break;
                case 2:
                    Console.Write("Enter new content: ");
                    string? content = Console.ReadLine();
                    if (repo.UpdateNoteContent(id, content))
                        SuccessMessage("Content updated successfully");
                    else
                        ErrorMessage("Content can't be empty");
                    break;
                case 3:
                    Console.Write("Enter new priority: ");
                    if (repo.UpdateNotePriority(id, GetPriority()))
                        SuccessMessage("Priority updated successfully");
                    else
                        ErrorMessage("Invalid priority");
                    break;
                case 4:
                    return;
                default:
                    ErrorMessage("Invalid option");
                    break;
            }
        }
        static void RemoveNoteUI(NotesRepo repo, int id)
        {
            while(!repo.Remove(id))
            {
                ErrorMessage("Note not found");
                id = GetNumber("Enter note id: ");
            }
            SuccessMessage("Note removed successfully");
        }
        static void ShowNotes(NotesRepo repo)
        {
            if (repo.GetAll().Count == 0)
            {
                ErrorMessage("No notes found");
                return;
            }
            repo.GetAll().ForEach(note =>
            {
                PriortyColor(note.Priority);
                Console.WriteLine($"{note.Id}. {note.Title}");
            });
            ChangeColor(defaultColor);
        }
        static void ShowNoteDetails(Note note)
        {
            ChangeColor(ConsoleColor.Blue);
            string separator = new string('^', 60);
            Console.WriteLine(separator);
            Console.WriteLine($"ID: {note.Id}");
            Console.WriteLine($"Title: {note.Title}");
            Console.WriteLine($"Content: {note.Content}");
            Console.WriteLine($"Priority: {note.Priority}");
            Console.WriteLine($"Created At: {note.CreatedAt.ToString("dd-MM-yyyy hh:mm")}");
            Console.WriteLine(separator);
            ChangeColor(defaultColor);
        }
        static int ShowOptions(NotesRepo repo)
        {
            string sparetor = new string('=', 70);
            Console.WriteLine("1. Add Note");
            Console.WriteLine("2. Update Note");
            Console.WriteLine("3. Remove Note");
            Console.WriteLine("4. Show Notes");
            Console.WriteLine("5. Show Note Details");
            Console.WriteLine("6. Exit");
            int Option = GetNumber("Enter your choice : ");
            
            switch (Option)
            {
                case 1:
                    AddNoteUI(repo);
                    break;
                case 2:
                    UpdateNoteUI(repo, GetNumber("Enter note id: "));
                    break;
                case 3:
                    RemoveNoteUI(repo, GetNumber("Enter note id: "));
                    break;
                case 4:
                    Console.WriteLine(sparetor);
                    Console.WriteLine("Your Notes:");
                    Console.WriteLine("===========");
                    ShowNotes(repo);
                    Console.WriteLine(sparetor);
                    break;
                case 5:
                    int id = GetNumber("Enter note id: ");
                    Note note = repo.Get(id);
                    while (note is null)
                    {
                        ErrorMessage("Note not found");
                        id = GetNumber("Enter note id: ");
                        note = repo.Get(id);
                    } 
                    ShowNoteDetails(note);
                    break;
                case 6:
                    Console.WriteLine("Closing...");
                    return -1;
                default:
                    ErrorMessage("Invalid Option!");
                    break;
            }
            return 0;
        }
    }
}
