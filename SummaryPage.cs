
namespace AppliLeCrocodile
{
    internal class SummaryPage : SwipableContent
    {
        public SummaryPage(MainPage mainPage) : base(mainPage)
        {
            base.title = "SummaryPage";

            VerticalStackLayout views = new VerticalStackLayout();
            views.Padding = new Thickness(0d, GetRelativeHeight(40d), 0d, 0d);
            views.Spacing = 0d;
            views.VerticalOptions = LayoutOptions.Fill;
            views.HorizontalOptions = LayoutOptions.Fill;
            views.BackgroundColor = Colors.WhiteSmoke;

            Label title = new Label();
            title.Text = LanguageManager.Instance.GetText("BAR_NAME");
            title.FontSize = GetRelativeFontSize(50d);
            title.FontAttributes = FontAttributes.Bold;
            title.HorizontalTextAlignment = TextAlignment.Center;
            title.TextColor = Colors.Black;
            views.Children.Add(title);

            Label barDescription = new Label();
            barDescription.Padding = new Thickness(0d, GetRelativeHeight(5d), 0d, 0d);
            barDescription.Text = LanguageManager.Instance.GetText("BAR_DESCRIPTION");
            barDescription.FontSize = GetRelativeFontSize(12d);
            barDescription.TextColor = Colors.Black;
            barDescription.HorizontalTextAlignment = TextAlignment.Center;
            views.Children.Add(barDescription);


            VerticalStackLayout barOpen = new VerticalStackLayout();
            barOpen.Padding = new Thickness(0d, GetRelativeHeight(25d), 0d, GetRelativeHeight(25d));
            barOpen.Spacing = GetRelativeHeight(7d);

            Label barOpeningDays = new Label();
            barOpeningDays.Text = LanguageManager.Instance.GetText("BAR_OPENING_DAYS");
            barOpeningDays.FontSize = GetRelativeFontSize(23d);
            barOpeningDays.TextColor = Colors.Black;
            barOpeningDays.FontAttributes = FontAttributes.Bold;
            barOpeningDays.HorizontalTextAlignment = TextAlignment.Center;
            barOpen.Children.Add(barOpeningDays);

            Label barOpeningHours = new Label();
            barOpeningHours.Text = LanguageManager.Instance.GetText("BAR_OPENING_HOURS");
            barOpeningHours.FontSize = GetRelativeFontSize(23d);
            barOpeningHours.TextColor = Colors.Black;
            barOpeningHours.FontAttributes = FontAttributes.Bold;
            barOpeningHours.HorizontalTextAlignment = TextAlignment.Center;
            barOpen.Children.Add(barOpeningHours);

            Label barClose = new Label();
            barClose.Text = LanguageManager.Instance.GetText("BAR_CLOSE_DAYS");
            barClose.FontSize = GetRelativeFontSize(23d);
            barClose.TextColor = Colors.Black;
            barClose.FontAttributes = FontAttributes.Bold;
            barClose.HorizontalTextAlignment = TextAlignment.Center;
            barOpen.Children.Add(barClose);

            views.Children.Add(barOpen);


            Label happyHour = new Label();
            happyHour.Padding = new Thickness(0d, GetRelativeHeight(5d), 0d, GetRelativeHeight(0d));
            happyHour.Text = LanguageManager.Instance.GetText("HAPPY_HOUR");
            happyHour.FontSize = GetRelativeFontSize(18d);
            happyHour.TextColor = Colors.Black;
            //happyHour.TextDecorations = TextDecorations.Underline;
            happyHour.FontAttributes = FontAttributes.Bold;
            happyHour.HorizontalTextAlignment = TextAlignment.Center;

            BoxView happyHourUnderline = new BoxView();
            happyHourUnderline.HeightRequest = GetRelativeHeight(1d);
            happyHourUnderline.WidthRequest = happyHour.Width;
            happyHourUnderline.BackgroundColor = Colors.Black;
            happyHourUnderline.HorizontalOptions = LayoutOptions.Fill;
            happyHourUnderline.Margin = new Thickness(0d, -GetRelativeHeight(4d), 0d, 0d);

            VerticalStackLayout hhGroup = new VerticalStackLayout();
            hhGroup.Spacing = 0d;
            hhGroup.HorizontalOptions = LayoutOptions.Center;
            hhGroup.Children.Add(happyHour);
            hhGroup.Children.Add(happyHourUnderline);
            views.Children.Add(hhGroup);


            VerticalStackLayout prices = new VerticalStackLayout();
            prices.Padding = new Thickness(0d, GetRelativeHeight(25d), 0d, GetRelativeHeight(20d));
            prices.Spacing = GetRelativeHeight(4d);

            Label hhDays = new Label();
            hhDays.Text = LanguageManager.Instance.GetText("HAPPY_HOURS_DAYS");
            hhDays.FontSize = GetRelativeFontSize(18d);
            hhDays.TextColor = Colors.Black;
            hhDays.HorizontalTextAlignment = TextAlignment.Center;
            prices.Children.Add(hhDays);

            Label hhHours = new Label();
            hhHours.Text = LanguageManager.Instance.GetText("HAPPY_HOURS_HOURS");
            hhHours.FontSize = GetRelativeFontSize(18d);
            hhHours.TextColor = Colors.Black;
            hhHours.HorizontalTextAlignment = TextAlignment.Center;
            prices.Children.Add(hhHours);

            Label weekHhCocktailPrice = new Label();
            weekHhCocktailPrice.Text = LanguageManager.Instance.GetText("HAPPY_HOURS_COCKTAILS_PRICE");
            weekHhCocktailPrice.FontSize = GetRelativeFontSize(18d);
            weekHhCocktailPrice.TextColor = Colors.Black;
            weekHhCocktailPrice.HorizontalTextAlignment = TextAlignment.Center;
            weekHhCocktailPrice.FontAttributes = FontAttributes.Bold;
            prices.Children.Add(weekHhCocktailPrice);

            Label weekCocktailPrice = new Label();
            weekCocktailPrice.Text = LanguageManager.Instance.GetText("SUMMARY_COCKTAILS_PRICE_WEEK");
            weekCocktailPrice.FontSize = GetRelativeFontSize(18d);
            weekCocktailPrice.TextColor = Colors.Black;
            weekCocktailPrice.HorizontalTextAlignment = TextAlignment.Center;
            prices.Children.Add(weekCocktailPrice);

            Label and = new Label();
            and.Text = LanguageManager.Instance.GetText("SUMMARY_AND");
            and.FontSize = GetRelativeFontSize(18d);
            and.TextColor = Colors.Black;
            and.HorizontalTextAlignment = TextAlignment.Center;
            prices.Children.Add(and);

            Label weekEnd = new Label();
            weekEnd.Text = LanguageManager.Instance.GetText("SUMMARY_WEEKEND");
            weekEnd.FontSize = GetRelativeFontSize(18d);
            weekEnd.TextColor = Colors.Black;
            weekEnd.HorizontalTextAlignment = TextAlignment.Center;
            prices.Children.Add(weekEnd);

            Label weekEndHh = new Label();
            weekEndHh.Text = LanguageManager.Instance.GetText("WEEKEND_HAPPY_HOURS_HOURS");
            weekEndHh.FontSize = GetRelativeFontSize(18d);
            weekEndHh.TextColor = Colors.Black;
            weekEndHh.HorizontalTextAlignment = TextAlignment.Center;
            prices.Children.Add(weekEndHh);

            Label weekEndHhPrice = new Label();
            weekEndHhPrice.Text = LanguageManager.Instance.GetText("WEEKEND_HAPPY_HOURS_HOURS_PRICE");
            weekEndHhPrice.FontSize = GetRelativeFontSize(18d);
            weekEndHhPrice.TextColor = Colors.Black;
            weekEndHhPrice.HorizontalTextAlignment = TextAlignment.Center;
            weekEndHhPrice.FontAttributes = FontAttributes.Bold;
            prices.Children.Add(weekEndHhPrice);

            Label weekEndPrice = new Label();
            weekEndPrice.Text = LanguageManager.Instance.GetText("WEEKEND_HAPPY_PRICE");
            weekEndPrice.FontSize = GetRelativeFontSize(18d);
            weekEndPrice.TextColor = Colors.Black;
            weekEndPrice.HorizontalTextAlignment = TextAlignment.Center;
            prices.Children.Add(weekEndPrice);

            views.Children.Add(prices);


            Label facebookInfo = new Label();
            facebookInfo.Text = LanguageManager.Instance.GetText("SUMMARY_FACEBOOK");
            facebookInfo.FontSize = 18;
            facebookInfo.Padding = new Thickness(0d, GetRelativeHeight(5d), 0d, 0d);
            facebookInfo.TextColor = Colors.Black;
            facebookInfo.HorizontalTextAlignment = TextAlignment.Center;
            facebookInfo.FontAttributes = FontAttributes.Bold;
            views.Children.Add(facebookInfo);

            Label adresse = new Label();
            adresse.Text = LanguageManager.Instance.GetText("BAR_ADRESSE");
            adresse.FontSize = GetRelativeFontSize(12d);
            adresse.TextColor = Colors.Black;
            adresse.HorizontalTextAlignment = TextAlignment.Center;
            adresse.Padding = new Thickness(0d, GetRelativeHeight(20d), 0d, GetRelativeHeight(40d));
            views.Children.Add(adresse);

            content = views;
        }
    }
}
