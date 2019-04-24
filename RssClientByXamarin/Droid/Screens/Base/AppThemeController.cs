using System;
using Android.App;
using Autofac;
using Core;
using Core.Configuration.Settings;
using Core.Repositories.Configurations;

namespace Droid.Screens.Base
{
    public class AppThemeController
    {
        private readonly IConfigurationRepository _configurationRepository;

        public AppThemeController() { _configurationRepository = App.Container.Resolve<IConfigurationRepository>(); }

        public void SetTheme(Activity activity)
        {
            var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();
            var appTheme = appConfiguration.AppTheme;

            int themeId;

            switch (appTheme)
            {
                case AppTheme.Light:
                    themeId = Resource.Style.AppTheme_Light_NoActionBar;
                    break;
                case AppTheme.Dark:
                    themeId = Resource.Style.AppTheme_Dark_NoActionBar;
                    break;
                case AppTheme.Default:
                    themeId = Resource.Style.AppTheme_Default_NoActionBar;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            activity.SetTheme(themeId);
        }
    }
}
