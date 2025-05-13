
namespace AppliLeCrocodile
{
    internal class LastPage : SwipableContent
    {
        private Beer[] beers;
        private Snack[] snacks;
        private Soda[] sodas;
        private Shoother[] shooters;

        public LastPage(MainPage mainPage, Beer[] beers, Snack[] snacks, Soda[] sodas, Shoother[] shooters) : base(mainPage)
        {
            this.beers = beers;
            this.snacks = snacks;
            this.sodas = sodas;
            this.shooters = shooters;

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

            //for beer title underline
            BoxView beerTitleUnderline = new BoxView();
            beerTitleUnderline.HeightRequest = GetRelativeHeight(1d);
            beerTitleUnderline.WidthRequest = beerTitle.Width;
            beerTitleUnderline.BackgroundColor = Colors.Black;
            beerTitleUnderline.HorizontalOptions = LayoutOptions.Fill;
            beerTitleUnderline.Margin = new Thickness(0d, -GetRelativeHeight(4d), 0d, 0d);

            VerticalStackLayout beerTitleGroup = new VerticalStackLayout();
            beerTitleGroup.Padding = new Thickness(0d, GetRelativeHeight(40d), 0d, 0d);
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
            snackTitleGroup.Padding = new Thickness(0d, GetRelativeHeight(30d), 0d, 0d);

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
            snacksLayout.HorizontalOptions = LayoutOptions.Fill;
            snacksLayout.Spacing = GetRelativeHeight(5d);
            double horizontalPadding = GetRelativeWidth(40d);
            snacksLayout.Padding = new Thickness(horizontalPadding, GetRelativeHeight(25d), horizontalPadding, 0d);

            foreach (Snack snack in snacks)
            {
                Layout snackLayout = CreateSnackLayout(snack);
                snacksLayout.Children.Add(snackLayout);
            }
            views.Children.Add(snacksLayout);

            VerticalStackLayout sodaTitleGroup = new VerticalStackLayout();
            sodaTitleGroup.Padding = new Thickness(0d, GetRelativeHeight(30), 0d, 0d);
            sodaTitleGroup.HorizontalOptions = LayoutOptions.Center;

            Label sodaTitle = new Label();
            sodaTitle.Text = LanguageManager.Instance.GetText("SODA_TITLE");
            sodaTitle.FontSize = GetRelativeFontSize(20d);
            sodaTitle.TextColor = Colors.Black;
            sodaTitle.FontAttributes = FontAttributes.Bold;

            BoxView sodaUnderline = new BoxView();
            sodaUnderline.HeightRequest = GetRelativeHeight(1d);
            sodaUnderline.WidthRequest = sodaTitle.Width;
            sodaUnderline.BackgroundColor = Colors.Black;
            sodaUnderline.Color = Colors.Black;
            sodaUnderline.HorizontalOptions = LayoutOptions.Fill;
            sodaUnderline.Margin = new Thickness(0d, -GetRelativeHeight(4d), 0d, 0d);

            sodaTitleGroup.Loaded += (s, e) =>
            {
                double width = sodaTitle.Measure(double.PositiveInfinity, double.PositiveInfinity).Width;
                sodaUnderline.WidthRequest = width;
                sodaUnderline.HorizontalOptions = LayoutOptions.Start;
            };

            sodaTitleGroup.Children.Add(sodaTitle);
            sodaTitleGroup.Children.Add(sodaUnderline);
            views.Children.Add(sodaTitleGroup);

            Grid sodaGrid = new Grid();
            sodaGrid.Padding = new Thickness(0d, GetRelativeHeight(25d), 0d, 0d);
            sodaGrid.ColumnSpacing = GetRelativeWidth(40d);
            sodaGrid.HorizontalOptions = LayoutOptions.Center;

            VerticalStackLayout sodasLayoutFirstRow = new VerticalStackLayout();
            sodasLayoutFirstRow.HorizontalOptions = LayoutOptions.Center;
            sodasLayoutFirstRow.Spacing = GetRelativeHeight(5d);
            int endIndex = sodas.Length % 2 == 0 ? sodas.Length >> 1 : (sodas.Length >> 1) + 1;
            for (int i = 0; i < endIndex; i++)
            {
                Layout sodaLayout = CreateSodaLayout(sodas[i]);
                sodasLayoutFirstRow.Children.Add(sodaLayout);
            }
            sodaGrid.Add(sodasLayoutFirstRow, 0, 0);

            VerticalStackLayout sodasLayoutSecondRow = new VerticalStackLayout();
            sodasLayoutSecondRow.HorizontalOptions = LayoutOptions.Center;
            sodasLayoutSecondRow.Spacing = GetRelativeHeight(5d);
            for (int i = endIndex; i < sodas.Length; i++)
            {
                Layout sodaLayout = CreateSodaLayout(sodas[i]);
                sodasLayoutSecondRow.Children.Add(sodaLayout);
            }
            sodaGrid.Add(sodasLayoutSecondRow, 1, 0);
            views.Children.Add(sodaGrid);


            VerticalStackLayout shootersGroup = new VerticalStackLayout();
            shootersGroup.HorizontalOptions = LayoutOptions.Center;
            shootersGroup.Padding = new Thickness(0d, GetRelativeHeight(30d), 0d, 0d);

            Label shooterTitle = new Label();
            shooterTitle.Text = LanguageManager.Instance.GetText("SHOOTERS_TITLE");
            shooterTitle.FontSize = GetRelativeFontSize(20d);
            shooterTitle.TextColor = Colors.Black;
            shooterTitle.FontAttributes = FontAttributes.Bold;

            BoxView shooterUnderline = new BoxView();
            shooterUnderline.HeightRequest = GetRelativeHeight(1d);
            shooterUnderline.WidthRequest = shooterTitle.Width;
            shooterUnderline.BackgroundColor = Colors.Black;
            shooterUnderline.Color = Colors.Black;
            shooterUnderline.HorizontalOptions = LayoutOptions.Fill;
            shooterUnderline.Margin = new Thickness(0d, -GetRelativeHeight(4d), 0d, 0d);

            shootersGroup.Children.Add(shooterTitle);
            shootersGroup.Children.Add(shooterUnderline);
            views.Children.Add(shootersGroup);


            VerticalStackLayout shootersPrice = new VerticalStackLayout();
            shootersPrice.Padding = new Thickness(0d, GetRelativeHeight(25d), 0d, 0d);
            shootersPrice.Spacing = GetRelativeHeight(4d);

            Label oneShooterPrice = new Label();
            oneShooterPrice.Text = LanguageManager.Instance.GetText("ONE_SHOOTER_PRICE");
            oneShooterPrice.FontSize = GetRelativeFontSize(14d);
            oneShooterPrice.HorizontalOptions = LayoutOptions.Center;
            oneShooterPrice.TextColor = Colors.Black;

            Label fiveShooterPrice = new Label();
            fiveShooterPrice.Text = LanguageManager.Instance.GetText("FIVE_SHOOTER_PRICE");
            fiveShooterPrice.FontSize = GetRelativeFontSize(14d);
            fiveShooterPrice.HorizontalOptions = LayoutOptions.Center;
            fiveShooterPrice.TextColor = Colors.Black;

            Label tenShooterPrice = new Label();
            tenShooterPrice.Text = LanguageManager.Instance.GetText("TEN_SHOOTER_PRICE");
            tenShooterPrice.FontSize = GetRelativeFontSize(14d);
            tenShooterPrice.HorizontalOptions = LayoutOptions.Center;
            tenShooterPrice.TextColor = Colors.Black;

            shootersPrice.Children.Add(oneShooterPrice);
            shootersPrice.Children.Add(fiveShooterPrice);
            shootersPrice.Children.Add(tenShooterPrice);
            views.Children.Add(shootersPrice);


            int nbShootersPerCol = (int)MathF.Ceiling(shooters.Length / 3f);
            Grid shootGrid = new Grid();
            shootGrid.ColumnSpacing = GetRelativeWidth(15d);
            shootGrid.RowSpacing = GetRelativeHeight(4d);
            shootGrid.HorizontalOptions = LayoutOptions.Center;
            shootGrid.Padding = new Thickness(0d, GetRelativeHeight(25d), 0d, 0d);

            for (int i = 0; i < nbShootersPerCol; i++)
            {
                Layout shootLayout = CreateShooterLayout(shooters[i]);
                shootGrid.Add(shootLayout, 0, i);
            }

            for (int i = nbShootersPerCol; i < 2 * nbShootersPerCol; i++)
            {
                Layout shootLayout = CreateShooterLayout(shooters[i]);
                shootGrid.Add(shootLayout, 1, i - nbShootersPerCol);
            }

            for (int i = 2 * nbShootersPerCol; i < shooters.Length; i++)
            {
                Layout shootLayout = CreateShooterLayout(shooters[i]);
                shootGrid.Add(shootLayout, 2, i - (2 * nbShootersPerCol));
            }

            views.Children.Add(shootGrid);

            Label menuLabel = new Label();
            menuLabel.Text = LanguageManager.Instance.GetText("BUY_MENU_CARD");
            menuLabel.FontSize = GetRelativeFontSize(14d);
            menuLabel.HorizontalOptions = LayoutOptions.Center;
            menuLabel.HorizontalTextAlignment = TextAlignment.Center;
            menuLabel.LineHeight = 1.3d;
            menuLabel.Padding = new Thickness(GetRelativeWidth(35d), GetRelativeHeight(40d), GetRelativeWidth(35d), 0d);
            menuLabel.TextColor = Colors.Black;

            views.Add(menuLabel);

            content = views;
        }

        private Layout CreateShooterLayout(in Shoother shoot)
        {
            StackLayout stack = new StackLayout();

            Label shootLabel = new Label();
            shootLabel.Text = LanguageManager.Instance.GetText(shoot.nameID);
            shootLabel.HorizontalOptions = LayoutOptions.Center;
            shootLabel.TextColor = Colors.Black;
            shootLabel.FontSize = GetRelativeFontSize(14d);

            stack.Children.Add(shootLabel);

            return stack;
        }

        private Layout CreateSodaLayout(in Soda soda)
        {
            Grid views = new Grid();
            views.ColumnDefinitions = new ColumnDefinitionCollection(
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Auto }
            );
            views.ColumnSpacing = GetRelativeWidth(20d);

            Label sodaLabel = new Label();
            sodaLabel.Text = LanguageManager.Instance.GetText(soda.nameID);
            sodaLabel.TextColor = Colors.Black;
            sodaLabel.HorizontalOptions = LayoutOptions.Start;
            sodaLabel.FontSize = GetRelativeFontSize(14d);
            views.Add(sodaLabel, 0, 0);

            Label sodaPrice = new Label();
            sodaPrice.Text = LanguageManager.Instance.GetText(soda.priceID);
            sodaPrice.TextColor = Colors.Black;
            sodaPrice.HorizontalOptions = LayoutOptions.End;
            sodaPrice.FontSize = GetRelativeFontSize(14d);
            views.Add(sodaPrice, 1, 0);

            return views;
        }

        private Layout CreateSnackLayout(in Snack snack)
        {
            Grid views = new Grid();
            views.ColumnDefinitions = new ColumnDefinitionCollection(
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Auto }
            );

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
            beerDemiPrice.FontSize = GetRelativeFontSize(16.5d);
            beerDemiPrice.TextColor = Colors.Black;
            beerLayout.Children.Add(beerDemiPrice);

            Label beerPintePrice = new Label();
            beerPintePrice.Text = LanguageManager.Instance.GetText(beer.pintPriceId);
            beerPintePrice.FontSize = GetRelativeFontSize(16.5d);
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
