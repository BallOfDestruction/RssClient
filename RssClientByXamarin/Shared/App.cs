﻿using Analytics;
using Analytics.Rss;
using Api;
using Autofac;
using Database;
using Repository;

namespace Shared
{
    public class App
    {
        public static ILifetimeScope Container { get; private set; }

        public static IContainer LifetimeScope { get; private set; }


        public static void Build(ContainerBuilder builder)
        {
            builder.RegisterType<RealmDatabase>().AsSelf().SingleInstance();
            builder.RegisterType<RssRepository>().As<IRssRepository>().SingleInstance();
            builder.RegisterType<RssMessagesRepository>().As<IRssMessagesRepository>().SingleInstance();

            builder.RegisterType<Log>().As<ILog>().SingleInstance();
            builder.RegisterType<RssLog>().AsSelf().SingleInstance();
            builder.RegisterType<RssMessageLog>().AsSelf().SingleInstance();
            builder.RegisterType<ScreenLog>().AsSelf().SingleInstance();

            builder.RegisterType<RssApiClient>().As<IRssApiClient>().SingleInstance();

            LifetimeScope = builder.Build();

            Container = LifetimeScope.BeginLifetimeScope();
        }
    }
}