﻿using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views.InputMethods;
using Android.Widget;
using Autofac;
using Droid.Screens.Base;
using RssClient.Repository;
using Shared;
using Shared.Database.Rss;

namespace Droid.Screens.RssEdit
{
    [Activity(Label = "@string/all_appName", Theme = "@style/AppTheme.NoActionBar")]
    public class RssEditActivity : ToolbarActivity
    {
        public const string ItemIntentId = "ItemIntentId";

        private TextInputLayout _url;
        private Button _sendButton;
        private RssModel _item;

	    private IRssRepository _rssRepository;

        protected override int ResourceView => Resource.Layout.activity_rss_edit;

        public static Intent StartActivity(Context context, string rssId)
        {
            var intent = new Intent(context, typeof(RssEditActivity));
            intent.PutExtra(ItemIntentId, rssId);

            return intent;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

	        _rssRepository = App.Container.Resolve<IRssRepository>();

			Title = GetText(Resource.String.edit_title);

            var idItem = Intent.GetStringExtra(ItemIntentId);
	        _item = _rssRepository.Find(idItem);

            if (_item == null)
                return;

            InitUrlEditText();

            InitSendButton();
        }

        private void InitSendButton()
        {
            _sendButton = FindViewById<Button>(Resource.Id.button_rssEdit_submit);
            _sendButton.Click += SendButtonOnClick;
        }

        private void InitUrlEditText()
        {
            _url = FindViewById<TextInputLayout>(Resource.Id.textInputLayout_rssEdit_link);
            _url.EditText.SetTextAndSetCursorToLast(_item.Rss);
            _url.EditText.EditorAction += (sender, args) =>
            {
                if (args.ActionId == ImeAction.Done) _sendButton.CallOnClick();
            };
        }

        private async void SendButtonOnClick(object sender, EventArgs eventArgs)
        {
            var url = _url.EditText.Text;

	        await _rssRepository.Update(_item.Id, url);

			Finish();
        }
    }
}