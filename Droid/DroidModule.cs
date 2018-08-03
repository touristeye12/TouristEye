using Autofac;

namespace TouristEye.Droid
{
    //using DVert.Droid.Service;
    //using DVert.Services;
    
    public class DroidModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<FirebaseAuthenticator>().As<IFirebaseAuthenticator>().SingleInstance();
        }
    }

}