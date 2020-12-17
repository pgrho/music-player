namespace Shipwreck.MusicPlayer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public PlaylistViewModel Playlist { get; } = new PlaylistViewModel();
    }
}