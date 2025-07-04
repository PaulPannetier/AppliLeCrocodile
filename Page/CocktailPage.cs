

namespace AppliLeCrocodile
{
    internal class CocktailPage : PageContent
    {
        private Cocktail cocktail;
        private const string cocktailsImageBasePath = "Cocktails";
        private readonly string infoImagePath = Path.Combine("UI", "info.png");

        public CocktailPage(MainPage mainPage, in Cocktail cocktail) : base(mainPage)
        {
            this.cocktail = cocktail;

            string cocktailName = LanguageManager.Instance.GetText(cocktail.nameID);
            title = cocktailName;

            VerticalStackLayout views = new VerticalStackLayout();
            views.HorizontalOptions = LayoutOptions.Fill;
            views.VerticalOptions = LayoutOptions.Fill;

            Label tileLabel = new Label();
            tileLabel.Padding = new Thickness(0d, GetRelativeHeight(30d), 0d, 0d);
            tileLabel.Text = cocktailName;
            tileLabel.FontSize = GetRelativeFontSize(30d);
            tileLabel.FontAttributes = FontAttributes.Bold;
            tileLabel.HorizontalTextAlignment = TextAlignment.Center;
            tileLabel.TextColor = Colors.Black;
            views.Add(tileLabel);

            Image cocktailImage = new Image();
            cocktailImage.Margin = new Thickness(GetRelativeWidth(20d), GetRelativeHeight(20d));
            cocktailImage.HeightRequest = GetRelativeHeight(400d);
            cocktailImage.Source = Path.Combine(cocktailsImageBasePath, cocktail.imagePath);
            cocktailImage.Aspect = Aspect.Fill;
            views.Add(cocktailImage);


            IView ingredientsView = CreateIngredientsView();
            views.Add(ingredientsView);

            content = views;
        }

        private IView CreateIngredientsView()
        {
            VerticalStackLayout views = new VerticalStackLayout();
            views.Padding = new Thickness(GetRelativeWidth(20d), GetRelativeHeight(10d));

            foreach (string ingredientNameID in cocktail.ingredientsNameID)
            {
                Ingredient ingredient = CocktailManager.Instance.GetIngredientByID(ingredientNameID);

                HorizontalStackLayout ingredientsView = new HorizontalStackLayout();
                string ingredientName = LanguageManager.Instance.GetText(ingredientNameID);
                string ingredientDescription = LanguageManager.Instance.GetText(ingredient.descriptionID);

                Label nameLabel = new Label();
                nameLabel.Text = ingredientName;
                nameLabel.FontSize = GetRelativeFontSize(20d);
                nameLabel.FontAttributes = FontAttributes.None;
                nameLabel.HorizontalTextAlignment = TextAlignment.Start;
                nameLabel.TextColor = Colors.Black;
                ingredientsView.Add(nameLabel);

                Image infoImage = new Image();
                infoImage.Source = infoImagePath;
                infoImage.Margin = new Thickness(GetRelativeWidth(0d), GetRelativeHeight(0d));
                infoImage.Aspect = Aspect.Fill;
                ingredientsView.Add(infoImage);

                nameLabel.Loaded += (s, e) =>
                {
                    Size size = nameLabel.Measure(double.PositiveInfinity, double.PositiveInfinity);
                    infoImage.WidthRequest = size.Height;
                    infoImage.HeightRequest = size.Height;
                };

                views.Add(ingredientsView);
            }


            return views;
        }

    }
}
