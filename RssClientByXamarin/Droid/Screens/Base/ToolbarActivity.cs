﻿using Android.OS;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Util;
using Autofac;
using Shared;
using Shared.Analytics.Rss;

namespace Droid.Screens.Base
{
    public abstract class ToolbarActivity : AppThemeActivity
    {
        protected abstract int ResourceView { get; }
        protected Toolbar Toolbar { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(ResourceView);

            Toolbar = FindViewById<Toolbar>(Resource.Id.toolbar_toolbarAll_toolbar);
            SetSupportActionBar(Toolbar);

            // TODO вот тут стиль применить
            var navigationColor = new TypedValue();
            this.Theme.ResolveAttribute(Resource.Attribute.navigation_color, navigationColor, true);
            Toolbar.SetTitleTextColor(navigationColor.Data);
            
            App.Container.Resolve<ScreenLog>().TrackScreenOpen(GetType());
        }

        public override bool OnSupportNavigateUp()
        {
            Finish();
            return true;
        }
    }
}