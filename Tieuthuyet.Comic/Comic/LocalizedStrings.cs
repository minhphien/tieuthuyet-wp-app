using Comic.Resources;

namespace Comic
{
    /// <summary>
    /// Provides access to string resources.
    /// </summary>
    public class LocalizedStrings
    {
        private static AppResources _localizedResources = new AppResources();
        private static Labels _localizedLabels = new Labels();
        public AppResources LocalizedResources { get { return _localizedResources; } }
        public Labels LocalizedLabels { get { return _localizedLabels; } }
    }
}