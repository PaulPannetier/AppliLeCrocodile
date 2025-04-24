
namespace AppliLeCrocodile
{
    internal class FrontPage : LinkContentPage
    {
        private Grid grid;

        public FrontPage() : base()
        {
            Title = "FrontPage";
            
            grid = new Grid();
            grid.RowDefinitions = new RowDefinitionCollection(
                new RowDefinition { Height = GridLength.Star },
                new RowDefinition { Height = GridLength.Auto }
            );


            Image backgroundImage = new Image();
            backgroundImage.Source = "croco.png";
            backgroundImage.Aspect = Aspect.Fill;

            grid.Children.Add(backgroundImage);

            Content = grid;
        }

        public override void Start()
        {
            base.Start();
            base.InitializeSwipe(grid);
        }

        protected override LinkContentPage Clone()
        {
            return new FrontPage();
        }
    }
}
