using System.Text;

namespace AppliLeCrocodile
{
    internal class CocktailsPage : SwipableContent
    {
        public const int nbColumns = 2;
        public const int nbRows = 12;

        private Cocktail[] cocktails;
        private StackLayout cocktailsLayout;

        public CocktailsPage(MainPage mainPage, Cocktail[] cocktails) : base(mainPage)
        {
            this.cocktails = cocktails;
            title = "CocktailsPage";


            VerticalStackLayout views = new VerticalStackLayout();
            views.HorizontalOptions = LayoutOptions.Center;
            views.VerticalOptions = LayoutOptions.Start;
            views.Padding = new Thickness(0d, GetRelativeHeight(25d), 0d, 0d);

            SearchBar searchBar = new SearchBar();
            searchBar.HorizontalOptions = LayoutOptions.Center;
            searchBar.Placeholder = LanguageManager.Instance.GetText("SEARCH_BAR_PLACEHOLDER");
            searchBar.TextChanged += OnSearchTextChange;

            views.Add(searchBar);

            cocktailsLayout = new StackLayout();
            cocktailsLayout.Add(CreateCocktailsLayout(cocktails));
            
            views.Add(cocktailsLayout);

            content = views;
        }

        private void OnSearchTextChange(object sender, TextChangedEventArgs e)
        {
            if(e.NewTextValue == null || e.NewTextValue == string.Empty)
            {
                Layout baseCocktailLayout = CreateCocktailsLayout(this.cocktails);
                ChangeCocktailLayout(baseCocktailLayout);
                return;
            }

            string pattern = e.NewTextValue.ToLower();
            PatternCocktailFilter filter = new PatternCocktailFilter(pattern);
            Cocktail[] newCocktails = CocktailManager.Instance.GetCocktails(filter);

            // < 0 => left < right
            // 0 => left = right
            // > 0 => left > right
            int CompareTo(Cocktail left, Cocktail right)
            {
                string s1 = LanguageManager.Instance.GetText(left.nameID);
                string s2 = LanguageManager.Instance.GetText(right.nameID);

                int index1 = s1.IndexOf(pattern, StringComparison.OrdinalIgnoreCase);
                int index2 = s2.IndexOf(pattern, StringComparison.OrdinalIgnoreCase);

                if(index1 == -1 || index2 == -1)
                {
                    if (index1 == index2) 
                        return 0;

                    return index1 == -1 ? 1 : -1;
                }

                return index1.CompareTo(index2);
            }

            Array.Sort(newCocktails, CompareTo);

            if (newCocktails.Length > nbColumns * nbRows)
            {
                newCocktails = newCocktails[0.. (nbColumns * nbRows)];
            }

            Layout cocktailLayout = CreateCocktailsLayout(newCocktails);
            ChangeCocktailLayout(cocktailLayout);
        }

        private void ChangeCocktailLayout(Layout newCocktailsLayout)
        {
            cocktailsLayout.Clear();
            cocktailsLayout.Add(newCocktailsLayout);
        }

        private Layout CreateCocktailsLayout(Cocktail[] cocktails)
        {
            if(cocktails.Length <= 0)
            {
                StackLayout views = new StackLayout();

                Label noCocktailLabel = new Label();
                noCocktailLabel.Text = LanguageManager.Instance.GetText("NO_COCKTAIL");
                noCocktailLabel.Padding = new Thickness(0d, GetRelativeHeight(30d), 0d, 0d);
                noCocktailLabel.HorizontalOptions = LayoutOptions.Center;
                noCocktailLabel.FontSize = GetRelativeFontSize(18d);
                noCocktailLabel.TextColor = Colors.Black;

                views.Add(noCocktailLabel);
                return views;
            }

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
                    if (i >= cocktails.Length)
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

            return horizontalStack;
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
