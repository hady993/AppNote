using AppNote.ViewModels;

namespace AppNote.Views;

public partial class NoteView : ContentView
{
	public NoteView()
	{
		InitializeComponent();
		BindingContext = new NoteViewModel();
	}
}