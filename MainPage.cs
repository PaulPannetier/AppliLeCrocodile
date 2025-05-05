using Microsoft.Maui.Layouts;

namespace AppliLeCrocodile
{
    internal class MainPage : ContentPage
    {
        private PageContent[] pages;
        private PageContent _currentContent;
        private PageContent currentContent
        {
            get => _currentContent;
            set
            {
                _currentContent = value;
                Content = value.content;
            }
        }

        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
        }

        public void Start()
        {
            CreatePages(null);
            LoadPageContent(new LoadPageContentParam(pages[pages.Length - 3], TransitionType.None));
        }

        private void CreatePages(CocktailFilter? filter)
        {
            Cocktail[] cocktails = CocktailManager.Instance.GetCocktails(filter);
            int nbPages = (int)MathF.Ceiling(cocktails.Length / (float)(CocktailsPage.nbColumns * CocktailsPage.nbRows)) + 4; //+3 for the front page + summary page + soft page + last page

            pages = new PageContent[nbPages];
            pages[0] = new FrontPage(this);
            pages[1] = new SummaryPage(this);
            pages[pages.Length - 2] = new SoftPage(this, CocktailManager.Instance.GetSofts(null), CocktailManager.Instance.GetFruitJuice(null));
            pages[pages.Length - 1] = new LastPage(this);

            int cocktailIndex = 0;
            int endIndexPage = nbPages - 3;
            for (int indexPage = 2; indexPage < nbPages - 2; indexPage++)
            {
                int endIndexCocktail = (int)MathF.Min(cocktailIndex + (CocktailsPage.nbColumns * CocktailsPage.nbRows), cocktails.Length);
                Cocktail[] pageCocktails = cocktails[cocktailIndex..endIndexCocktail];
                pages[indexPage] = new CocktailsPage(this, pageCocktails);
                cocktailIndex = endIndexCocktail;
            }

            ((SwipableContent)pages[0]).Initialize(null, (SwipableContent)pages[1]);
            ((SwipableContent)pages[pages.Length - 1]).Initialize((SwipableContent)pages[pages.Length - 2], null);
            for (int i = 1; i < pages.Length - 2; i++)
            {
                ((SwipableContent)pages[i]).Initialize((SwipableContent)pages[i - 1], (SwipableContent)pages[i + 1]);
            }

            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].Start();
            }
        }

        public async Task LoadPageContent(LoadPageContentParam loadParam)
        {
            if (loadParam.newContent == null || loadParam.newContent.content == null)
                return;

            if (currentContent == null)
            {
                currentContent = loadParam.newContent;
                return;
            }

            switch (loadParam.transitionType)
            {
                case TransitionType.None:
                    currentContent = loadParam.newContent;
                    break;
                case TransitionType.Swipe:
                    await LoadSwipe((SwipePageContentParam)loadParam);
                    break;
                default:
                    break;
            }
        }

        private async Task LoadSwipe(SwipePageContentParam loadParam)
        {
            Layout oldView = currentContent.content;
            Layout newView = loadParam.newContent.content;
            double pageWidth = this.Width;

            double startX = loadParam.left ? -pageWidth : pageWidth;
            newView.TranslationX = startX;

            AbsoluteLayout transitionLayout = new AbsoluteLayout();
            AbsoluteLayout.SetLayoutBounds(oldView, new Rect(0d, 0d, 1d, 1d));
            AbsoluteLayout.SetLayoutFlags(oldView, AbsoluteLayoutFlags.All);

            AbsoluteLayout.SetLayoutBounds(newView, new Rect(0d, 0d, 1d, 1d));
            AbsoluteLayout.SetLayoutFlags(newView, AbsoluteLayoutFlags.All);

            transitionLayout.Children.Add(oldView);
            transitionLayout.Children.Add(newView);

            Content = transitionLayout;

            await Task.WhenAll(
                oldView.TranslateTo(-startX, 0d, SwipableContent.SWIPE_DURATION, Easing.CubicOut),
                newView.TranslateTo(0d, 0d, SwipableContent.SWIPE_DURATION, Easing.CubicOut)
            );

            transitionLayout.Children.Remove(newView);
            transitionLayout.Children.Remove(oldView);
            currentContent = loadParam.newContent;
        }

        #region TransitionParam

        public enum TransitionType : byte
        {
            None,
            Swipe
        }

        public class LoadPageContentParam
        {
            public PageContent newContent;
            public TransitionType transitionType;

            public LoadPageContentParam(PageContent newContent, TransitionType transitionType)
            {
                this.newContent = newContent;
                this.transitionType = transitionType;
            }
        }

        public class SwipePageContentParam : LoadPageContentParam
        {
            public bool left;

            public SwipePageContentParam(PageContent newContent, TransitionType transitionType, bool left) : base(newContent, transitionType)
            {
                this.newContent = newContent;
                this.transitionType = transitionType;
                this.left = left;
            }
        }

        #endregion
    }
}
