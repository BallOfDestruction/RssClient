using Shared.Services.Navigator;

namespace Shared.ViewModels
{
    public class RssEditViewModel : ViewModel
    {
        public abstract class Way : DataWay<RssEditViewModel, Way.WayData>
        {
            public class WayData
            {
                public string RssId { get; }

                public WayData(string rssId)
                {
                    RssId = rssId;
                }
            }
        }
    }
}