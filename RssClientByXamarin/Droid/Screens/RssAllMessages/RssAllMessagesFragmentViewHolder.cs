using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using JetBrains.Annotations;
using Shared.Extensions;

namespace Droid.Screens.RssAllMessages
{
    public class RssAllMessagesFragmentViewHolder
    {
        public RssAllMessagesFragmentViewHolder([NotNull] View view)
        {
            FloatingActionButton = view.FindViewById<FloatingActionButton>(Resource.Id.fab_allMessages_addRss).NotNull();
            
            RecyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView_allMessages_list).NotNull();
            RecyclerView.SetLayoutManager(new LinearLayoutManager(view.Context, LinearLayoutManager.Vertical, false));
            RecyclerView.AddItemDecoration(new DividerItemDecoration(view.Context, DividerItemDecoration.Vertical));
        }
        
        [NotNull] public RecyclerView RecyclerView { get; }
        
        [NotNull] public FloatingActionButton FloatingActionButton { get; }
    }
}
