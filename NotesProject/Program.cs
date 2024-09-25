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
            else
            {
                ChangeColor(ConsoleColor.Green);
            }
        }
        static void AddNoteUI(NotesRepo repo)
        {
            Console.Write("Enter title: ");
            string? title = Console.ReadLine();
            if(string.IsNullOrEmpty(title))
            {
                Console.WriteLine("Title can't be empty");
                return;
            }
            Console.Write("Enter content: ");
            string? content = Console.ReadLine() ?? "";
            Console.Write("Enter priority (range[1,2,3] 1 is the most priority): ");
            string inputPriority = Console.ReadLine();
            if (string.IsNullOrEmpty(inputPriority)) inputPriority = "3";
            int priority = int.Parse(inputPriority);
            if (priority < 1 || priority > 3)
            {
                Console.WriteLine("Invalid priority");
                return;
            }
            Note note = new(title, content, priority);
            repo.Add(note);
            Console.WriteLine("Note added successfully");
        }
        static void UpdateNoteUI(NotesRepo repo, int id)
        {
            Note? n = repo.Get(id);
            if (n is null)
            {
                Console.WriteLine("Note not found");
                return;
            }

            Console.WriteLine("1. Update Title");
            Console.WriteLine("2. Update Content");
            Console.WriteLine("3. Update Priority");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");
            int option = int.Parse(Console.ReadLine() ?? "-1");

            switch (option)
            {
                case 1:
                    Console.Write("Enter new title: ");
                    string? title = Console.ReadLine();
                    if (repo.UpdateNoteTitle(id, title))
                        Console.WriteLine("Title updated successfully");
                    else
                        Console.WriteLine("Title can't be empty");
                    break;
                case 2:
                    Console.Write("Enter new content: ");
                    string? content = Console.ReadLine();
                    if (repo.UpdateNoteContent(id, content))
                        Console.WriteLine("Content updated successfully");
                    else
                        Console.WriteLine("Content can't be empty");
                    break;
                case 3:
                    Console.Write("Enter new priority: ");
                    int priority;
                    if (int.TryParse(Console.ReadLine(), out priority) && repo.UpdateNotePriority(id, priority))
                        Console.WriteLine("Priority updated successfully");
                    else
                        Console.WriteLine("Invalid priority");
                    break;
                case 4:
                    return;
                default:
                    Console.WriteLine("Invalid option - the valid options are [1,2,3]");
                    break;
            }
        }
        static void RemoveNoteUI(NotesRepo repo, int id)
        {
            if(!repo.Remove(id)) Console.WriteLine("Note not found");
            else Console.WriteLine("Note removed successfully");
        }
        static void ShowNotes(NotesRepo repo)
        {
            repo.GetAll().ForEach(note =>
            {
                PriortyColor(note.Priority);
                Console.WriteLine($"{note.Id}. {note.Title}");
            });
            ChangeColor(defaultColor);


        }
        static void ShowNoteDetails(Note note)
        {
            string separator = new string('*',60);
            Console.WriteLine(separator);
            Console.WriteLine($"Title: {note.Title}");
            Console.WriteLine($"Content: {note.Content}");
            Console.WriteLine($"Priority: {note.Priority}");
            Console.WriteLine(separator);
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
            int EditOption = int.Parse(Console.ReadLine() ?? "-1");
            switch (EditOption)
            {
                case 1:
                    AddNoteUI(repo);
                    break;
                case 2:
                    Console.Write("Enter note id: ");
                    int id = int.Parse(Console.ReadLine() ?? "-1");
                    UpdateNoteUI(repo, id);
                    break;
                case 3:
                    Console.Write("Enter note id: ");
                    int id2 = int.Parse(Console.ReadLine() ?? "-1");
                    RemoveNoteUI(repo, id2);
                    break;
                case 4:
                    Console.WriteLine(sparetor);
                    Console.WriteLine("Your Notes");
                    Console.WriteLine("==========");
                    ShowNotes(repo);
                    Console.WriteLine(sparetor);
                    break;
                case 5:
                    Console.Write("Enter note id: ");
                    int id3 = int.Parse(Console.ReadLine() ?? "-1");
                    Note? note = repo.Get(id3);
                    if(note is null) Console.WriteLine("Note not found");
                    else ShowNoteDetails(note);
                    break;
                case 6:
                    return -1;
                default:
                    Console.WriteLine("Invalid Option!");
                    break;
            }
            return 0;
        }
    }
}
