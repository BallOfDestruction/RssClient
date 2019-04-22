﻿using Droid.Screens.Navigation;
using Shared.Infrastructure.Navigation;
using Shared.ViewModels.About;

namespace Droid.Screens.About
{
    public class AboutWay : IWay<AboutViewModel>
    {
        private readonly IFragmentManager _fragmentActivity;

        public AboutWay(IFragmentManager fragmentActivity) { _fragmentActivity = fragmentActivity; }

        public void Go()
        {
            var fragment = new AboutFragment();

            _fragmentActivity.AddFragment(fragment, CacheState.Replace);
        }
    }
}
