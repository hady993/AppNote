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
        private NoteEntity dataHelper;

        public NoteViewModel()
        {
            noteCollection = new ObservableCollection<Note>();

            dataHelper = new NoteEntity();
            LoadData();
        }

        // Voids Write Data !
        [RelayCommand]
        async Task EditNote()
        {
            if (SelectedNote != null)
            {
                // Set New Note !
                var newNote = new Note
                {
                    Id = SelectedNote.Id,
                    Title = NoteTitle,
                    Description = NoteDescription
                };

                // Remove Note !
                await dataHelper.UpdateDataAsync(newNote);
                LoadData();
            }
        }

        [RelayCommand]
        async Task DeleteNote()
        {
            if (SelectedNote != null)
            {
                await dataHelper.RemoveDataAsync(SelectedNote);
                LoadData();

                // Rest values !
                NoteTitle = string.Empty;
                NoteDescription = string.Empty;
            }
        }

        [RelayCommand]
        async Task AddNote()
        {
            // Set new note !
            var note = new Note
            {
                Title = NoteTitle,
                Description = NoteDescription
            };

            await dataHelper.AddDataAsync(note);
            LoadData();

            // Rest values !
            NoteTitle = string.Empty;
            NoteDescription = string.Empty;
        }

        public void SetData()
        {
            NoteTitle = SelectedNote.Title;
            NoteDescription = SelectedNote.Description;
        }

        public async void LoadData()
        {
            NoteCollection.Clear();
            foreach (var note in await dataHelper.GetAllAsync())
            {
                NoteCollection.Add(note);
            }
        }
    }
}
