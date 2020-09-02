using MusicPlayerApp.Model;
using MusicPlayerApp.View;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace MusicPlayerApp.ViewModel
{
	public class LandingVM : BaseVM
	{
		#region Props
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

		#endregion

		public ICommand SelectionCommand => new Command(PlayMusic);

		public LandingVM()
		{
			musicCollection = GetMusics();
			recentMusic = musicCollection.Where(x => x.IsRecent == true).FirstOrDefault();
		}
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
		private ObservableCollection<Music> GetMusics()
		{
			return new ObservableCollection<Music>
			{
				new Music { Title = "Beach Walk", Band = "Unicorn Heads", Url = "https://devcrux.com/wp-content/uploads/Beach_Walk.mp3", CoverImage = "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcRU6FVly4jMTD3AKB_sHxqPofJVQwqqUj5peEvgA1H4XegM3uJ7&usqp=CAU", IsRecent = true},
				new Music { Title = "I'll Follow You", Band = "Density & Time", Url = "https://devcrux.com/wp-content/uploads/I_ll_Follow_You.mp3", CoverImage = "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcRm-su97lHFGZrbR6BkgL32qbzZBj2f3gKGrFR0Pn66ih01SyGj&usqp=CAU"},
				new Music { Title = "Ancient", Band = "Density & Time", Url = "https://devcrux.com/wp-content/uploads/Ancient.mp3"},
				new Music { Title = "News Room News", Band = "Spence", Url = "https://devcrux.com/wp-content/uploads/Cats_Searching_for_the_Truth.mp3"},
				new Music { Title = "Bro Time", Band = "Nat Keefe & BeatMowe", Url = "https://devcrux.com/wp-content/uploads/Bro_Time.mp3"},
				new Music { Title = "Cats Searching for the Truth", Band = "Nat Keefe & Hot Buttered Rum", Url = "https://devcrux.com/wp-content/uploads/Cats_Searching_for_the_Truth.mp3"}
			};
		}
	}
}
