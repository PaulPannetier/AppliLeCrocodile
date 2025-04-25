
namespace AppliLeCrocodile
{
    internal class SwipableContent : PageContent
    {
        public const uint SWIPE_DURATION = 250;

        private PanGestureRecognizer panGesture;
        private double panX;

        protected SwipableContent? previousPage;
        protected SwipableContent? nextPage;

        public double swipeThreshold;

        public SwipableContent(MainPage mainPage) : base(mainPage)
        {
            swipeThreshold = 100d;
        }

        public virtual void Initialize(SwipableContent? previousPage, SwipableContent? nextPage)
        {
            this.previousPage = previousPage;
            this.nextPage = nextPage;
        }

        public override void Start()
        {
            base.Start();
            if(content == null)
                return;
            panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += OnPanUpdated;
            content.GestureRecognizers.Add(panGesture);
        }

        private async Task LoadPreviousPage()
        {
            Console.WriteLine($"Loading previous page {previousPage.title}");
            await mainPage.LoadPageContent(new MainPage.SwipePageContentParam(previousPage, MainPage.TransitionType.Swipe, true));
        }

        private async Task LoadNextPage()
        {
            Console.WriteLine($"Loading previous page {nextPage.title}");
            await mainPage.LoadPageContent(new MainPage.SwipePageContentParam(nextPage, MainPage.TransitionType.Swipe, false));
        }

        private async void OnPanUpdated(object? sender, PanUpdatedEventArgs? panArgs)
        {
            if (panArgs == null)
                return;

            switch (panArgs.StatusType)
            {
                case GestureStatus.Running:
                    panX = panArgs.TotalX;
                    break;
                case GestureStatus.Completed:
                    if (nextPage != null && panX < -swipeThreshold)
                    {
                        await LoadNextPage();
                    }
                    else if (previousPage != null && panX > swipeThreshold)
                    {
                        await LoadPreviousPage();
                    }

                    panX = 0;
                    break;
            }
        }
    }
}
