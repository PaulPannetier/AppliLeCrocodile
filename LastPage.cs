
namespace AppliLeCrocodile
{
    internal class LastPage : SwipableContent
    {
        private Beer[] beers;
        private Snack[] snacks;

        public LastPage(MainPage mainPage, Beer[] beers, Snack[] snacks) : base(mainPage)
        {
            this.beers = beers;
            this.snacks = snacks;

            title = "Last Page";

            VerticalStackLayout views = new VerticalStackLayout();
            views.HorizontalOptions = LayoutOptions.Fill;
            views.VerticalOptions = LayoutOptions.Fill;

            Label beerTitle = new Label();
            beerTitle.Text = LanguageManager.Instance.GetText("BEER_TITLE");
            beerTitle.FontSize = GetRelativeFontSize(20d);
            beerTitle.FontAttributes = FontAttributes.Bold;
            //beerTitle.TextDecorations = TextDecorations.Underline;
            beerTitle.HorizontalTextAlignment = TextAlignment.Center;
            beerTitle.TextColor = Colors.Black;
            beerTitle.Padding = new Thickness(0d, GetRelativeHeight(30d), 0d, 0d);

            //for beer title underline
            BoxView beerTitleUnderline = new BoxView();
            beerTitleUnderline.HeightRequest = GetRelativeHeight(1d);
            beerTitleUnderline.WidthRequest = beerTitle.Width;
            beerTitleUnderline.BackgroundColor = Colors.Black;
            beerTitleUnderline.HorizontalOptions = LayoutOptions.Fill;
            beerTitleUnderline.Margin = new Thickness(0d, -GetRelativeHeight(4d), 0d, 0d);

            VerticalStackLayout beerTitleGroup = new VerticalStackLayout();
            beerTitleGroup.Spacing = 0d;
            beerTitleGroup.HorizontalOptions = LayoutOptions.Center;
            beerTitleGroup.Children.Add(beerTitle);
            beerTitleGroup.Children.Add(beerTitleUnderline);
            views.Children.Add(beerTitleGroup);

            HorizontalStackLayout beersLayout = new HorizontalStackLayout();
            beersLayout.HorizontalOptions = LayoutOptions.Fill;
            beersLayout.Padding = new Thickness(0d, GetRelativeHeight(30d), 0d, 0d);
            beersLayout.HorizontalOptions = LayoutOptions.Center;
            beersLayout.Spacing = GetRelativeWidth(35d);
            foreach (Beer beer in beers)
            {
                Layout beerLayout = CreateBeerLayout(beer);
                beersLayout.Children.Add(beerLayout);
            }
            views.Children.Add(beersLayout);


            VerticalStackLayout snackTitleGroup = new VerticalStackLayout();
            snackTitleGroup.HorizontalOptions = LayoutOptions.Fill;
            snackTitleGroup.Padding = new Thickness(0d, GetRelativeHeight(20d), 0d, 0d);

            Label snacksTitle = new Label();
            snacksTitle.Text = LanguageManager.Instance.GetText("SNACKS_TITLE");
            snacksTitle.FontSize = GetRelativeFontSize(20d);
            snacksTitle.FontAttributes = FontAttributes.Bold;
            snacksTitle.HorizontalTextAlignment = TextAlignment.Center;
            snacksTitle.TextColor = Colors.Black;

            BoxView snackUnderline = new BoxView();
            snackUnderline.HeightRequest = GetRelativeHeight(1d);
            snackUnderline.WidthRequest = snacksTitle.Width;
            snackUnderline.BackgroundColor = Colors.Black;
            snackUnderline.Color = Colors.Black;
            snackUnderline.HorizontalOptions = LayoutOptions.Fill;
            snackUnderline.Margin = new Thickness(0d, -GetRelativeHeight(4d), 0d, 0d);

            snacksTitle.Loaded += (s, e) =>
            {
                double width = snacksTitle.Measure(double.PositiveInfinity, double.PositiveInfinity).Width;
                snackUnderline.WidthRequest = width;
                snackUnderline.HorizontalOptions = LayoutOptions.Center;
            };

            snackTitleGroup.Children.Add(snacksTitle);
            snackTitleGroup.Children.Add(snackUnderline);
            views.Children.Add(snackTitleGroup);


            VerticalStackLayout snacksLayout = new VerticalStackLayout();
            snacksLayout.HorizontalOptions = LayoutOptions.Center;
            snacksLayout.Spacing = GetRelativeHeight(5d);
            snacksLayout.Padding = new Thickness(0d, GetRelativeHeight(15d), 0d, 0d);

            foreach (Snack snack in snacks)
            {
                Layout snackLayout = CreateSnackLayout(snack);
                snacksLayout.Children.Add(snackLayout);
            }

            views.Children.Add(snacksLayout);


            content = views;
        }

        private Layout CreateSnackLayout(in Snack snack)
        {
            HorizontalStackLayout views = new HorizontalStackLayout();
            views.WidthRequest = GetRelativeWidth(250d);

            Label snackLabel = new Label();
            snackLabel.Text = LanguageManager.Instance.GetText(snack.nameID);
            snackLabel.TextColor = Colors.Black;
            snackLabel.HorizontalOptions = LayoutOptions.Start;
            snackLabel.FontSize = GetRelativeFontSize(14d);
            views.Children.Add(snackLabel);

            Label snackPrice = new Label();
            snackPrice.Text = LanguageManager.Instance.GetText(snack.priceID);
            snackPrice.TextColor = Colors.Black;
            snackPrice.HorizontalOptions = LayoutOptions.End;
            snackPrice.FontSize = GetRelativeFontSize(14d);
            views.Children.Add(snackPrice);

            return views;

        }

        private Layout CreateBeerLayout(in Beer beer)
        {
            VerticalStackLayout beerLayout = new VerticalStackLayout();
            beerLayout.Spacing = GetRelativeHeight(5d);

            VerticalStackLayout beerNameGroup = new VerticalStackLayout();
            beerNameGroup.HorizontalOptions = LayoutOptions.Fill;

            Label beerName = new Label();
            beerName.Text = LanguageManager.Instance.GetText(beer.nameID);
            beerName.FontSize = GetRelativeFontSize(18d);
            beerName.FontAttributes = FontAttributes.Bold;
            beerName.TextColor = Colors.Black;
            beerName.HorizontalOptions = LayoutOptions.Start;
            beerName.HorizontalTextAlignment = TextAlignment.Start;

            BoxView beerNameUnderline = new BoxView();
            beerNameUnderline.HeightRequest = GetRelativeHeight(1d);
            beerNameUnderline.WidthRequest = beerName.Width;
            beerNameUnderline.BackgroundColor = Colors.Black;
            beerNameUnderline.Color = Colors.Black;
            beerNameUnderline.HorizontalOptions = LayoutOptions.Fill;
            beerNameUnderline.Margin = new Thickness(0d, -GetRelativeHeight(4d), 0d, 0d);

            beerNameGroup.Children.Add(beerName);
            beerNameGroup.Children.Add(beerNameUnderline);
            beerLayout.Children.Add(beerNameGroup);

            Label beerDemiPrice = new Label();
            beerDemiPrice.Text = LanguageManager.Instance.GetText(beer.demiPriceId);
            beerDemiPrice.FontSize = GetRelativeFontSize(18d);
            beerDemiPrice.TextColor = Colors.Black;
            beerLayout.Children.Add(beerDemiPrice);

            Label beerPintePrice = new Label();
            beerPintePrice.Text = LanguageManager.Instance.GetText(beer.pintPriceId);
            beerPintePrice.FontSize = GetRelativeFontSize(18d);
            beerPintePrice.TextColor = Colors.Black;
            beerLayout.Children.Add(beerPintePrice);

            beerName.Loaded += (s, e) =>
            {
                double width = beerName.Measure(double.PositiveInfinity, double.PositiveInfinity).Width;
                beerNameUnderline.WidthRequest = width;
                beerNameUnderline.HorizontalOptions = LayoutOptions.Start;
            };

            return beerLayout;
        }
    }
}
