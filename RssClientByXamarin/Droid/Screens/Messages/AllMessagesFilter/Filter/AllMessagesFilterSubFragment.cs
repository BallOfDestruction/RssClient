﻿using System;
using System.Reactive.Linq;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Core.Configuration.AllMessageFilter;
using Core.Extensions;
using Core.ViewModels.Messages.AllMessagesFilter;
using Droid.Screens.Navigation;
using JetBrains.Annotations;
using ReactiveUI;

namespace Droid.Screens.Messages.AllMessagesFilter.Filter
{
    public class AllMessagesFilterSubFragment : BaseFragment<AllMessagesFilterFilterViewModel>
    {
        [NotNull] private AllMessagesFilterSubFragmentViewHolder _viewHolder;

        // ReSharper disable once EmptyConstructor
        // ReSharper disable once NotNullMemberIsNotInitialized
        public AllMessagesFilterSubFragment() { }

        protected override int LayoutId => Resource.Layout.fragment_all_messages_filter_sub;

        public override bool IsRoot => false;

        protected override void RestoreState(Bundle saved) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            _viewHolder = new AllMessagesFilterSubFragmentViewHolder(view.NotNull());

            OnActivation(disposable =>
            {
                ViewModel.WhenAnyValue(model => model.MessageFilterType)
                    .NotNull()
                    .Subscribe(SetFilterType)
                    .AddTo(disposable);

                this.Bind(ViewModel, model => model.FromDateText, fragment => fragment._viewHolder.FromButton.Text)
                    .AddTo(disposable);

                this.Bind(ViewModel, model => model.ToDateText, fragment => fragment._viewHolder.ToButton.Text)
                    .AddTo(disposable);

                _viewHolder.FromButton.Events()
                    ?.Click?
                    .Subscribe(w => OpenFromDatePicker())
                    .AddTo(disposable);

                _viewHolder.ToButton.Events()
                    ?.Click?
                    .Subscribe(w => OpenToDatePicker())
                    .AddTo(disposable);
                
                _viewHolder.RootRadioGroup.Events()
                    .NotNull()
                    .CheckedChange
                    .NotNull()
                    .Select(w => w.NotNull().CheckedId)
                    .Select(ConvertToType)
                    .InvokeCommand(ViewModel.SetMessageFilterTypeCommand)
                    .AddTo(disposable);
            });

            return view;
        }

        private MessageFilterType ConvertToType(int id)
        {
            if (id == _viewHolder.AllRadioButton.Id) return MessageFilterType.None;
            
            if (id == _viewHolder.FavoriteRadioButton.Id) return MessageFilterType.Favorite;
            
            if (id == _viewHolder.ReadRadioButton.Id) return MessageFilterType.Read;
            
            return id == _viewHolder.UnreadRadioButton.Id ? MessageFilterType.Unread : MessageFilterType.None;
        }
        
        private void SetFilterType(MessageFilterType type)
        {
            switch (type)
            {
                default:
                    _viewHolder.AllRadioButton.Checked = true;
                    break;
                case MessageFilterType.Favorite:
                    _viewHolder.FavoriteRadioButton.Checked = true;
                    break;
                case MessageFilterType.Read:
                    _viewHolder.ReadRadioButton.Checked = true;
                    break;
                case MessageFilterType.Unread:
                    _viewHolder.UnreadRadioButton.Checked = true;
                    break;
            }
        }

        private void OpenFromDatePicker()
        {
            var fromDate = ViewModel.FromDate;
            var picker = new DatePickerDialog(Context, SetFromDate, fromDate.Year, fromDate.Month - 1, fromDate.Day);
            picker.Show();
        }

        private void OpenToDatePicker()
        {
            var defaultDate = ViewModel.ToDate;
            var picker = new DatePickerDialog(Context, SetToDate, defaultDate.Year, defaultDate.Month - 1, defaultDate.Day);
            picker.Show();
        }

        private void SetFromDate(object sender, [NotNull] DatePickerDialog.DateSetEventArgs e)
        {
            ViewModel.SetFromDateTypeCommand.ExecuteIfCan(e.Date);
        }

        private void SetToDate(object sender, [NotNull] DatePickerDialog.DateSetEventArgs e)
        {
            ViewModel.SetToDateTypeCommand.ExecuteIfCan(e.Date);
        }
    }
}
