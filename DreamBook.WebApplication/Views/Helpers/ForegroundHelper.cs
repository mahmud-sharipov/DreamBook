using System;
using System.Drawing;

namespace DreamBook.WebApplication.View.Helpers
{
    public static class ForegroundHelper
    {
        public static string GetIdealTextColor(Color bg)
        {
            int nThreshold = 105;
            int bgDelta = Convert.ToInt32((bg.R * 0.299) + (bg.G * 0.587) +
                                          (bg.B * 0.114));

            Color foreColor = (255 - bgDelta < nThreshold) ? Color.Black : Color.White;
            return ColorTranslator.ToHtml(Color.FromArgb(foreColor.ToArgb()));
        }
    }
}
