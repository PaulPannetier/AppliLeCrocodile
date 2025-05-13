using System.Text;

namespace AppliLeCrocodile
{
    internal class CocktailsPage : SwipableContent
    {
        public const int nbColumns = 2;
        public const int nbRows = 12;

        private Cocktail[] cocktails;

        public CocktailsPage(MainPage mainPage, Cocktail[] cocktails) : base(mainPage)
        {
            this.cocktails = cocktails;
            title = "CocktailsPage";

            HorizontalStackLayout horizontalStack = new HorizontalStackLayout();
            double horiPadding = GetRelativeWidth(35d);
            double colSpacing = GetRelativeWidth(25d);
            double screenSize = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
            double columnWidth = (screenSize - colSpacing - (2d * horiPadding)) * 0.5d;

            horizontalStack.HorizontalOptions = LayoutOptions.Center;
            horizontalStack.VerticalOptions = LayoutOptions.Start;
            horizontalStack.Padding = new Thickness(horiPadding, GetRelativeHeight(30d), horiPadding, 0d);
            horizontalStack.Spacing = colSpacing;

            for (int j = 0; j < nbColumns; j++)
            {
                VerticalStackLayout column = new VerticalStackLayout();
                column.VerticalOptions = LayoutOptions.Center;
                column.HorizontalOptions = LayoutOptions.Center;
                column.WidthRequest = columnWidth;
                column.Spacing = GetRelativeHeight(5d);

                bool end = false;
                int begCocktailIndex = nbRows * j;
                int endCocktailIndex = j == nbColumns - 1 ? cocktails.Length : begCocktailIndex + nbRows;
                for (int i = begCocktailIndex; i < endCocktailIndex; i++)
                {
                    if(i >= cocktails.Length)
                    {
                        end = true;
                        break;
                    }

                    Layout cocktailLayout = CreateCocktailLayout(cocktails[i]);
                    column.Children.Add(cocktailLayout);
                }

                horizontalStack.Children.Add(column);

                if (end)
                    break;
            }

            content = horizontalStack;
        }

        private Layout CreateCocktailLayout(in Cocktail cocktail)
        {
            VerticalStackLayout views = new VerticalStackLayout();
            Label title = new Label();
            title.Text = LanguageManager.Instance.GetText(cocktail.nameID);
            title.HorizontalOptions = LayoutOptions.Start;
            title.FontSize = GetRelativeFontSize(16d);
            title.TextColor = Colors.Black;
            title.HorizontalTextAlignment = TextAlignment.Center;
            title.FontAttributes = FontAttributes.Bold;
            views.Children.Add(title);

            StringBuilder sb = new StringBuilder();
            foreach(string ingredientID in cocktail.ingredientsNameID)
            {
                string ingredientText = LanguageManager.Instance.GetText(ingredientID);
                sb.Append(ingredientText);
                sb.Append(", ");
            }
            sb.Remove(sb.Length - 2, 2);

            Label ingredient = new Label();
            ingredient.Text = sb.ToString();
            ingredient.FontSize = GetRelativeFontSize(12d);
            ingredient.TextColor = Colors.Black;
            ingredient.HorizontalTextAlignment = TextAlignment.Start;

            views.Children.Add(ingredient);
            return views;
        }
    }
}
