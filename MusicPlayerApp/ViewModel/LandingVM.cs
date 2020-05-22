using MusicPlayerApp.Model;
using MusicPlayerApp.View;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace MusicPlayerApp.ViewModel
{
	public class LandingVM : BaseVM
	{
		private ObservableCollection<Music> musicCollection;

		public ObservableCollection<Music> MusicCollection
		{
			get { return musicCollection; }
			set { musicCollection = value; OnPropertyChanged(); }
		}

		private Music recentMusic;
		public Music RecentMusic
		{
			get { return recentMusic; }
			set { recentMusic = value; OnPropertyChanged(); }
		}

		private Music selectedMusic;
		public Music SelectedMusic
		{
			get { return selectedMusic; }
			set { selectedMusic = value; OnPropertyChanged(); }
		}

		public ICommand SelectionCommand => new Command(PlayMusic);

		private void PlayMusic()
		{
			// если музыка выбрана
			if(selectedMusic != null)
			{
				// заполняем vm страницы плеера
				PlayerVM playerVM = new PlayerVM(selectedMusic, musicCollection);
				// соединяем контекст PlayerPage с PlayerVM
				var playerPage = new PlayerPage() { BindingContext = playerVM };
				// получаем главную страницу текущего приложения
				var navigation = Application.Current.MainPage as NavigationPage;
				// переходим на PlayerPage сраницу
				navigation.PushAsync(playerPage, true);
			}
		}
	}
}
