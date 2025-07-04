
namespace AppliLeCrocodile
{
    internal class FrontPage : SwipableContent
    {
        public FrontPage(MainPage mainPage) : base(mainPage)
        {
            title = "FrontPage";

            Grid grid = new Grid();
            grid.RowDefinitions = new RowDefinitionCollection(
                new RowDefinition { Height = GridLength.Star },
                new RowDefinition { Height = GridLength.Auto }
            );


            Image backgroundImage = new Image();
            backgroundImage.Source = "croco.png";
            backgroundImage.Aspect = Aspect.Fill;
            backgroundImage.HorizontalOptions = LayoutOptions.Center;
            backgroundImage.VerticalOptions = LayoutOptions.Start;

            grid.Children.Add(backgroundImage);

            content = grid;
        }
    }
}
