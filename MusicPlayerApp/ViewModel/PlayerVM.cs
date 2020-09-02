using MediaManager;
using MusicPlayerApp.Model;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MusicPlayerApp.ViewModel
{
    public class PlayerVM : BaseVM
    {
        #region Props
        private ObservableCollection<Music> musicCollection;
        public ObservableCollection<Music> MusicCollection
        {
            get { return musicCollection; }
            set { musicCollection = value; OnPropertyChanged(); }
        }

        private Music selectedMusic;
        public Music SelectedMusic
        {
            get { return selectedMusic; }
            set { selectedMusic = value; OnPropertyChanged(); }
        }

        private TimeSpan duration;
        public TimeSpan Duration
        {
            get { return duration; }
            set { duration = value; OnPropertyChanged(); }
        }
        private TimeSpan position;
        public TimeSpan Position
        {
            get { return position; }
            set { position = value; OnPropertyChanged(); }
        }

        private double maximum = 100f;
        public double Maximum
        {
            get { return maximum; }
            set 
            { 
                if(value > 0)
                {
                    maximum = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool isPlaying;

        public bool IsPlaying
        {
            get { return isPlaying; }
            set { isPlaying = value; OnPropertyChanged(); OnPropertyChanged(nameof(PlayIcon)); }
        }
        public string PlayIcon { get => isPlaying ? "ic_pause.png" : "ic_play_arrow.png"; }
        #endregion

        public PlayerVM(Music selectedMusic, ObservableCollection<Music> musicCollection)
        {
            this.selectedMusic = selectedMusic;
            this.musicCollection = musicCollection;
            PlayMusic(selectedMusic);
            isPlaying = true;
        }
        public ICommand PlayCommand => new Command(Play);
        public ICommand ChangeCommand => new Command(ChangeMusic);
        public ICommand BackCommand => new Command(() => Application.Current.MainPage.Navigation.PopAsync());
        public ICommand ShareCommand => new Command(() => Share.RequestAsync(selectedMusic.Url, selectedMusic.Title));


        private async void Play()
        {
            if (isPlaying)
            {
                await CrossMediaManager.Current.Pause();
                IsPlaying = false;
            }
            else
            {
                await CrossMediaManager.Current.Play();
                IsPlaying = true;
            }
        }

        private void ChangeMusic(object obj)
        {
            if ((string)obj == "P")
                PreviousMusic();
            else if ((string)obj == "N")
                NextMusic();
        }

        private async void PlayMusic(Music music)
        {
            var mediaInfo = CrossMediaManager.Current;
            // Запустить можно как ссылку так и файл, см https://theconfuzedsourcecode.wordpress.com/2020/06/28/playing-audio-with-the-mediamanager-plugin-for-xamarin-forms/ 
            await CrossMediaManager.Current.Play("https://ia800605.us.archive.org/32/items/Mp3Playlist_555/Daughtry-Homeacoustic.mp3");
            IsPlaying = true;

            mediaInfo.MediaItemFinished += (sender, args) =>
            {
                IsPlaying = false;
                NextMusic();
            };

            Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
            {
                Duration = mediaInfo.Duration;
                Maximum = duration.TotalSeconds;
                Position = mediaInfo.Position;
                return true;
            });
        }

        private void NextMusic()
        {
            var currentIndex = musicCollection.IndexOf(selectedMusic);

            if (currentIndex < musicCollection.Count - 1)
            {
                SelectedMusic = musicCollection[currentIndex + 1];
                PlayMusic(selectedMusic);
            }
        }

        private void PreviousMusic()
        {
            var currentIndex = musicCollection.IndexOf(selectedMusic);

            if (currentIndex > 0)
            {
                SelectedMusic = musicCollection[currentIndex - 1];
                PlayMusic(selectedMusic);
            }
        }

        //public ICommand PlayCommand => new Command(Play);
        //public ICommand ChangeCommand => new Command(ChangeMusic);
        //public ICommand BackCommand => new Command(() => Application.Current.MainPage.Navigation.PopAsync());
        //public ICommand ShareCommand => new Command(() => Share.RequestAsync(selectedMusic.Url, selectedMusic.Title));

        //private async void Play()
        //{
        //    if (isPlaying)
        //    {
        //        await CrossMediaManager.Current.Pause();
        //        isPlaying = false;
        //    }
        //    else
        //    {
        //        await CrossMediaManager.Current.Play();
        //        isPlaying = true;

        //    }
        //}


        //private void ChangeMusic(object obj)
        //{
        //    if ((string)obj == "P") PrevMusic();
        //    else if ((string)obj == "N") NextMusic();
        //}

        //private void NextMusic()
        //{
        //    var currentMusic = musicCollection.IndexOf(selectedMusic);
        //    if (currentMusic < musicCollection.Count - 1)
        //    {
        //        selectedMusic = musicCollection[currentMusic + 1];
        //        PlayMusic(selectedMusic);
        //    }
        //}

        //private void PrevMusic()
        //{
        //    var currentMusic = musicCollection.IndexOf(selectedMusic);
        //    if (currentMusic > 0)
        //    {
        //        selectedMusic = musicCollection[currentMusic - 1];
        //        PlayMusic(selectedMusic);
        //    }
        //}

        //private async Task PlayMusic(Music music)
        //{
        //    var mediaInfo = CrossMediaManager.Current;
        //    await mediaInfo.Play(music?.Url);
        //    isPlaying = true;

        //    mediaInfo.MediaItemFinished += (sender, args) =>
        //    {
        //        isPlaying = false;
        //        NextMusic();
        //    };

        //    Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
        //    {
        //        duration = mediaInfo.Duration;
        //        maximum = duration.TotalSeconds;
        //        position = mediaInfo.Position;
        //        return true;
        //    });
        //}
    }
}
