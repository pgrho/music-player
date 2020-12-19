using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Shipwreck.MusicPlayer.Models
{
    public static class M3uParser
    {
        public static List<PlaylistItem> Load(string fileName)
        {
            var fp = Path.GetFullPath(fileName);
            using (var sr = new FileStream(fp, FileMode.Open))
            {
                return Parse(sr, new Uri(fp)).ToList();
            }
        }

        public static List<PlaylistItem> Parse(Stream stream, Uri baseUri)
        {
            using (var r = new StreamReader(stream, leaveOpen: true))
            {
                return Parse(r, baseUri).ToList();
            }
        }

        public static IEnumerable<PlaylistItem> Parse(TextReader reader, Uri baseUri)
        {
            var isTop = false;
            var isExtended = false;
            for (var l = reader.ReadLine(); l != null; l = reader.ReadLine())
            {
                if (!string.IsNullOrWhiteSpace(l))
                {
                    var v = l.Trim();
                    if (isTop && v == "#EXTM3U")
                    {
                        isExtended = true;
                        isTop = false;
                        continue;
                    }
                    isTop = false;

                    if (v[0] == '#')
                    {
                        if (isExtended && v.StartsWith("#EXTINF:"))
                        {
                            // TODO: read extinf
                        }
                        continue;
                    }

                    if (Uri.TryCreate(v, UriKind.RelativeOrAbsolute, out var u))
                    {
                        // TODO: include extinf
                        yield return new PlaylistItem(u);
                    }
                }
            }
        }
    }
}