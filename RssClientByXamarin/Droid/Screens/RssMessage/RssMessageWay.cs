using System;
using Droid.Repositories.Configuration;
using Droid.Screens.Navigation;
using Shared.Configuration.Settings;
using Shared.Infrastructure.Navigation;
using Shared.ViewModels.RssMessage;
using Xamarin.Essentials;

namespace Droid.Screens.RssMessage
{
    public class RssMessageWay : IWayWithParameters<RssMessageViewModel, RssMessageParameters>
    {
        private readonly IFragmentManager _activity;
        private readonly IConfigurationRepository _configurationRepository;
        private readonly RssMessageParameters _parameters;

        public RssMessageWay(
            IFragmentManager activity,
            IConfigurationRepository configurationRepository,
            RssMessageParameters parameters)
        {
            _activity = activity;
            _configurationRepository = configurationRepository;
            _parameters = parameters;
        }

        public void Go()
        {
            var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();

            switch (appConfiguration.MessagesViewer)
            {
                case MessagesViewer.Browser:
                    Browser.OpenAsync(_parameters.RssMessageModel.Url);
                    break;
                case MessagesViewer.App:
                    var fragment = new RssMessageFragment(_parameters.RssMessageModel.Id);
                    fragment.SetParameters(_parameters);

                    _activity.AddFragment(fragment);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
