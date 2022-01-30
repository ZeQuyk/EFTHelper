namespace EFTHelper.Models
{
    public interface IMenuItem
    {
        #region Properties

        public string Label { get; }

        public string Title { get; }

        public object Icon { get; }

        #endregion
    }
}
