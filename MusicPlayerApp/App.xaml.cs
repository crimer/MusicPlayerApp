using MediaManager;
using MusicPlayerApp.View;
using Xamarin.Forms;

namespace MusicPlayerApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //CrossMediaManager.Current.Init();
            MainPage = new NavigationPage(new LandingPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
