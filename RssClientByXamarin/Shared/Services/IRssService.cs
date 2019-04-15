using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;
using Shared.Repository.Rss;

namespace Shared.Services
{
    public interface IRssService
    {
        Task Create(string url, CancellationToken cancellationToken = default);
        
        Task Update(string id, string rss, CancellationToken token = default);
        
        Task<RssData> Find(string id, CancellationToken token = default);
        
        Task Remove(string id, CancellationToken token = default);
        
        Task<IEnumerable<RssData>> GetList(CancellationToken token = default);
        
        Task Update(string rssId, SyndicationFeed feed, CancellationToken token = default);
        
        Task UpdatePosition(string id, int position, CancellationToken token = default);
        
        Task ReadAllMessages(string id, CancellationToken token = default);
    }
}