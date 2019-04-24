﻿using System;
using System.Linq;
using SQLite;

namespace Core.Database.Rss
{
    public class RssMessageModel : IHaveId
    {
        [PrimaryKey] public string Id { get; set; } = Guid.NewGuid().ToString();
        
        public string SyndicationId { get; set; }

        public string Title { get; set; }

        public DateTimeOffset CreationDate { get; set; }

        public string Text { get; set; }

        public string Url { get; set; }

        public string ImageUrl { get; set; }

        public bool IsRead { get; set; }

        public bool IsFavorite { get; set; }
    }
}
