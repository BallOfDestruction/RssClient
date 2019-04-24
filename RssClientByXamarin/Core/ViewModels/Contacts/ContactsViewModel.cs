using System.Reactive;
using System.Threading.Tasks;
using Core.Extensions;
using Core.Infrastructure.ViewModels;
using Core.Resources;
using JetBrains.Annotations;
using ReactiveUI;
using Xamarin.Essentials;

namespace Core.ViewModels.Contacts
{
    public class ContactsViewModel : ViewModel
    {
        public ContactsViewModel()
        {
            GoTelegramCommand = ReactiveCommand.CreateFromTask(async () => await OpenLink(Strings.ContactsTelegramLink)).NotNull();
            GoMailCommand = ReactiveCommand.CreateFromTask(async () => await OpenLink(Strings.ContactsMailLink)).NotNull();
            GoLinkedinCommand = ReactiveCommand.CreateFromTask(async () => await OpenLink(Strings.ContactsLinkedInLink)).NotNull();
            GoDiscordCommand = ReactiveCommand.CreateFromTask(async () => await CopyToClipboard(Strings.ContactsDiscordLink)).NotNull();
        }

        [NotNull] public ReactiveCommand<Unit, Unit> GoTelegramCommand { get; }
        [NotNull] public ReactiveCommand<Unit, Unit> GoMailCommand { get; }
        [NotNull] public ReactiveCommand<Unit, Unit> GoLinkedinCommand { get; }
        [NotNull] public ReactiveCommand<Unit, Unit> GoDiscordCommand { get; }

        [NotNull]
        private async Task CopyToClipboard([CanBeNull] string text)
        {
            await Clipboard.SetTextAsync(text).NotNull();
            // TODO го клиборт
            //            Context.ToastClipboard(text);
        }

        [NotNull]
        private async Task OpenLink([CanBeNull] string text)
        {
            if (await Launcher.CanOpenAsync(text).NotNull())
                await Launcher.OpenAsync(text).NotNull();
        }
    }
}
