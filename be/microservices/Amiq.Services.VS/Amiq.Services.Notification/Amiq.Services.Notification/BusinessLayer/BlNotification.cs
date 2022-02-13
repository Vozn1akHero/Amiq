using Amiq.Services.Common.Contracts;
using Amiq.Services.Common.DbOperation;
using Amiq.Services.Notification.BusinessLayer.Utils;
using Amiq.Services.Notification.Contracts.Notification;
using Amiq.Services.Notification.DataAccessLayer;

namespace Amiq.Services.Notification.BusinessLayer
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
