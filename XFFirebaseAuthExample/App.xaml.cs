using Autofac;
using Xamarin.Forms;
using TouristEye.ViewModels;
using Firebase.Xamarin.Auth;
using TouristEye.Views;
using TouristEye.Models;

namespace TouristEye
{
    public partial class App : Application
    {
        public IContainer Container { get; }
        public string AuthToken { get; set; }
        public UserProfile UserProfile { get; set; }
        public FirebaseAuthLink AuthLink { get; set; }

        public App(Module module)
        {
            InitializeComponent();
            Container = BuildContainer(module);
            MainPage = new Loader();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        IContainer BuildContainer(Module module)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<LoginViewModel>().AsSelf();
            builder.RegisterType<SignupPageViewModel>().AsSelf();
            //builder.RegisterType<HomeViewModel>().AsSelf();
            //builder.RegisterType<NavigationService>().AsSelf().SingleInstance();
            builder.RegisterModule(module);
            return builder.Build();
        }
    }
}
