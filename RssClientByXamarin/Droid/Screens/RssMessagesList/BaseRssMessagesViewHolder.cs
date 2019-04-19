#region

using Android.Graphics;
using Android.Views;
using Droid.Screens.Base.Adapters;
using Droid.Screens.Base.SwipeButtonRecyclerView;
using Shared.Repositories.RssMessage;

#endregion

namespace Droid.Screens.RssMessagesList
{
    public abstract class BaseRssMessagesViewHolder : SwipeButtonViewHolder, IDataBind<RssMessageDomainModel>
    {
        protected Color BackgroundItemColor = new Color(0, 0, 0, 0);
        protected Color BackgroundItemSelectColor = new Color(0, 0, 0, 95);

        protected BaseRssMessagesViewHolder(View itemView) : base(itemView) { }

        public override bool IsLeftButton => true;
        public override bool IsRightButton => true;
        public override string LeftButtonText => "Read";
        public override string RightButtonText => "Favorite";

        public RssMessageDomainModel Item { get; set; }

        public abstract void BindData(RssMessageDomainModel item);
    }
}
