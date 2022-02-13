using System.Windows;

namespace EFTHelper.Models
{
    public class WindowInformations
    {
        #region Constructors

        public WindowInformations()
        {
            Position = new WindowPosition();
            Height = 800;
            Width = 1200;
        }

        #endregion

        #region Properties
        public double Height { get; set; }

        public double Width { get; set; }

        public WindowPosition Position { get; set; }

        #endregion

        #region Methods

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

        #endregion
    }
}
