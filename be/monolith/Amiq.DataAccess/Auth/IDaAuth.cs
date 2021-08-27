using Amiq.Contracts.Auth;
using Amiq.Contracts.User;

namespace Amiq.DataAccess.Auth
{
    public interface IDaAuth
    {
        DtoUserRegistartionResult Register(DtoUserRegistration dtoUserRegistration);
        DtoUserInfo GetUserByLogin(string login);
        string GetUserHashedPasswordByLogin(string login);
        bool EmailExists(string email);
    }
}
