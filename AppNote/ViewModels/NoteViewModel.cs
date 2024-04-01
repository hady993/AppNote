using AppNote.Models;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AppNote.ViewModels
{
    public partial class NoteViewModel : ObservableObject
    {
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
            NoteCollection.Add(note);

            // Rest values !
            NoteTitle = string.Empty;
            NoteDescription = string.Empty;
        }
    }
}
