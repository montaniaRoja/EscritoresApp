namespace EscritoresApp
{
    public partial class App : Application
    {
        public static Controllers.AutorController AutorController { get; private set; }

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Views.MainPage());
            AutorController = new Controllers.AutorController();
        }
    }
}