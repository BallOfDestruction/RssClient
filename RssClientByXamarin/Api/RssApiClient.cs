﻿using System;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using Analytics;

namespace Api
{
    public class RssApiClient : HttpClient, IRssApiClient
    {
        private readonly ILog _log;
		public RssApiClient(ILog log) : base()
        {
            _log = log;
        }

		public async Task<SyndicationFeed> Update(string rssUrl)
		{
			try
			{
				var response = await GetAsync(rssUrl);

				var stream = response.Content.ReadAsStreamAsync();
				var xmlReader = XmlReader.Create(stream.Result);
				var feed = SyndicationFeed.Load(xmlReader);

				return feed;
			}
			catch (Exception e)
			{
				_log.TrackLog(LogLevel.Warn, "UpdateFeed", "При попытке обновить данные", e);
				return null;
			}
		}
	}
}