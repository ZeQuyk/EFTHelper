using System.Windows;

namespace EFTHelper.Models
{
    public class WindowInformations
    {
        public WindowInformations()
        {
            Position = new Position();
            Height = 800;
            Width = 1200;
        }

        public double Height { get; set; }

        public double Width { get; set; }

        public Position Position { get; set; }

        public void Copy(Window window)
        {
            if (window == null)
            {
                return;
            }

            Height = window.Height;
            Width = window.Width;
            Position.Top = window.Top;
            Position.Left = window.Left;
        }
    }
}
