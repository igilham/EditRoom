using System;
using System.IO;
using System.Windows.Media;

namespace EditRoom
{
    class Options
    {
        public FileInfo Source { get; private set; }
        public string FontFamily { get; private set; }
        public uint FontSize { get; private set; }
        public Color Background { get; private set; }
        public Color Foreground { get; private set; }

        public Options(string source)
        {
            Source = new FileInfo(source);

            // tracks if foreground and background are set in the file
            bool fg = false, bg = false;

            if (Source.Exists)
            {
                // todo: define options file format
                // todo: read in current options
                
            }
            
            // fill in default parameters
            if (FontFamily == null)
                FontFamily = "Consolas";
            if (FontSize == 0)
                FontSize = 14;
            if (!bg)
                Background = Colors.Black;
            if (!fg)
                Foreground = Colors.LimeGreen;
        }
    }
}
