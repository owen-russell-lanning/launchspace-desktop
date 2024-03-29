﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace launchspace_desktop.lib
{
    class Constants
    {

        public static readonly SolidColorBrush PRIMARY_COLOR = new SolidColorBrush(Color.FromRgb(33, 33, 33));

        public static readonly SolidColorBrush SECONDARY_COLOR = new SolidColorBrush(Color.FromRgb(28, 28, 27));
        public static readonly SolidColorBrush TERTIARY_COLOR = new SolidColorBrush(Color.FromRgb(66, 66, 66));
        public static readonly SolidColorBrush TERTIARY_HOVER_COLOR = new SolidColorBrush(Color.FromRgb(78, 78, 78));
        public static readonly SolidColorBrush HIGHLIGHT_COLOR = new SolidColorBrush(Color.FromRgb(0, 134, 230));

        public static readonly SolidColorBrush TEXT_COLOR = new SolidColorBrush(Colors.White);
        public static readonly SolidColorBrush SECONDARY_TEXT_COLOR = new SolidColorBrush(Color.FromRgb(130, 130, 129));

        public static readonly int TITLE_FONT_SIZE = 25;
        public static readonly int SUBTITLE_FONT_SIZE = 17;
      
    }
}
