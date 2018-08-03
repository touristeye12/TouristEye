using Autofac;

namespace TouristEye.iOS
{
    class IOSModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<FirebaseAuthenticator>().As<IFirebaseAuthenticator>().SingleInstance();
        }
    }
}