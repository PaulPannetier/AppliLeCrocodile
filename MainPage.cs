using Microsoft.Maui.Layouts;
using System.Collections.Generic;

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
            LoadPageContent(new LoadPageContentParam(pages[2], TransitionType.None));
        }

        private void CreatePages(CocktailFilter? filter)
        {
            List<PageContent> currentPages = new List<PageContent>
            {
                new FrontPage(this),
                new SummaryPage(this),
            };

            List<Cocktail> cocktails = CocktailManager.Instance.GetCocktails(filter).ToList();
            List<List<Cocktail>> pagesCocktails = new List<List<Cocktail>>();
            while (cocktails.Count > 0)
            {
                if(cocktails.Count >= CocktailsPage.nbColumns * CocktailsPage.nbRows)
                {
                    pagesCocktails.Add(cocktails[0 .. (CocktailsPage.nbColumns * CocktailsPage.nbRows)].ToList());
                    cocktails.RemoveRange(0, CocktailsPage.nbColumns * CocktailsPage.nbRows);
                    continue;
                }

                if(cocktails.Count == 1)
                {
                    pagesCocktails[pagesCocktails.Count - 1].Add(cocktails[0]);
                    cocktails.Clear();
                    break;
                }

                pagesCocktails.Add(cocktails[0..].ToList());
                cocktails.Clear();
                break;
            }

            foreach(List<Cocktail> pageCocktail in pagesCocktails)
            {
                currentPages.Add(new CocktailsPage(this, pageCocktail.ToArray()));
            }

            currentPages.Add(new SoftPage(this, CocktailManager.Instance.GetSofts(null), CocktailManager.Instance.GetFruitJuice()));
            currentPages.Add(new LastPage(this, CocktailManager.Instance.GetBeers(), CocktailManager.Instance.GetSnacks(), CocktailManager.Instance.GetSodas(), CocktailManager.Instance.GetShooters()));

            pages = currentPages.ToArray();

            ((SwipableContent)pages[0]).Initialize(null, (SwipableContent)pages[1]);
            ((SwipableContent)pages[pages.Length - 1]).Initialize((SwipableContent)pages[pages.Length - 2], null);
            for (int i = 1; i < pages.Length - 1; i++)
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
