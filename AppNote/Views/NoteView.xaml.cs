using AppNote.ViewModels;

namespace AppNote.Views;

public partial class NoteView : ContentView
{
    private readonly NoteViewModel noteView;

    public NoteView(NoteViewModel noteView)
	{
		InitializeComponent();
		BindingContext = noteView;
        this.noteView = noteView;
    }

    private void ListViewNote_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        // Set data to Title and Description !
        noteView.SetData();
    }
}
