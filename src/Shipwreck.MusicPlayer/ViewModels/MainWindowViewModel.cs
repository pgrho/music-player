using System.Linq;
using System.Windows.Input;

namespace Shipwreck.MusicPlayer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public IInteractionService Interaction { get; } = new InteractionService();

        public PlaylistViewModel Playlist { get; } = new PlaylistViewModel();

        #region AddTrackCommand

        private ICommand _AddTrackCommand;

        public ICommand AddTrackCommand
            => _AddTrackCommand ??= new SimpleCommand(() =>
            {
                var fs = Interaction.OpenFiles();
                if (fs?.Length > 0)
                {
                    foreach (var f in fs)
                    {
                        if (!Playlist.Items.Any(e => e.FullPath == f))
                        {
                            Playlist.Items.Add(new MusicViewModel(f));
                        }
                    }
                }
            });

        #endregion AddTrackCommand
    }
}