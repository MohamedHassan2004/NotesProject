using System.Globalization;

namespace NotesProject
{
    internal class Note
    {
        static int count = 0;
        int id;
        string title;
        string content;
        int priority;
        DateTime createdAt;

        public int Id { get => id; }
        public string Title { get => title;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("value");
                }
                else { title = value; }
            }
        }
        public string Content { get => content; set{ content = value; }}
        public static int MaxPriority { get => 3; }
        public static int MinPriority { get => 1; }
        public int Priority
        {
            get => priority; 
            set
            {
                if (value >= MinPriority && value <= MaxPriority)
                {
                    priority = value;
                }
                else
                {
                    throw new System.Exception("Invalid priority");
                }
            }
        }
        public DateTime CreatedAt { get => createdAt; }

        public Note(string _title, string _content, int _priority)
        {
            this.id = ++count;
            Title = _title;
            Content = _content;
            Priority = _priority;
            this.createdAt = DateTime.Now;
        }

        public override string ToString()
        {
            string sparetor = new string('=', 70);
            string note = $"{id}. {title}\tCreated At: {createdAt.ToString("dd-MM-yyyy hh:mm")}" 
                + $"\n{new string('-',50)}\n" +
                $"Content: {content}";
            return sparetor + "\n" + note + "\n" + sparetor;
        }

    }
}
