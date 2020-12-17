using Id3;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Shipwreck.MusicPlayer.ViewModels
{
    public sealed class MusicViewModel : ViewModelBase
    {
        internal MusicViewModel(string fullPath)
        {
            FullPath = fullPath;
        }

        public string FullPath { get; }

        public string FileName => Path.GetFileName(FullPath);

        #region Tag

        private Task _TagTask;

        private Task TagTask
            => _TagTask ??= Task.Run(() =>
            {
                using (var fs = new FileStream(FullPath, FileMode.Open))
                using (var mp3 = new Mp3(fs, Mp3Permissions.Read))
                {
                    var tag = mp3.GetTag(Id3TagFamily.Version2X);
                    Album = tag?.Album.Value?.Trim('\0');
                    Title = tag?.Title?.Value?.Trim('\0');
                    Artist = tag?.Artists?.Value?.FirstOrDefault()?.Trim('\0');
                }
            });

        private MusicViewModel BeginLoad()
        {
            TagTask.GetHashCode();
            return this;
        }

        #region Album

        private string _Album;

        public string Album
        {
            get => BeginLoad()._Album;
            private set => SetProperty(ref _Album, value);
        }

        #endregion Album

        #region Title

        private string _Title;

        public string Title
        {
            get => BeginLoad()._Title;
            private set => SetProperty(ref _Title, value);
        }

        #endregion Title

        #region Artist

        private string _Artist;

        public string Artist
        {
            get => BeginLoad()._Artist;
            private set => SetProperty(ref _Artist, value);
        }

        #endregion Artist

        #endregion Tag
    }
}