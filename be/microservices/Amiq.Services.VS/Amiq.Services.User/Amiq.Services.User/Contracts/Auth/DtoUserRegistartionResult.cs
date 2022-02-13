using Amiq.Services.Common.DbOperation;
using Amiq.Services.User.Contracts.User;

namespace Amiq.Services.User.Contracts.Auth
{
    public class DtoUserRegistartionResult : DbOperationResult
    {
        public DtoBasicUserInfo BasicUserInfo { get; set; }
    }
}
