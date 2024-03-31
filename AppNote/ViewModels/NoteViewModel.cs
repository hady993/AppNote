using AppNote.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace AppNote.ViewModels
{
    public partial class NoteViewModel : INotifyPropertyChanged
    {
        // Fields !
        private string _noteTitle;
        private string _noteDescription;
        private Note _selectedNote;

        // Get and Set !
        public string NoteTitle
        {
            get => _noteTitle;
            set
            {
                if (_noteTitle != value)
                {
                    _noteTitle = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NoteDescription
        {
            get => _noteDescription;
            set
            {
                if (_noteDescription != value)
                {
                    _noteDescription = value;
                    OnPropertyChanged();
                }
            }
        }

        public Note SelectedNote
        {
            get => _selectedNote;
            set
            {
                if (_selectedNote != value)
                {
                    _selectedNote = value;
                    // Set from list to UI !
                    NoteTitle = SelectedNote.Title;
                    NoteDescription = SelectedNote.Description;
                    OnPropertyChanged();
                }
            }
        }

        // Property !
        public ObservableCollection<Note> NoteCollection { get; set; }
        public ICommand AddNoteCommand { get; }
        public ICommand EditNoteCommand { get; }
        public ICommand RemoveNoteCommand { get; }

        public NoteViewModel()
        {
            NoteCollection = new ObservableCollection<Note>();
            AddNoteCommand = new Command(AddNote);
            RemoveNoteCommand = new Command(DeleteNote);
            EditNoteCommand = new Command(EditNote);
        }

        private void EditNote(object obj)
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

        private void DeleteNote(object obj)
        {
            if (SelectedNote != null)
            {
                NoteCollection.Remove(SelectedNote);

                // Rest values !
                NoteTitle = string.Empty;
                NoteDescription = string.Empty;
            }
        }

        private void AddNote(object obj)
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

        // Property Changed (The main step before any code) !
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
