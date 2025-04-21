
namespace AppliLeCrocodile
{
    internal class FrontPage : LinkContentPage
    {
        public FrontPage() : base()
        {
            Title = "FrontPage";

            Image backgroundImage = new Image();
            backgroundImage.Source = "croco.png";
            backgroundImage.Aspect = Aspect.Fill;
            
            Grid grid = new Grid();
            grid.RowDefinitions = new RowDefinitionCollection(
                new RowDefinition { Height = GridLength.Star },
                new RowDefinition { Height = GridLength.Auto }
            );

            grid.Children.Add(backgroundImage);

            Content = grid;
        }

        private async void OnNextPageButtonClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Clicked");
            await Navigation.PushAsync(nextPage);
        }
    }
}
