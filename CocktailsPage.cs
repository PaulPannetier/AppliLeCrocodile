using System.Text;

namespace AppliLeCrocodile
{
    internal class CocktailsPage : SwipableContent
    {
        public const int nbColumns = 2;
        public const int nbRows = 11;

        private Cocktail[] cocktails;

        public CocktailsPage(MainPage mainPage, Cocktail[] cocktails) : base(mainPage)
        {
            this.cocktails = cocktails;
            title = "CocktailsPage";

            Grid grid = new Grid();
            grid.BackgroundColor = Colors.White;
            double horizontalPadding = 25d;
            grid.Padding = new Thickness(horizontalPadding, 30d, horizontalPadding, 30d);
            grid.VerticalOptions = LayoutOptions.Fill;

            VerticalStackLayout[] columns = new VerticalStackLayout[nbColumns];
            for (int j = 0; j < nbColumns; j++)
            {
                VerticalStackLayout column = new VerticalStackLayout();
                columns[j] = column;
                column.VerticalOptions = LayoutOptions.Center;
                column.Padding = new Thickness(10d, 0d, 10d, 0f);
                column.Spacing = 11d;

                bool end = false;
                for(int i = 0; i < nbRows; i++)
                {
                    int cocktailIndex = i + (j * nbRows);
                    if(cocktailIndex >= cocktails.Length)
                    {
                        end = true;
                        break;
                    }

                    IView view = CreateCocktailView(cocktails[cocktailIndex]);
                    column.Children.Add(view);
                }

                grid.Add(columns[j], j, 0);

                if (end)
                    break;
            }

            content = grid;
        }

        private IView CreateCocktailView(in Cocktail cocktail)
        {
            VerticalStackLayout views = new VerticalStackLayout();
            Label title = new Label();
            title.Text = LanguageManager.Instance.GetText(cocktail.nameID);
            title.HorizontalOptions = LayoutOptions.Start;
            title.FontSize = 17;
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
            ingredient.FontSize = 12;
            ingredient.TextColor = Colors.Black;
            ingredient.HorizontalTextAlignment = TextAlignment.Start;

            views.Children.Add(ingredient);
            return views;
        }
    }
}
