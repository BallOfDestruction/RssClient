using Autofac;
using Shared.Services;

namespace Shared.Container.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<RssService>().As<IRssService>().SingleInstance();
        }
    }
}