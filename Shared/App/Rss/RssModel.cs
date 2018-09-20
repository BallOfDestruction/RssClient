﻿using System;
using System.Collections.Generic;
using System.Linq;
using RssClient.App.Rss.Detail;
using Shared.App.Base.Database;

namespace Shared.App.Rss
{
    public class RssModel : Entity
    {
        public string Name { get; set; }
        public string Rss { get; set; }
        public DateTime CreationTime { get; set; }

        [SQLite.Ignore]
        public List<RssMessageModel> Messages { get; set; }

        public RssModel()
        {
            
        }

        public RssModel(string name, string rss, DateTime creationTime)
        {
            Name = name;
            Rss = rss;
            CreationTime = creationTime;
        }

        public void LoadMessagesFromDb(ILocalDb localDb)
        {
            Messages = localDb.GetItems<RssMessageModel>().Where(w => w.PrimaryKeyRssModel == Id).ToList();
        }
    }
}
