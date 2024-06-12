using EscritoresApp.Views;
namespace EscritoresApp.Views
{ 
public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}
    private void btnVerAutores_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new verAutores());
    }

    private void btnAgregar_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new nuevoAutor());
    }
}
}