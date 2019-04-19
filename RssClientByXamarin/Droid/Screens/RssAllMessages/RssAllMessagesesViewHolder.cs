﻿#region

using Android.Views;
using Android.Widget;
using Droid.NativeExtension;
using Droid.Screens.Base;
using Droid.Screens.RssMessagesList;
using FFImageLoading;
using FFImageLoading.Views;
using Shared.Infrastructure.Locale;
using Shared.Repositories.RssMessage;

#endregion

namespace Droid.Screens.RssAllMessages
{
    public class RssAllMessagesesViewHolder : BaseRssMessagesViewHolder, IShowAndLoadImage
    {
        public RssAllMessagesesViewHolder(View itemView, bool isShowAndLoadImages) : base(itemView)
        {
            IsShowAndLoadImages = isShowAndLoadImages;

            Title = itemView.FindViewById<TextView>(Resource.Id.textView_allMessagesItem_title);
            Text = itemView.FindViewById<TextView>(Resource.Id.textView_allMessagesItem_text);
            CreationDate = itemView.FindViewById<TextView>(Resource.Id.textView_allMessagesItem_date);
            Canal = itemView.FindViewById<TextView>(Resource.Id.textView_allMessagesItem_canal);
            ClickView = itemView.FindViewById<LinearLayout>(Resource.Id.linearLayout_allMessagesItem_content);
            ImageView = itemView.FindViewById<ImageViewAsync>(Resource.Id.imageView_allMessagesItem_image);
            Background = itemView.FindViewById<LinearLayout>(Resource.Id.linearLayout_allMessagesItem_background);
            RatingBar = itemView.FindViewById<RatingBar>(Resource.Id.ratingBar_allMessagesItem_favorite);

            ImageView.Visibility = IsShowAndLoadImages.ToVisibility();
        }

        public TextView Title { get; }
        public TextView Text { get; }
        public TextView CreationDate { get; }
        public TextView Canal { get; }
        public ImageViewAsync ImageView { get; }
        public LinearLayout ClickView { get; }
        public LinearLayout Background { get; }
        public RatingBar RatingBar { get; }
        public bool IsShowAndLoadImages { get; }

        public override void BindData(RssMessageDomainModel item)
        {
            Item = item;

            Title.Text = item.Title;
            Text.SetTextAsHtml(item.Text);
            CreationDate.Text = item.CreationDate.ToShortDateLocaleString();
            Canal.Text = item.RssParent.Name;
            Background.SetBackgroundColor(item.IsRead ? BackgroundItemSelectColor : BackgroundItemColor);
            RatingBar.Rating = item.IsFavorite ? 1 : 0;
            RatingBar.Visibility = item.IsFavorite.ToVisibility();

            if (IsShowAndLoadImages)
            {
                ImageView.Visibility = (!string.IsNullOrEmpty(item.Url)).ToVisibility();
                ImageService.Instance.LoadUrl(item.ImageUrl).Into(ImageView);
            }
        }
    }
}
