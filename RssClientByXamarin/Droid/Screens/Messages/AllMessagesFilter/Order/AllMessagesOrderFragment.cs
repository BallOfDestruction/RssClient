﻿using System;
using Android.OS;
using Android.Views;
using Android.Widget;
using Core.Configuration.AllMessageFilter;
using Core.Extensions;
using Core.ViewModels.Messages.AllMessagesFilter;
using Droid.Resources;
using Droid.Screens.Navigation;
using JetBrains.Annotations;
using ReactiveUI;

namespace Droid.Screens.Messages.AllMessagesFilter.Order
{
    public class AllMessagesOrderFragment : BaseFragment<AllMessagesOrderFilterViewModel>, RadioGroup.IOnCheckedChangeListener
    {
        // ReSharper disable once NotNullMemberIsNotInitialized
        [NotNull] private AllMessagesOrderFragmentViewHolder _viewHolder;

        protected override int LayoutId => Resource.Layout.fragment_all_messages_order_sub;

        public override bool IsRoot => false;

        public void OnCheckedChanged(RadioGroup group, int checkedId)
        {
            Sort sort;

            switch (checkedId)
            {
                case Resource.Id.radioButton_rss_all_messages_order_newest:
                    sort = Sort.Newest;
                    break;
                case Resource.Id.radioButton_rss_all_messages_order_oldest:
                    sort = Sort.Oldest;
                    break;
                default:
                    sort = Sort.Newest;
                    break;
            }

            ViewModel.UpdateSortCommand.Execute(sort).NotNull().Subscribe();
        }

        protected override void RestoreState(Bundle saved) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState).NotNull();

            _viewHolder = new AllMessagesOrderFragmentViewHolder(view);

            _viewHolder.RootRadioGroup.SetOnCheckedChangeListener(this);

            OnActivation(disposable => ViewModel.WhenAnyValue(w => w.Sort)
                .NotNull()
                .Subscribe(UpdateSortFilter)
                .AddTo(disposable));

            return view;
        }

        private void UpdateSortFilter(Sort sort)
        {
            switch (sort)
            {
                case Sort.Oldest:
                    _viewHolder.OldestRadioButton.Checked = true;
                    break;
                case Sort.Newest:
                    _viewHolder.NewestRadioButton.Checked = true;
                    break;
            }
        }
    }
}