
namespace AppliLeCrocodile
{
    internal class LinkContentPage : ContentPage
    {
        private PanGestureRecognizer panGesture;
        private double panX;

        protected LinkContentPage? previousPage;
        protected LinkContentPage? nextPage;

        public double swipeThreshold;

        public LinkContentPage()
        {
            swipeThreshold = 100d;
        }

        public virtual void Initialize(LinkContentPage? previousPage, LinkContentPage? nextPage)
        {
            this.previousPage = previousPage;
            this.nextPage = nextPage;
        }

        public virtual void Start()
        {

        }

        protected virtual LinkContentPage Clone() => null;

        protected void InitializeSwipe(Layout mainLayout)
        {
            panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += OnPanUpdated;
            mainLayout.GestureRecognizers.Add(panGesture);
        }

        private async Task LoadPreviousPage()
        {
            LinkContentPage previousClone = previousPage.Clone();
            previousClone.nextPage = this;
            previousClone.previousPage = previousPage.previousPage;
            previousClone.Start();
            Console.WriteLine($"Loading previous page {previousClone.Title}");
            await Application.Current.MainPage.Navigation.PushAsync(previousClone);
        }

        private async Task LoadNextPage()
        {
            LinkContentPage nextClone = nextPage.Clone();
            nextClone.previousPage = this;
            nextClone.nextPage = nextPage.nextPage;
            nextClone.Start();
            Console.WriteLine($"Loading next page {nextClone.Title}");
            await Application.Current.MainPage.Navigation.PushAsync(nextClone);
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
