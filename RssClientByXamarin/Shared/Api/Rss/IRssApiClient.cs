﻿#region

using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

#endregion

namespace Shared.Api.Rss
{
    public interface IRssApiClient
    {
        [NotNull]
        Task<SyndicationFeed> LoadFeedsAsync([CanBeNull] string rssUrl, CancellationToken token = default);
    }
}
