namespace Dezhban.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);


#if WINDOWS

        window.Width = 800;
        window.Height = 500;
        window.MinimumWidth = 800;
        window.MinimumHeight = 400;


            var display = DeviceDisplay.Current.MainDisplayInfo;
            var screenWidth = display.Width / display.Density;
            var screenHeight = display.Height / display.Density;

            window.X = (screenWidth - window.Width) / 2;
            window.Y = (screenHeight - window.Height) / 2;

#endif

            return window;
        }
    }
}
