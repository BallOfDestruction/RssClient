﻿using System;
using System.Reactive;
using System.Reactive.Linq;
using Android.OS;
using Android.Support.V7.Widget.Helper;
using Android.Views;
using Android.Widget;
using Core.Extensions;
using Core.Services.RssFeeds;
using Core.ViewModels.RssFeeds.List;
using Droid.Infrastructure.Collections;
using Droid.NativeExtension;
using Droid.NativeExtension.Events;
using Droid.Screens.Base.SwipeRecyclerView;
using Droid.Screens.Navigation;
using ReactiveUI;
using PopupMenu = Android.Support.V7.Widget.PopupMenu;

namespace Droid.Screens.RssFeeds.List
{
    public class RssFeedListFragment : BaseFragment<RssFeedListViewModel>
    {
        private RssFeedListFragmentViewHolder _viewHolder;

        protected override int LayoutId => Resource.Layout.fragment_rss_list;
        public override bool IsRoot => true;

        protected override void RestoreState(Bundle saved) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            Title = Activity.GetText(Resource.String.rssList_title);

            HasOptionsMenu = true;

            _viewHolder = new RssFeedListFragmentViewHolder(view);

            var adapter = new RssFeedListAdapter(Activity, ViewModel.AppConfiguration);
            _viewHolder.RecyclerView.SetAdapter(adapter);
            adapter.NotifyDataSetChanged();

            var callback = new SwipeTouchHelperCallback(adapter);
            var touchHelper = new ItemTouchHelper(callback);
            touchHelper.AttachToRecyclerView(_viewHolder.RecyclerView);

            var adapterUpdater = new AdapterUpdater<RssFeedServiceModel>(_viewHolder.RecyclerView, adapter, ViewModel.ListViewModel.SourceList);

            OnActivation(disposable =>
            {
                _viewHolder.FloatingActionButton.Events()
                    .Click
                    .SelectUnit()
                    .InvokeCommand(ViewModel.OpenCreateScreenCommand)
                    .AddTo(disposable);

                ViewModel.WhenAnyValue(w => w.ListViewModel.IsEmpty)
                    .Subscribe(w => _viewHolder.EmptyTextView.Visibility = w.ToVisibility())
                    .AddTo(disposable);

                adapter.GetClickAction()
                    .InvokeCommand(ViewModel.RssFeedItemViewModel.OpenMessagesListCommand)
                    .AddTo(disposable);

                adapter.GetLongClickAction()
                    .Subscribe(model => ItemLongClick(model.EventArgs, model.Sender))
                    .AddTo(disposable);

                adapter.GetRssItemDismissEvent()
                    .InvokeCommand(ViewModel.RssFeedItemViewModel.DeleteItemCommand)
                    .AddTo(disposable);

                ViewModel.ListViewModel.ConnectChanges
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Subscribe(w => adapterUpdater.Update(w))
                    .AddTo(disposable);

                ViewModel.RssFeedsUpdaterViewModel.UpdateCommand.IsExecuting
                    .Subscribe(w => _viewHolder.TopProgressBar.Visibility = w.ToVisibility())
                    .AddTo(disposable);

                ViewModel.RssFeedsUpdaterViewModel.UpdateCommand
                    .Subscribe(w => Toast.MakeText(Activity, "123", ToastLength.Short).Show())
                    .AddTo(disposable);
                
                ViewModel.GetListCommand.Execute().Subscribe().AddTo(disposable);
                
                ViewModel.RssFeedsUpdaterViewModel.UpdateCommand.ExecuteIfCan(Unit.Default).AddTo(disposable);
            });

            return view;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater) { inflater.Inflate(Resource.Menu.menu_rssList, menu); }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menuItem_rssList_change:
                    ViewModel.OpenAllMessagesScreenCommand.Execute().Subscribe();
                    break;

                case Resource.Id.menuItem_rssList_editMode:
                    ViewModel.OpenListEditScreenCommand.Execute().Subscribe();
                    break;
                
                case Resource.Id.menuItem_rssList_refresh:
                    ViewModel.RssFeedsUpdaterViewModel.UpdateCommand.ExecuteIfCan(Unit.Default);
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void ItemLongClick(RssFeedServiceModel holderItem, object sender)
        {
            var menu = new PopupMenu(Activity, sender as View, (int) GravityFlags.Right);
            menu.MenuItemClick += (o, eventArgs) => MenuClick(holderItem, eventArgs);
            menu.Inflate(Resource.Menu.contextMenu_rssList);
            menu.Show();
        }

        private void MenuClick(RssFeedServiceModel holderItem, PopupMenu.MenuItemClickEventArgs eventArgs)
        {
            switch (eventArgs.Item.ItemId)
            {
                case Resource.Id.menuItem_rssList_contextEdit:
                    ViewModel.RssFeedItemViewModel.OpenEditItemCommand.Execute(holderItem).Subscribe();
                    break;
                case Resource.Id.menuItem_rssList_contextRemove:
                    ViewModel.RssFeedItemViewModel.ShowDeleteDialogCommand.Execute(holderItem).Subscribe();
                    break;
                case Resource.Id.menuItem_rssList_contextShare:
                    ViewModel.RssFeedItemViewModel.ShareCommand.Execute(holderItem).Subscribe();
                    break;
                case Resource.Id.menuItem_rssList_contextReadAllMessages:
                    ViewModel.RssFeedItemViewModel.ReadAllMessagesCommand.Execute(holderItem).Subscribe();
                    break;
            }
        }
    }
}
