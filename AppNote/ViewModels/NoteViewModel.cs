using AppNote.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AppNote.Data;

namespace AppNote.ViewModels
{
    public partial class NoteViewModel : ObservableObject
    {
        DBContext db;

        // Fields !
        [ObservableProperty]
        string noteTitle;

        [ObservableProperty]
        string noteDescription;

        [ObservableProperty]
        Note selectedNote;

        [ObservableProperty]
        ObservableCollection<Note> noteCollection;

        public NoteViewModel()
        {
            noteCollection = new ObservableCollection<Note>();
            db = new DBContext();
            var listOfNotes = db.Notes.ToList();
        }

        // Voids Write Data !
        [RelayCommand]
        void EditNote()
        {
            if (SelectedNote != null)
            {
                foreach (Note note in NoteCollection.ToList())
                {
                    if (note == SelectedNote)
                    {
                        // Set New Note !
                        var newNote = new Note
                        {
                            Id = note.Id,
                            Title = NoteTitle,
                            Description = NoteDescription
                        };

                        // Remove Note !
                        NoteCollection.Remove(note);
                        NoteCollection.Add(newNote);
                    }
                }
            }
        }

        [RelayCommand]
        void DeleteNote()
        {
            if (SelectedNote != null)
            {
                NoteCollection.Remove(SelectedNote);

                // Rest values !
                NoteTitle = string.Empty;
                NoteDescription = string.Empty;
            }
        }

        [RelayCommand]
        void AddNote()
        {
            // Generate a unique id !
            int newId = NoteCollection.Count > 0 ? NoteCollection.Max(x => x.Id) + 1 : 1;

            // Set new note !
            var note = new Note
            {
                Id = newId,
                Title = NoteTitle,
                Description = NoteDescription
            };

            // For Test !
            var note1 = new Note
            {
                Title = NoteTitle,
                Description = NoteDescription
            };
            db.Notes.Add(note1);
            db.SaveChanges();

            NoteCollection.Add(note);

            // Rest values !
            NoteTitle = string.Empty;
            NoteDescription = string.Empty;
        }

        public void SetData()
        {
            NoteTitle = SelectedNote.Title;
            NoteDescription = SelectedNote.Description;
        }
    }
}
