using MusicPlayerApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerApp.ViewModel
{
    public class PlayerVM
    {
        private Music selectedMusic;
        private ObservableCollection<Music> musicCollection;

        public PlayerVM(Music selectedMusic, ObservableCollection<Music> musicCollection)
        {
            this.selectedMusic = selectedMusic;
            this.musicCollection = musicCollection;
        }
    }
}
