
namespace AppliLeCrocodile
{
    internal class ApplicationManager
    {
        public static ApplicationManager Instance { get; private set; }

        public static void Initialize()
        {
            if(Instance == null)
            {
                Instance = new ApplicationManager();
            }
        }

        private LinkContentPage[] pages;

        public const int nbColumns = 2;
        public const int nbRows = 11;

        public const string applicationName = "AppliLeCrocodile";
        public const string relativeSaveDirectory = "Save";

        public Action onSleepCallback;

        private App _application;
        public App application
        {
            get => _application;
            set
            { 
                _application = value;
                _application.onApplicationSleep += OnSleep;
            }
        }

        private ApplicationManager()
        {
            onSleepCallback = new Action(() => { });

            LanguageManager.Initialize();
            SettingsManager.Initialize();
            CocktailManager.Initialize();

            CreatePages(null);
        }

        public void Start()
        {
            LanguageManager.Instance.Start();
            SettingsManager.Instance.Start();
            CocktailManager.Instance.Start();

            foreach(LinkContentPage page in pages)
            {
                page.Start();
            }
        }

        private void CreatePages(CocktailFilter? filter)
        {
            Cocktail[] cocktails = CocktailManager.Instance.GetCocktails(filter);
            int nbPages = (int)MathF.Ceiling(cocktails.Length / (float)(nbColumns * nbRows)) + 4; //+3 for the front page + summary page + soft page + last page

            pages = new LinkContentPage[nbPages];
            pages[0] = new FrontPage();
            pages[1] = new SummaryPage();
            pages[pages.Length - 2] = new SoftPage();
            pages[pages.Length - 1] = new LastPage();

            int cocktailIndex = 0;
            int endIndexPage = nbPages - 3;
            for (int indexPage = 2; indexPage <= endIndexPage; indexPage++)
            {
                int endIndexCocktail = (int)MathF.Min(cocktailIndex + (nbColumns * nbRows), cocktails.Length);
                Cocktail[] pageCocktails = cocktails[cocktailIndex..endIndexCocktail];
                pages[indexPage] = new CocktailsPage(pageCocktails);
                cocktailIndex = endIndexCocktail;
            }

            pages[0].Initialize(null, pages[1]);
            pages[pages.Length - 1].Initialize(pages[pages.Length - 2], null);
            for (int i = 1; i < pages.Length - 2; i++)
            {
                pages[i].Initialize(pages[i - 1], pages[i + 1]);
            }
        }

        public Page GetRootPage()
        {
            return pages[0];
        }

        private void OnSleep()
        {
            onSleepCallback.Invoke();
        }
    }
}
