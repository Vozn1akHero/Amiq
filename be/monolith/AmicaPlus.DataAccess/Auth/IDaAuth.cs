using AmicaPlus.Contracts.Auth;
using AmicaPlus.Contracts.User;

namespace AmicaPlus.DataAccess.Auth
{
    public interface IDaAuth
    {
        DtoUserRegistartionResult Register(DtoUserRegistration dtoUserRegistration);
        DtoUserInfo GetUserByLogin(string login);
        string GetUserHashedPasswordByLogin(string login);
        bool EmailExists(string email);
    }
}
