using System.Text;

namespace AppliLeCrocodile
{
    internal class SoftPage : SwipableContent
    {
        private Cocktail[] softs;
        private Ingredient[] fruitJuices;

        public SoftPage(MainPage mainPage, Cocktail[] softs, Ingredient[] fruitJuices) : base(mainPage)
        {
            this.softs = softs;
            this.fruitJuices = fruitJuices;

            title = "SoftPage";

            VerticalStackLayout views = new VerticalStackLayout();
            views.HorizontalOptions = LayoutOptions.Fill;
            views.VerticalOptions = LayoutOptions.Fill;


            Label softLabel = new Label();
            softLabel.Text = LanguageManager.Instance.GetText("SOFT_TITLE");
            softLabel.Padding = new Thickness(0d, GetRelativeHeight(50d), 0d, 0d);
            softLabel.FontSize = GetRelativeFontSize(30d);
            softLabel.FontAttributes = FontAttributes.Bold;
            softLabel.HorizontalTextAlignment = TextAlignment.Center;
            softLabel.TextColor = Colors.Black;
            views.Children.Add(softLabel);


            HorizontalStackLayout gridLike = new HorizontalStackLayout();
            gridLike.HorizontalOptions = LayoutOptions.Fill;
            double gridHorizontalPadding = GetRelativeWidth(30d);
            gridLike.Padding = new Thickness(gridHorizontalPadding, GetRelativeHeight(10d), gridHorizontalPadding, 0d);


            double columnHorizontalPadding = GetRelativeWidth(30d); // Distance between the 2 columns
            double columnVerticalPadding = GetRelativeHeight(4d);
            double totalHorizontalPadding = (2d * gridHorizontalPadding) + columnHorizontalPadding;
            double fullScreenWidth = GetRelativeWidth(ApplicationManager.developpementWidth / ApplicationManager.developpementDensity);
            double softWidth = (fullScreenWidth - totalHorizontalPadding) * 0.5d;

            VerticalStackLayout column1 = new VerticalStackLayout();
            column1.Padding = new Thickness(0d, 0d, columnHorizontalPadding * 0.5d, 0d);
            int endIndex = softs.Length % 2 == 0 ? softs.Length / 2 : (softs.Length / 2) + 1;
            for (int i = 0; i < endIndex; i++)
            {
                Layout softView = CreateSoftView(softs[i]);
                softView.WidthRequest = softWidth;
                softView.Padding = new Thickness(0d, columnVerticalPadding, 0d, columnVerticalPadding);
                column1.Children.Add(softView);
            }

            VerticalStackLayout column2 = new VerticalStackLayout();
            column2.Padding = new Thickness(columnHorizontalPadding * 0.5d, 0d, 0d, 0d);
            for (int i = endIndex; i < softs.Length; i++)
            {
                Layout softView = CreateSoftView(softs[i]);
                softView.WidthRequest = softWidth;
                softView.Padding = new Thickness(0d, columnVerticalPadding, 0d, columnVerticalPadding);
                column2.Children.Add(softView);
            }

            gridLike.Children.Add(column1);
            gridLike.Children.Add(column2);
            views.Children.Add(gridLike);


            Label fruitJuiceLabel = new Label();
            fruitJuiceLabel.Text = LanguageManager.Instance.GetText("FRUIT_JUICE_TITLE");
            fruitJuiceLabel.Padding = new Thickness(0d, GetRelativeHeight(40d), 0d, 0d); 
            fruitJuiceLabel.FontSize = GetRelativeFontSize(30d);
            fruitJuiceLabel.FontAttributes = FontAttributes.Bold;
            fruitJuiceLabel.HorizontalTextAlignment = TextAlignment.Center;
            fruitJuiceLabel.TextColor = Colors.Black;
            views.Children.Add(fruitJuiceLabel);

            Label chooseLabel = new Label();
            chooseLabel.Text = LanguageManager.Instance.GetText("CHOOSE");
            chooseLabel.Padding = new Thickness(0d, GetRelativeHeight(4d), 0d, 0d);
            chooseLabel.FontSize = GetRelativeFontSize(14d);
            chooseLabel.HorizontalTextAlignment = TextAlignment.Center;
            chooseLabel.TextColor = Colors.Black;
            views.Children.Add(chooseLabel);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < fruitJuices.Length; i++)
            {
                ref Ingredient ingredient = ref fruitJuices[i];
                sb.Append(LanguageManager.Instance.GetText(ingredient.nameID));
                sb.Append(", ");
            }

            sb.Remove(sb.Length - 2, 2);
            Label juicesLabel = new Label();
            juicesLabel.Text = sb.ToString();
            juicesLabel.Padding = new Thickness(GetRelativeWidth(30d), GetRelativeHeight(2d), GetRelativeWidth(30d), 0d);
            juicesLabel.LineHeight = 1.3d;
            juicesLabel.FontSize = GetRelativeFontSize(14d);
            juicesLabel.HorizontalTextAlignment = TextAlignment.Center;
            juicesLabel.HorizontalOptions = LayoutOptions.Center;
            juicesLabel.TextColor = Colors.Black;
            views.Children.Add(juicesLabel);

            content = views;
        }

        public Layout CreateSoftView(Cocktail soft)
        {
            VerticalStackLayout views = new VerticalStackLayout();
            Label title = new Label();
            title.Text = LanguageManager.Instance.GetText(soft.nameID);
            title.HorizontalOptions = LayoutOptions.Start;
            title.FontSize = GetRelativeFontSize(17d);
            title.TextColor = Colors.Black;
            title.HorizontalTextAlignment = TextAlignment.Center;
            title.FontAttributes = FontAttributes.Bold;
            views.Children.Add(title);

            StringBuilder sb = new StringBuilder();
            foreach (string ingredientID in soft.ingredientsNameID)
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
