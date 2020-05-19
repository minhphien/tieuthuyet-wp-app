using System;

namespace Comic.Model
{
    public class ContentFormat
    {
        #region Style Properties

        #endregion
        const int NORMAL_SIZE = 30;
        public enum Sizes
        {
            XS = NORMAL_SIZE - 4,
            S = NORMAL_SIZE - 2,
            M = NORMAL_SIZE,
            L = NORMAL_SIZE + 2,
            XL = NORMAL_SIZE + 4
        }
        public enum Styles { light, classic, dark }
        enum Fonts { }
        public Sizes Size { get; set; }
        public Styles Style { get; set; }
        public Sizes SetSize(string SizeName)
        {
            Sizes result = Sizes.M;
            var IsSized = Enum.TryParse<Sizes>(SizeName, out result);
            return result;
        }
        public ContentFormat()
        {
            this.Size = Sizes.M;
            this.Style = Styles.classic;
        }
    }
}
