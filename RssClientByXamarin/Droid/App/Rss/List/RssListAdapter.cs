﻿using System.Linq;
using Android.App;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Com.Bumptech.Glide;
using Database.Rss;
using Newtonsoft.Json;
using RssClient.App.Rss.Detail;
using RssClient.App.Rss.Edit;
using Shared.App.Rss;

namespace RssClient.App.Rss.List
{
	public class RssListAdapter : RecyclerView.Adapter
    {
        private const string DeletePositiveTitle = "Yes";
        private const string DeleteNegativeTitle = "No";
        private const string DeleteTitle = "Ara you sure?";

        private readonly Activity _activity;
	    private readonly RssRepository _rssRepository;

        public RssListAdapter(IQueryable<RssModel> items, Activity activity)
        {
			_rssRepository = RssRepository.Instance;
            _activity = activity;
	        Items = items;
        }

        public override int ItemCount => Items.Count();
        public IQueryable<RssModel> Items { get; }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
	        var item = Items.ElementAt(position);

            if (holder is RssListViewHolder rssListViewHolder)
            {
                rssListViewHolder.TitleTextView.Text = item.Name;
                rssListViewHolder.SubtitleTextView.Text = item.UpdateTime == null ? "Не обновлено" : $"Обновлено: {item.UpdateTime.Value:g}";
                rssListViewHolder.Item = item;
                rssListViewHolder.CountTextView.Text = item.CountMessages.ToString();
                Glide.With(_activity).Load(item.UrlPreviewImage).Into(rssListViewHolder.IconView);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.rss_list_item, parent, false);
            var holder = new RssListViewHolder(view);

            holder.ClickView.Click += (sender, args) => { OpenDetailActivity(holder.Item); };
            holder.ClickView.LongClick += (sender, args) => { ItemLongClick(holder.Item, sender); };

            return holder;
        }


        private void ItemLongClick(RssModel holderItem, object sender)
        {
            var menu = new PopupMenu(_activity, sender as View, (int) GravityFlags.Right);
            menu.MenuItemClick += (o, eventArgs) => MenuClick(holderItem, eventArgs);
            menu.Inflate(Resource.Menu.rss_list_item);
            menu.Show();
        }

        private void MenuClick(RssModel holderItem, PopupMenu.MenuItemClickEventArgs eventArgs)
        {
            switch (eventArgs.Item.ItemId)
            {
                case Resource.Id.rss_item_edit_action:
                    EditItem(holderItem);
                    break;
                case Resource.Id.rss_item_remove_action:
                    DeleteItem(holderItem);
                    break;
            }
        }

        private void EditItem(RssModel holderItem)
        {
            var intent = new Intent(_activity, typeof(RssEditActivity));
            intent.PutExtra(RssEditActivity.ItemIntentId, holderItem.Id);
            _activity.StartActivityForResult(intent, RssListActivity.EditRequestCode);
        }

        private void DeleteItem(RssModel holderItem)
		{
			var builder = new AlertDialog.Builder(_activity);
			builder.SetPositiveButton(DeletePositiveTitle, (sender, args) =>
			{
				_rssRepository.Remove(holderItem);
			});
			builder.SetNegativeButton(DeleteNegativeTitle, (sender, args) => { });
			builder.SetTitle(DeleteTitle);
			builder.Show();
		}

        private void OpenDetailActivity(RssModel holderItem)
        {
            var intent = new Intent(_activity, typeof(RssDetailActivity));
            intent.PutExtra(RssDetailActivity.ItemIntentId, holderItem.Id);
            _activity.StartActivity(intent);
        }
    }
}