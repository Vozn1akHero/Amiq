using Amiq.BusinessLayer.Utils;
using Amiq.Common.DbOperation;
using Amiq.Contracts.Notification;
using Amiq.Contracts.Utils;
using Amiq.DataAccessLayer.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.BusinessLayer.Notification
{
    public class BlNotification : BusinessLayerBase
    {
        private DaoNotification daNotification = new DaoNotification();

        public async Task<DtoListResponseOf<DtoNotification>> GetNotificationsAsync(int userId, DtoPaginatedRequest dtoPaginatedRequest)
        {
            return await daNotification.GetNotificationsAsync(userId, dtoPaginatedRequest);
        }

        public async Task<DtoNotReadNotificationsExistResult> AnyNotReadExistAsync(int userId) => await daNotification.AnyNotReadExistAsync(userId);

        public async Task<DbOperationResult> SetAllReadAsync(int userId) => await daNotification.SetAllReadAsync(userId);
    }
}
