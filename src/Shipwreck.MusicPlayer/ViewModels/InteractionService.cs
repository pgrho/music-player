using Microsoft.Win32;
using System.Windows;

namespace Shipwreck.MusicPlayer.ViewModels
{
    public sealed class InteractionService : IInteractionService
    {
        public string[] OpenFiles()
        {
            var mw = Application.Current?.MainWindow;
            var dialog = new OpenFileDialog()
            {
                Filter = "MP3|*.mp3",
                Multiselect = true,
            };
            if (dialog.ShowDialog(Application.Current?.MainWindow) == true)
            {
                return dialog.FileNames;
            }
            return null;
        }
    }
}