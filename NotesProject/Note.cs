using System.Globalization;

namespace NotesProject
{
    internal class Note
    {

        string ToTitleCase(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(str.ToLower());
        }

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
                else { title = ToTitleCase(value); }
            }
        }
        public string Content { get => content; set{ content = value; }}
        public int Priority
        {
            get => priority; 
            set
            {
                if (value > 0 && value < 4)
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
