using Droid.Screens.Navigation;
using Shared.Infrastructure.Navigation;
using Shared.ViewModels.RssEdit;
using Shared.ViewModels.Settings;

namespace Droid.Screens.RssEdit
{
    public class RssEditWay : IWayWithParameters<RssEditViewModel, RssEditParameters>
    {
        private readonly FragmentActivity _fragmentActivity;
        private readonly RssEditParameters _parameters;

        public RssEditWay(FragmentActivity fragmentActivity, RssEditParameters parameters)
        {
            _fragmentActivity = fragmentActivity;
            _parameters = parameters;
        }

        public void Go()
        {
            var fragment = new RssEditFragment(_parameters.RssId);
            fragment.SetParameters(_parameters);
            
            _fragmentActivity.AddFragment(fragment);
        }
    }
}