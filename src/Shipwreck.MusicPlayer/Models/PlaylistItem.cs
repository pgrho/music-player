using System;

namespace Shipwreck.MusicPlayer.Models
{
    public sealed class PlaylistItem
    {
        public PlaylistItem(Uri uri)
        {
            Uri = uri;
        }

        public Uri Uri { get; }
    }
}