using System;
using System.Reactive;
using Core.Extensions;
using Core.Infrastructure.Navigation;
using Core.Infrastructure.ViewModels;
using Core.Resources;
using Core.Services.RssFeeds;
using JetBrains.Annotations;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Core.ViewModels.RssFeeds.Create
{
    public class RssFeedCreateViewModel : ViewModel
    {
        public RssFeedCreateViewModel([NotNull] IRssFeedService feedService, [NotNull] INavigator navigator)
        {
            Url = Strings.CreateRssUrlDefault;

            CreateCommand = ReactiveCommand.CreateFromTask(async token => await feedService.AddAsync(Url, token)).NotNull();
            CreateCommand.Subscribe(_ => navigator.GoBack());
        }

        [Reactive] [CanBeNull] public string Url { get; set; }

        [NotNull] public ReactiveCommand<Unit, Unit> CreateCommand { get; }
    }
}
