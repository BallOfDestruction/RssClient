using Shared.Services.Navigator;

namespace Shared.ViewModels
{
    public class ContactsViewModel : ViewModel
    {
        public abstract class Way : Way<ContactsViewModel, Way.WayData>
        {
        
            public Way()
            {
                
            }
            
            public class WayData
            {
            
            }
        }
    }
}