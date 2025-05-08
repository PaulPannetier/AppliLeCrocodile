

namespace AppliLeCrocodile
{
    internal class PageContent
    {
        private const double developpementScreenWidthDip = ApplicationManager.developpementWidth / ApplicationManager.developpementDensity;

        protected MainPage mainPage;

        public Layout content {  get; protected set; }  
        public string title {  get; protected set; }

        public PageContent(MainPage mainPage)
        {
            this.mainPage = mainPage;
        }

        public virtual void Start()
        {

        }

        protected double GetRelativeWidth(double width)
        {
            double designPixels = width * ApplicationManager.developpementDensity;
            double screenPercent = designPixels / ApplicationManager.developpementWidth;
            double actualScreenWidthPixels = DeviceDisplay.MainDisplayInfo.Width;
            double targetPixels = screenPercent * actualScreenWidthPixels;
            double responsiveDip = targetPixels / DeviceDisplay.MainDisplayInfo.Density;
            return responsiveDip;
        }

        protected double GetRelativeHeight(double height)
        {
            double designPixels = height * ApplicationManager.developpementDensity;
            double screenPercent = designPixels / ApplicationManager.developpementHeight;
            double actualScreenWidthPixels = DeviceDisplay.MainDisplayInfo.Height;
            double targetPixels = screenPercent * actualScreenWidthPixels;
            double responsiveDip = targetPixels / DeviceDisplay.MainDisplayInfo.Density;
            return responsiveDip;
        }

        protected double GetRelativeFontSize(double fontSize)
        {
            double currentWidthDip = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
            double scaleFactor = currentWidthDip / developpementScreenWidthDip;
            return fontSize * scaleFactor;
        }
    }
}
