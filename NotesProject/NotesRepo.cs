namespace NotesProject
{
    internal class NotesRepo
    {
        Dictionary<int, Note> notes = new();
        HashSet<string> Titles = new();
        public bool Add(Note note)
        {
            if (note is null) 
                return false;
            else
            {
                notes.Add(note.Id, note);
                Titles.Add(note.Title);
                return true;
            }
        }
        public bool Remove(int id)
        {
            if (!notes.ContainsKey(id)) return false;
            notes.Remove(id);
            return true;
        }
        public Note Get(int id)
        {
            if (!notes.ContainsKey(id)) return null;
            return notes[id];
        }
        public List<Note> GetAll()
        {
            List<Note> nn = notes.Values.ToList();
            nn.Sort((x, y) => x.Priority - y.Priority);
            return nn;
        }
        public void Clear()
        {
            notes.Clear();
        }
        public int UpdateNoteTitle(int id, string newTitle)
        {
            var note = Get(id);
            if (note != null && !string.IsNullOrEmpty(newTitle))
            {
                if(Titles.Contains(newTitle)) return -2;
                note.Title = newTitle;
                return 0;
            }
            return -1;
        }
        public bool UpdateNoteContent(int id, string newContent)
        {
            var note = Get(id);
            if (note != null && !string.IsNullOrEmpty(newContent))
            {
                note.Content = newContent;
                return true;
            }
            return false;
        }
        public bool UpdateNotePriority(int id, int newPriority)
        {
            var note = Get(id);
            if (note != null && newPriority <= Note.MaxPriority && newPriority >= Note.MinPriority)
            {
                note.Priority = newPriority;
                return true;
            }
            return false;
        }
        public bool TitleExists(string title)
        {
            return Titles.Contains(title);
        }
    }
}
