using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesProject
{
    internal class NotesRepo
    {
        Dictionary<int, Note> notes = new();
        public bool Add(Note note)
        {
            if (notes.ContainsKey(note.Id)) return false;
            if (note is null) return false;
            notes.Add(note.Id, note);
            return true;
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
        public bool UpdateNoteTitle(int id, string newTitle)
        {
            var note = Get(id);
            if (note != null && !string.IsNullOrEmpty(newTitle))
            {
                note.Title = newTitle;
                return true;
            }
            return false;
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
            if (note != null && newPriority > 0 && newPriority < 4)
            {
                note.Priority = newPriority;
                return true;
            }
            return false;
        }
    }
}
