

namespace AppliLeCrocodile
{
    internal class CocktailsPage : LinkContentPage
    {
        private Cocktail[] cocktails;

        public CocktailsPage(Cocktail[] cocktails)
        {
            this.cocktails = cocktails;
            Title = "CocktailsPage";

            VerticalStackLayout views = new VerticalStackLayout();
            views.Padding = new Thickness(0d, 25f, 0d, 25d);
            views.Spacing = 0;
            views.VerticalOptions = LayoutOptions.Fill;
            views.HorizontalOptions = LayoutOptions.Fill;
            views.BackgroundColor = Colors.WhiteSmoke;

            CreateCocktailLabel(cocktails[0]);


        }

        private IView CreateCocktailLabel(in Cocktail cocktail)
        {
            VerticalStackLayout views = new VerticalStackLayout();
            Label title = new Label();
            title.Text = LanguageManager.Instance.GetText(cocktail.nameID);


            views.Children.Add(title);

            return views;
        }
    }
}
