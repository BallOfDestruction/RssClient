#region

using System;
using System.Reactive;
using Droid.EmbeddedResourse;
using JetBrains.Annotations;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shared.Extensions;
using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;
using Shared.Services.Rss;

#endregion

namespace Shared.ViewModels.RssCreate
{
    public class RssCreateViewModel : ViewModel
    {
        [NotNull] private readonly INavigator _navigator;
        [NotNull] private readonly IRssService _service;

        public RssCreateViewModel([NotNull] IRssService service, [NotNull] INavigator navigator)
        {
            _service = service;
            _navigator = navigator;

            Url = Strings.CreateRssUrlDefault;

            CreateCommand = ReactiveCommand.CreateFromTask(async token => await _service.AddAsync(Url, token)).NotNull();
            CreateCommand.Subscribe(_ => _navigator.GoBack());
        }

        [Reactive] public string Url { get; set; }

        [NotNull] public ReactiveCommand<Unit, Unit> CreateCommand { get; }
    }
}
