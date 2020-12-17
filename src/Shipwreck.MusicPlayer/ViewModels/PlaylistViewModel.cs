using System.Collections.ObjectModel;

namespace Shipwreck.MusicPlayer.ViewModels
{
    public sealed class PlaylistViewModel : ViewModelBase
    {
        public ObservableCollection<MusicViewModel> Items { get; }
            = new ObservableCollection<MusicViewModel>();
    }
}