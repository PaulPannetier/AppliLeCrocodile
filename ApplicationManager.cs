
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

        private MainPage mainPage;

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

            mainPage = new MainPage();
        }

        public void Start()
        {
            LanguageManager.Instance.Start();
            SettingsManager.Instance.Start();
            CocktailManager.Instance.Start();
            mainPage.Start();
        }

        public Page GetRootPage()
        {
            return mainPage;
        }

        private void OnSleep()
        {
            onSleepCallback.Invoke();
        }
    }
}
