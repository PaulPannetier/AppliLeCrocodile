
namespace AppliLeCrocodile
{
    public partial class App : Application
    {
        public Action onApplicationSleep;

        public App()
        {
            InitializeComponent();

            Application.Current.Resources = new ResourceDictionary
            {
                new Style(typeof(Label))
                {
                    Setters =
                    {
                        new Setter { Property = Label.FontFamilyProperty, Value = "NextSunday" }
                    }
                },
                new Style(typeof(Button))
                {
                    Setters =
                    {
                        new Setter { Property = Button.FontFamilyProperty, Value = "NextSunday" }
                    }
                },
                new Style(typeof(Entry))
                {
                    Setters =
                    {
                        new Setter { Property = Entry.FontFamilyProperty, Value = "NextSunday" }
                    }
                }
            };

            onApplicationSleep += new Action(() => { });
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            ApplicationManager.Initialize();
            ApplicationManager.Instance.application = this;
            NavigationPage rootPage = new NavigationPage(ApplicationManager.Instance.GetRootPage());
            return new Window(rootPage);
        }

        protected override void OnStart()
        {
            base.OnStart();
            ApplicationManager.Instance.Start();
        }

        protected override void OnSleep()
        {
            base.OnSleep();
            onApplicationSleep.Invoke();
        }
    }
}