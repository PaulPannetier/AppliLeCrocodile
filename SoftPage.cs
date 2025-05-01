
namespace AppliLeCrocodile
{
    internal class SoftPage : SwipableContent
    {
        public SoftPage(MainPage mainPage) : base(mainPage)
        {
            title = "SoftPage";

            VerticalStackLayout views = new VerticalStackLayout();
            views.HorizontalOptions = LayoutOptions.Fill;
            views.VerticalOptions = LayoutOptions.Fill;

            Label softLabel = new Label();
            //softLabel.Text = LanguageManager.Instance.GetText("");
        }
    }
}
