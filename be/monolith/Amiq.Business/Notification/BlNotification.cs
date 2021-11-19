using Amiq.Business.Utils;
using Amiq.Contracts;
using Amiq.Contracts.Notification;
using Amiq.Contracts.Utils;
using Amiq.DataAccess.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Business.Notification
{
    public class BlNotification : BusinessLayerBase
    {
        private DaNotification daNotification = new DaNotification();

        public async Task<DtoListResponseOf<DtoNotification>> GetNotificationsAsync(int userId, DtoPaginatedRequest dtoPaginatedRequest)
        {
            return await daNotification.GetNotificationsAsync(userId, dtoPaginatedRequest);
        }

        public async Task CreateNotificationsForUserAsync()
        {

        }
    }
}
