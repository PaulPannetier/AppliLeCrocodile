

using System.Text;

namespace AppliLeCrocodile
{
    internal class CocktailsPage : LinkContentPage
    {
        private Cocktail[] cocktails;
        private Grid grid;

        public CocktailsPage(Cocktail[] cocktails)
        {
            this.cocktails = cocktails;
            Title = "CocktailsPage";

            grid = new Grid();
            grid.Padding = new Thickness(25d, 40d, 25d, 40d);
            grid.VerticalOptions = LayoutOptions.Fill;

            for (int j = 0; j < ApplicationManager.nbColumns; j++)
            {
                VerticalStackLayout column = new VerticalStackLayout();
                column.VerticalOptions = LayoutOptions.Center;
                column.Padding = new Thickness(10d, 0d, 10d, 0f);
                column.Spacing = 11d;

                bool end = false;
                for(int i = 0; i < ApplicationManager.nbRows; i++)
                {
                    int cocktailIndex = i + (j * ApplicationManager.nbRows);
                    if(cocktailIndex >= cocktails.Length)
                    {
                        end = true;
                        break;
                    }

                    IView view = CreateCocktailView(cocktails[cocktailIndex]);
                    column.Children.Add(view);
                }
                
                grid.Add(column, j, 0);

                if (end)
                    break;
            }

            Content = grid;
        }

        public override void Start()
        {
            base.Start();
            base.InitializeSwipe(grid);
        }

        private IView CreateCocktailView(in Cocktail cocktail)
        {
            VerticalStackLayout views = new VerticalStackLayout();
            Label title = new Label();
            title.Text = LanguageManager.Instance.GetText(cocktail.nameID);
            title.HorizontalOptions = LayoutOptions.Start;
            title.FontSize = 19;
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
            ingredient.FontSize = 14;
            ingredient.TextColor = Colors.Black;
            ingredient.HorizontalTextAlignment = TextAlignment.Start;

            views.Children.Add(ingredient);
            return views;
        }

        protected override LinkContentPage Clone()
        {
            return new CocktailsPage(cocktails);
        }
    }
}
