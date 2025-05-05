
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

            return;

            VerticalStackLayout views = new VerticalStackLayout();
            views.HorizontalOptions = LayoutOptions.Fill;
            views.VerticalOptions = LayoutOptions.Fill;
            views.Padding = new Thickness(30d, 50d, 30d, 50d);

            Label softLabel = new Label();
            softLabel.Text = LanguageManager.Instance.GetText("SOFT_TITLE");
            softLabel.FontSize = 50;
            softLabel.FontAttributes = FontAttributes.Bold;
            softLabel.HorizontalTextAlignment = TextAlignment.Center;
            softLabel.TextColor = Colors.Black;
            views.Children.Add(softLabel);

            return;

            VerticalStackLayout softStack = new VerticalStackLayout();
            softStack.Padding = new Thickness(0d, 0d, 0d, 0d);

            Grid softsGrid = new Grid();
            int endIndex = softs.Length % 2 == 0 ? (softs.Length / 2) - 1 : softs.Length / 2;
            for (int i = 0; i < endIndex; i++)
            {
                IView softView = CreateSoftView(softs[i]);
                softsGrid.Add(softView, 0, i);
            }
            for (int i = endIndex; i < softs.Length; i++)
            {
                IView softView = CreateSoftView(softs[i]);
                softsGrid.Add(softView, 1, i);
            }

            softStack.Children.Add(softsGrid);
            views.Children.Add(softStack);


            Label fruitJuiceLabel = new Label();
            fruitJuiceLabel.Text = LanguageManager.Instance.GetText("FRUIT_JUICE_TITLE");
            fruitJuiceLabel.FontSize = 50;
            fruitJuiceLabel.FontAttributes = FontAttributes.Bold;
            fruitJuiceLabel.HorizontalTextAlignment = TextAlignment.Center;
            fruitJuiceLabel.TextColor = Colors.Black;
            views.Children.Add(softLabel);

            Label chooseLabel = new Label();
            chooseLabel.Text = LanguageManager.Instance.GetText("CHOOSE");
            chooseLabel.FontSize = 20;
            chooseLabel.HorizontalTextAlignment = TextAlignment.Center;
            chooseLabel.TextColor = Colors.Black;
            views.Children.Add(softLabel);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < fruitJuices.Length; i++)
            {
                ref Ingredient ingredient = ref fruitJuices[i];
                sb.Append(LanguageManager.Instance.GetText(ingredient.nameID));
                sb.Append(", ");
            }

            sb.Remove(sb.Length - 2, 2);
            Label juicesLabel = new Label();
            juicesLabel.Padding = new Thickness(20d, 10d);
            juicesLabel.Text = sb.ToString();
            juicesLabel.FontSize = 20;
            juicesLabel.HorizontalTextAlignment = TextAlignment.Center;
            juicesLabel.TextColor = Colors.Black;
            views.Children.Add(juicesLabel);

            content = views;
        }

        public IView CreateSoftView(Cocktail soft)
        {
            VerticalStackLayout views = new VerticalStackLayout();
            Label title = new Label();
            title.Text = LanguageManager.Instance.GetText(soft.nameID);
            title.HorizontalOptions = LayoutOptions.Start;
            title.FontSize = 17;
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
            ingredient.FontSize = 12;
            ingredient.TextColor = Colors.Black;
            ingredient.HorizontalTextAlignment = TextAlignment.Start;

            views.Children.Add(ingredient);
            return views;
        }
    }
}
