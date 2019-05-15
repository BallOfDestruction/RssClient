using System;
using Android.Content;
using Android.Support.V4.View;
using Android.Views;
using Core.Services.RssMessages;
using DynamicData;
using ReactiveUI.AndroidSupport;

namespace Droid.Screens.Messages.Book
{
    public class BookViewPagerAdapterHolder
    {
        private readonly Context _context;
        
        public ReactivePagerAdapter<RssMessageServiceModel> Adapter { get; }
            
        public BookViewPagerAdapterHolder(Context context, IObservable<IChangeSet<RssMessageServiceModel>> changeSet)
        {
            _context = context;
            Adapter = new ReactivePagerAdapter<RssMessageServiceModel>(changeSet, ViewCreator, ViewInitializer);
        }

        private void ViewInitializer(RssMessageServiceModel model, View view)
        {
            var viewHolder = new BookMessageViewHolder(view);
            viewHolder.Bind(model);
            viewHolder.SetCounting(Adapter.GetItemPosition(view), Adapter.Count);
        }

        private View ViewCreator(RssMessageServiceModel message, ViewGroup parent)
        {
            var view = LayoutInflater.From(_context).Inflate(Resource.Layout.view_book_message, parent, false);

            return view;
        }
    }
}