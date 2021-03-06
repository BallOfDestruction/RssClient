﻿using Core.Infrastructure.ViewModels;

namespace Core.Infrastructure.Navigation
{
    public interface IWayWithParameters<TViewModel, TParameter> : IWay<TViewModel>
        where TViewModel : ViewModelWithParameter<TParameter>
        where TParameter : ViewModelParameters
    {
    }

    // ReSharper disable once UnusedTypeParameter
    public interface IWay<TViewModel> : IWay
        where TViewModel : ViewModel
    {
    }

    public interface IWay
    {
        void Go();
    }
}
