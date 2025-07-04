
namespace AppliLeCrocodile
{
    internal class TestPage : SwipableContent
    {
        public TestPage(MainPage mainPage) : base(mainPage)
        {
            title = "TestPage";

            VerticalStackLayout views = new VerticalStackLayout();
            views.Padding = 0;
            views.Spacing = 0;
            views.VerticalOptions = LayoutOptions.Fill;
            views.HorizontalOptions = LayoutOptions.Fill;
            views.BackgroundColor = Colors.AntiqueWhite;

            Stepper stepper = new Stepper();
            stepper.Minimum = 0;
            stepper.Maximum = 100;
            views.Children.Add(stepper);

            Label label = new Label();
            label.Text = stepper.Value.ToString();
            stepper.PropertyChanged += (s, e) => label.Text = stepper.Value.ToString();
            views.Children.Add(label);

            ProgressBar progressBar = new ProgressBar();
            progressBar.Progress = 0.5f;
            views.Children.Add(progressBar);

            Slider slider = new Slider();
            slider.Minimum = 0f;
            slider.Maximum = 1f;
            slider.Value = 0.5f;

            slider.PropertyChanged += (s, e) => progressBar.Progress = slider.Value;
            views.Children.Add(slider);

            ScrollView scrollView = new ScrollView();
            HorizontalStackLayout horizontalStackLayout = new HorizontalStackLayout();
            Label label2 = new Label();
            label2.Text = "Salut ";
            horizontalStackLayout.Children.Add(label2);
            label2 = new Label();
            label2.Text = "les ";
            horizontalStackLayout.Children.Add(label2);
            label2 = new Label();
            label2.Text = "petits ";
            horizontalStackLayout.Children.Add(label2);
            label2 = new Label();
            label2.Text = "monstres.";
            horizontalStackLayout.Children.Add(label2);
            scrollView.Content = horizontalStackLayout;
            views.Children.Add(scrollView);

            Picker picker = new Picker();
            picker.Title = "Choose a fruits";
            picker.ItemsSource = new List<string> { "Apple", "Banana", "Pear" };
            views.Children.Add(picker);

            Switch @switch = new Switch();
            @switch.IsToggled = true;
            views.Children.Add(@switch);

            CheckBox checkBox = new CheckBox();
            checkBox.IsChecked = false;
            views.Children.Add(checkBox);

            Editor editor = new Editor();
            editor.AutoSize = EditorAutoSizeOption.TextChanges;
            editor.Placeholder = "Write here...";
            views.Children.Add(editor);

            Entry entry = new Entry();
            entry.Placeholder = "You're name ?";
            entry.Keyboard = Keyboard.Text;
            views.Children.Add(entry);

            content = views;
        }
    }
}
